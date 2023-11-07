using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class AircraftOfGoldenEagle:AircraftOfEnemy
    {
        public static Vector2f PrefferedAircraftSize = new Vector2f(50, 60);
        public AircraftOfGoldenEagle(int lifePoints, Vector2f speed, bool adjustHead): base(lifePoints, speed, adjustHead)
        {
            _aircraftType = AircraftType.AircraftOfGoldenEagle;

            _sprite.Texture = TextureManager.Enemy;
            _sprite.Scale = new Vector2f(PrefferedAircraftSize.X / TextureManager.Enemy.Size.X, PrefferedAircraftSize.Y / TextureManager.Enemy.Size.Y);
            _sprite.Origin = new Vector2f(_sprite.GetLocalBounds().Width / 2f, _sprite.GetLocalBounds().Height / 2f);

            _healthSystem.Init(true, new Vector2f(0, 0), new Vector2f(30, 5), Color.Red);
        }
        public override void Fire(Vector2f attackTargetPos)
        {
            Vector2f prefferedProjectileSize = new Vector2f(30f, 15f);
            Vector2f projectilePos = _sprite.Position;
            Vector2f speed = Tools.GetVectorSpeed(5, projectilePos, attackTargetPos);

            Projectile projectile = new Projectile(projectilePos, 1, speed);
            projectile.Sprite.Origin = new Vector2f(prefferedProjectileSize.X / 2f, prefferedProjectileSize.Y / 2f);
            projectile.Sprite.Texture = TextureManager.Bomb;
            projectile.Sprite.Scale = new Vector2f(prefferedProjectileSize.X / TextureManager.Bomb.Size.X, prefferedProjectileSize.Y / TextureManager.Bomb.Size.Y);
            projectile.Sprite.Rotation = Tools.GetDegree(projectilePos, attackTargetPos);

            ManagerOfEnemyFiredProjectile.FiredProjectiles.AddLast(projectile);
        }
    }
}
