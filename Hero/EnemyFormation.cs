using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    enum FormationType
    {
        HorizonShape,
        SlashShape,
        VShape,
        AroundCenterShape,
        None
    }
    internal abstract class EnemyFormation
    {
        protected FormationType _formationType = FormationType.None;
        protected AircraftType _aircraftType;
        protected int _numOfAircraftsInFormation;
        protected int _numOfAircraftWillFire;//number of aircrafts that will fire in order to coordinate the firepower. Can be used to different difficluty level by changing it
        protected int _numOfAircraftDestroyedByPlayer = 0;
        protected int _fireDelay;//it means how many times of update(frame) this formation ignores until it coordinates firepower.Can be used to different difficluty level by changing it
        protected int _countForDelay = 0;
        protected DoubleLinkedList<AircraftOfEnemy> _aircraftList = new DoubleLinkedList<AircraftOfEnemy>();
        //protected RenderWindow _window;

        public FormationType FormationType { get { return _formationType; } }
        public AircraftType AircraftType { get { return _aircraftType; } }
        protected abstract void CreateFormation(AircraftType type);
        public abstract void Update(Player player);
        
        //fireFrequency means how long aircraft fire once outside. Inside it means how many frame update it ignores until it fires.
        //for example, if the firing frequency is 0.5, it means every 2 seconds the formation will coordinate firing
        public EnemyFormation(int numOfAircraftsInFormation, int numOfAircraftWillFire,
            float fireFrequency, AircraftType aircraftType)
        {
            //_window = window;
            _fireDelay = Convert.ToInt32((float)Game.FrameLimit / fireFrequency);
            _numOfAircraftsInFormation = numOfAircraftsInFormation;
            _numOfAircraftWillFire = numOfAircraftWillFire;
            _aircraftType = aircraftType;
            if (numOfAircraftWillFire > numOfAircraftsInFormation)
                _numOfAircraftWillFire = numOfAircraftsInFormation;

        }
        public bool AreAllAircraftsDestroyed() { return _aircraftList.IsEmpty(); }
        public void Draw(RenderWindow window)
        {
            for (int i = 0; i < _aircraftList.Count; i++)
            {
                _aircraftList[i].Draw(window);
            }
        }
        //This is just a simple method to coordinate the firepower.Maybe a sector would be better
        protected virtual void CoordinateFirepower(Vector2f targetPos)
        {
            if (_aircraftList.IsEmpty())
            {
                _numOfAircraftWillFire = 0;
                return;
            }
            if (_numOfAircraftWillFire > _aircraftList.Count)
                _numOfAircraftWillFire = _aircraftList.Count;

            _countForDelay++;
            if (_countForDelay < _fireDelay)
                return;
            _countForDelay -= _fireDelay;//count for next fire
            //it's time to fire
            int[] aircraftsWillFire = Tools.ProduceNonrepetitiveNumber(0, _aircraftList.Count - 1, _numOfAircraftWillFire);
            for (int i = 0; i < aircraftsWillFire.Length; i++)
            {
                _aircraftList[aircraftsWillFire[i]].Fire(targetPos);
            }
        }
        protected bool CheckIfCollidedByPlayer(AircraftOfEnemy aircraft, Player player)
        {
            bool aircraftDestroyed = false;
            if (player.Sprite.GetGlobalBounds().Intersects(aircraft.Sprite.GetGlobalBounds()))
            {
                int temp = aircraft.LifePoints;
                aircraft.LifePoints -= player.LifePoints;
                player.LifePoints -= temp;
                if (player.LifePoints <= 0 )
                {
                    Player.PlayerState = PlayerState.Lost;
                }
                if(aircraft.LifePoints <= 0 )
                {
                    aircraftDestroyed = true;
                    _numOfAircraftDestroyedByPlayer++;
                }
                Console.WriteLine("harmByCollision: " + temp + "  life points: " + player.LifePoints.ToString());
                ManagerOfMusic.Explosion.Play();
            }

            return aircraftDestroyed;
        }
        protected virtual bool CheckIfDestroyedByWeapon(AircraftOfEnemy aircraft)
        {
            DoubleLinkedList<Projectile> projectiles = ManagerOfPlayerFiredProjectile.FiredProjectiles;
            for (int i = 0; i < projectiles.Count;)
            {
                if (aircraft.Sprite.GetGlobalBounds().Intersects(projectiles[i].Sprite.GetGlobalBounds()))
                {
                    //player get score
                    Player.Score += 10;

                    Projectile projectile = projectiles[i];
                    projectiles.Remove(projectile);
                    aircraft.LifePoints -= projectile.DamagePower;
                    if (aircraft.LifePoints <= 0)
                    {
                        _numOfAircraftDestroyedByPlayer++;
                        ManagerOfMusic.Explosion.Play();

                        return true;
                    }
                }
                else
                {
                    ++i;
                }
            }
            return false;
        }
    }
}
