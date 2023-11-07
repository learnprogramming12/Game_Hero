using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    enum AircraftType
    {
        AircraftOfGoldenEagle,
        AircraftOfHarpyEagle,
        AircraftOfHoneyBadger,
        AircraftOfRotation,
        AircraftOfBoss
    }
    internal abstract class AircraftOfEnemy
    {
       // protected int _lifePoints;
       // protected int _firePower = 1;
        protected Vector2f _speed;
        protected AircraftType _aircraftType;
        //protected RenderWindow _window;
        protected bool _adjustHead;//make direction of aircraft's head align with it's speed 
        protected HealthSystem _healthSystem;

        protected Sprite _sprite;
        public int LifePoints
        {
            get { return _healthSystem.RemainingLifePoints; }
            set { //_lifePoints = value;
                _healthSystem.RemainingLifePoints = value;
            }
        }
        public AircraftType AircraftType { get { return _aircraftType; } }
        public Sprite Sprite
        {
            get { return _sprite; }
        }
        public Vector2f Speed
        {
            get { return _speed; }
            set 
            {
                _speed = value;
                if(_adjustHead)
                    _sprite.Rotation = Tools.GetDegree(new Vector2f(0, 0), _speed) - 90;//default direction of enemy aircraft's head is downward.
            }
        }
        public AircraftOfEnemy(int lifePoints, Vector2f speed, bool adjustHead = true)
        {
            _healthSystem = new HealthSystem(lifePoints);
            _sprite = new Sprite();
            _adjustHead = adjustHead;
            Speed = speed;
        }

        public abstract void Fire(Vector2f attackTargetPos);
        public virtual void Update()
        {
            _sprite.Position = new Vector2f(_sprite.Position.X + _speed.X, _sprite.Position.Y + _speed.Y);
            _healthSystem.Update(_sprite.Position);

        }
        public virtual void Draw(RenderWindow window)
        {
            window.Draw(_sprite);
            _healthSystem.Draw(window);

        }
    }
}
