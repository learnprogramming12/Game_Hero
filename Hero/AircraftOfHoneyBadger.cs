using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class AircraftOfHoneyBadger:AircraftOfEnemy
    {
        public static Vector2f PrefferedAircraftSize = new Vector2f(50, 60);
        //private float _startChaseY;
        //private int _chaseCount = 0;
        private bool _isChased = false;
        private int _targetDifference;
        public bool Chase { 
            set { _isChased = value; }
        }

        public AircraftOfHoneyBadger(int lifePoints, Vector2f speed, bool adjustHead) : base(lifePoints, speed, adjustHead)
        {
            _aircraftType = AircraftType.AircraftOfHoneyBadger;
            //_startChaseY = Game.WindowSize.Y / 3f;
            _targetDifference = Game.Random.Next(-(int)Player.GetInstance().Sprite.GetGlobalBounds().Width / 2, (int)Player.GetInstance().Sprite.GetGlobalBounds().Width / 2);
            //Console.WriteLine("========== " + (int)Player.GetInstance().Sprite.GetGlobalBounds().Width / 2);
            _sprite.Texture = TextureManager.EnemyHoneyBadger;
            _sprite.Scale = new Vector2f(PrefferedAircraftSize.X / TextureManager.EnemyHoneyBadger.Size.X, PrefferedAircraftSize.Y / TextureManager.EnemyHoneyBadger.Size.Y);
            _sprite.Origin = new Vector2f(_sprite.GetLocalBounds().Width / 2f, _sprite.GetLocalBounds().Height / 2f);

            _healthSystem.Init(true, new Vector2f(0, 0), new Vector2f(30, 5), Color.Red);
        }
        public override void Fire(Vector2f attackTargetPos)
        {
            Vector2f prefferedProjectileSize = new Vector2f(30f, 15f);
            Vector2f projectilePos = _sprite.Position;
            Vector2f speed = Tools.GetVectorSpeed(10f, projectilePos, attackTargetPos);

            Projectile projectile = new Projectile(projectilePos, 1, speed);
            projectile.Sprite.Texture = TextureManager.EnemyMissle2;
            projectile.Sprite.Origin = new Vector2f(projectile.Sprite.TextureRect.Width / 2f, projectile.Sprite.TextureRect.Height / 2f);
            projectile.Sprite.Scale = new Vector2f(prefferedProjectileSize.X / projectile.Sprite.TextureRect.Width, prefferedProjectileSize.Y / projectile.Sprite.TextureRect.Height);
            projectile.Sprite.Rotation = Tools.GetDegree(projectilePos, attackTargetPos);

            ManagerOfEnemyFiredProjectile.FiredProjectiles.AddLast(projectile);
        }
        public override void Update()
        {
            if (_isChased == false/* && _sprite.Position.Y < _startChaseY*/)
            {
                _sprite.Position = new Vector2f(_sprite.Position.X + Speed.X, _sprite.Position.Y + Speed.Y);
            }
            else
            {
                //_isChased = true;
                //_speed = new Vector2f(0, 3);
                Vector2f target = new Vector2f(Player.GetInstance().Position.X + _targetDifference, Player.GetInstance().Position.Y + _targetDifference);
                float degree = Tools.GetDegree(_sprite.Position, target);
                _sprite.Rotation = degree - 90;//the initial direction of enemy aircraft's head is downward by default
                Speed = Tools.GetVectorSpeed(Speed, _sprite.Position, target);
                _sprite.Position = new Vector2f(_sprite.Position.X + Speed.X, _sprite.Position.Y + Speed.Y);
            }
            _healthSystem.Update(_sprite.Position);
        }
        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
            _healthSystem.Draw(window);
        }
    }
}
