using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class FormationOfHorizonShapeEnemy: EnemyFormation
    {
        private float _rotationAngleEachTime;
        private float _flyRangeY;
  
        private DoubleLinkedList<float> _rotationOfAllAircrafts = new DoubleLinkedList<float>();

        public FormationOfHorizonShapeEnemy(int numOfAircraftsInFormation, int numOfAircraftWillFire, 
            float fireFrequency, float rotationAngleEachTime = 2f, AircraftType aircraftType = AircraftType.AircraftOfGoldenEagle) : base(numOfAircraftsInFormation, numOfAircraftWillFire, fireFrequency, aircraftType) 
        {
            _formationType = FormationType.HorizonShape;
            _flyRangeY = Game.WindowSize.Y / 3f;
            _rotationAngleEachTime = rotationAngleEachTime;
            
            CreateFormation(aircraftType);
        }
        protected override void CreateFormation(AircraftType aircraftType)
        {
            Vector2f prefferedSpriteSize;
            switch(aircraftType)
            {
                case AircraftType.AircraftOfHarpyEagle:
                    prefferedSpriteSize = AircraftOfHarpyEagle.PrefferedAircraftSize;
                    break;
                case AircraftType.AircraftOfGoldenEagle:
                    prefferedSpriteSize = AircraftOfGoldenEagle.PrefferedAircraftSize;
                    break;
                case AircraftType.AircraftOfRotation:
                    prefferedSpriteSize = AircraftOfRotation.PrefferedAircraftSize;
                    break;
                case AircraftType.AircraftOfHoneyBadger:
                    prefferedSpriteSize = AircraftOfHoneyBadger.PrefferedAircraftSize;
                    break;
                case AircraftType.AircraftOfBoss:
                    prefferedSpriteSize = AircraftOfBoss.PrefferedAircraftSize;
                    break;
                default:
                    prefferedSpriteSize = AircraftOfGoldenEagle.PrefferedAircraftSize;
                    break;
            }
            float perSegmentWidth = (Game.WindowSize.X - 2 * prefferedSpriteSize.X) / _numOfAircraftsInFormation;
            float randomMax = perSegmentWidth - prefferedSpriteSize.X;
            if(randomMax <= 0)
            {
                throw new ArgumentOutOfRangeException("number of aircraft is too many.");
            }
            float xPos;
            for (int i = 0; i < _numOfAircraftsInFormation; i++)
            {
                float span = Game.Random.Next(0, Convert.ToInt32(randomMax));
                xPos = i * perSegmentWidth + span + prefferedSpriteSize.X;
               // Console.WriteLine("randomMax: " + randomMax.ToString() + "  Xpos:" + xPos.ToString());
                Vector2f aircraftPos = new Vector2f(xPos, -prefferedSpriteSize.Y / 2f + 10);//The origin is center of the aircraft, so set it negative value
                Vector2f speed = new Vector2f(0, 2f);
                AircraftOfEnemy aircraft;
                switch (aircraftType)
                {
                    case AircraftType.AircraftOfHarpyEagle:                  
                        aircraft = new AircraftOfHarpyEagle(2, speed, true);
                        break;
                    case AircraftType.AircraftOfGoldenEagle:
                        speed.Y = 2f;
                        aircraft = new AircraftOfGoldenEagle(1, speed, true);
                        break;
                    case AircraftType.AircraftOfRotation:
                        speed.Y = 4;
                        aircraft = new AircraftOfRotation(2, speed, false);
                        break;
                    case AircraftType.AircraftOfHoneyBadger:
                        speed.Y = 3;
                        aircraft = new AircraftOfHoneyBadger(2, speed, true);
                        break;
                    case AircraftType.AircraftOfBoss:
                        speed.Y = 0.1f;
                        aircraft = new AircraftOfBoss(200, speed, true);
                        break;
                    default:
                        speed.Y = 2f;
                        aircraft = new AircraftOfGoldenEagle(1, speed, true);
                        break;
                }
                aircraft.Sprite.Position = aircraftPos;

                _aircraftList.AddLast(aircraft);

                if (aircraftPos.X <= Game.WindowSize.X / 2f)
                {
                    _rotationOfAllAircrafts.AddLast(-_rotationAngleEachTime);
                }
                else
                {
                    _rotationOfAllAircrafts.AddLast(_rotationAngleEachTime);
                }
            }
        }
        private void Redirect(AircraftOfEnemy aircraft, int index)
        {
            if (_rotationOfAllAircrafts.Count != _aircraftList.Count)
                throw new IndexOutOfRangeException("Length of two lists are not same.");

            //aircraft.Sprite.Rotation += _rotationOfAllAircrafts[index];
            aircraft.Speed = Tools.GetVectorAfterRotation(new Vector2f(aircraft.Speed.X, aircraft.Speed.Y), _rotationOfAllAircrafts[index]);
        }
        private bool CheckIfOutOfRenderWindow(AircraftOfEnemy aircraft)
        {
            FloatRect rectWindow = new FloatRect(0, 0, Game.WindowSize.X, Game.WindowSize.Y);
            if (rectWindow.Intersects(aircraft.Sprite.GetGlobalBounds()))
                return false;

            return true;
        }

        private void DestroyAircraft(int index)
        {
            if (_aircraftList.Count != _rotationOfAllAircrafts.Count)
                throw new IndexOutOfRangeException("Length of two lists are not same.");

            _aircraftList.RemoveAt(index);
            _rotationOfAllAircrafts.RemoveAt(index);
        }
        public override void Update(Player player)
        {
            for(int i = 0; i < _aircraftList.Count;)
            {
                if (/*_rotationAngleEachTime != 0 && */_aircraftList[i].Sprite.Position.Y >= _flyRangeY)
                {
                    Redirect(_aircraftList[i], i);
                    //    Console.WriteLine($"PositionY: {_aircraftList[i].Position.Y}  flyRangeY: {_flyRangeY}   bounds: {_aircraftList[i].Sprite.GetGlobalBounds().ToString()}");

                    if (_aircraftList[i].AircraftType == AircraftType.AircraftOfHoneyBadger)
                    {
                        ((AircraftOfHoneyBadger)_aircraftList[i]).Chase = true;
                    }
                }

                _aircraftList[i].Update();

                bool shouldDestroy = false;
                if (CheckIfDestroyedByWeapon(_aircraftList[i]) || CheckIfCollidedByPlayer(_aircraftList[i], player))
                {
                    shouldDestroy = true;
                    ManagerOfAnimation.Add(new Animation(TextureManager.ExplosionAnimation, _aircraftList[i].Sprite.Position));
                }
                if (shouldDestroy || CheckIfOutOfRenderWindow(_aircraftList[i]))
                {
                    DestroyAircraft(i);
                    //Console.WriteLine($"count of horizon aircraft: {_aircraftList.Count}");

                    continue;
                }

                ++i;
            }
            
            FloatRect playerBounds = player.Sprite.GetGlobalBounds();
            Vector2f targetPos = new Vector2f(playerBounds.Left + playerBounds.Width / 2f, playerBounds.Top + playerBounds.Height / 2f);
            CoordinateFirepower(targetPos);
        }
    }
}
