using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class AircraftOfRotation:AircraftOfEnemy
    {
        public static Vector2f PrefferedAircraftSize = new Vector2f(60, 60);
        public AircraftOfRotation(int lifePoints, Vector2f speed, bool adjustHead): base(lifePoints, speed, adjustHead)
        {
            _aircraftType = AircraftType.AircraftOfRotation;

            _sprite.Texture = TextureManager.EnemyRotation;
            _sprite.Scale = new Vector2f(PrefferedAircraftSize.X / TextureManager.Enemy.Size.X, PrefferedAircraftSize.Y / TextureManager.Enemy.Size.Y);
            _sprite.Origin = new Vector2f(_sprite.GetLocalBounds().Width / 2f, _sprite.GetLocalBounds().Height / 2f);

            _healthSystem.Init(false, new Vector2f(0, 0), new Vector2f(30, 5), Color.Red);
        }
        public override void Fire(Vector2f attackTargetPos)
        {             
        }
        public override void Update()
        {
            _sprite.Position = new Vector2f(_sprite.Position.X + Speed.X, _sprite.Position.Y + Speed.Y);
            _sprite.Rotation += 2;

            _healthSystem.Update(_sprite.Position);
        }
    }
}
