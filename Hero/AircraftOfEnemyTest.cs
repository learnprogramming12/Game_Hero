using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class AircraftOfEnemyTest: AircraftOfEnemy
    {
        int _count = 0;
        public AircraftOfEnemyTest(int lifePoints, Vector2f speed,bool adjustHead) :base(lifePoints, speed, adjustHead)
        {

        }
        Sprite _spriteTest = new Sprite();
        public override void Fire(Vector2f attackTargetPos)
        {
            Vector2f prefferedProjectileSize = new Vector2f(30f, 15f);
            FloatRect spriteBounds = _sprite.GetGlobalBounds();
            Vector2f projectilePos = new Vector2f(spriteBounds.Left + spriteBounds.Width/* - prefferedProjectileSize.X) *// 2f, spriteBounds.Top + spriteBounds.Height/* - prefferedProjectileSize.Y)*/ / 2f);
            // Vector2f targetPos = new Vector2f(projectilePos.X, 0);
            Vector2f speed = Tools.GetVectorSpeed(10f, projectilePos, attackTargetPos);

            Projectile projectile = new Projectile(projectilePos, 1, speed);
            projectile.Sprite.Texture = TextureManager.EnemyMissle2;
            projectile.Sprite.Origin = new Vector2f(projectile.Sprite.TextureRect.Width / 2f, projectile.Sprite.TextureRect.Height / 2f);
            projectile.Sprite.Scale = new Vector2f(prefferedProjectileSize.X / projectile.Sprite.TextureRect.Width, prefferedProjectileSize.Y / projectile.Sprite.TextureRect.Height);
            projectile.Sprite.Rotation = Tools.GetDegree(projectilePos, attackTargetPos);
            

            ++_count;
            if(_count > 1)
            {
                Console.WriteLine("aircraft: " + spriteBounds.ToString() + "  Local: " + projectile.Sprite.GetLocalBounds()
                    + "Position: " + projectile.Sprite.Position.ToString() + "projectile: " + projectile.Sprite.GetGlobalBounds());
                _count = 0;
            }

            ManagerOfEnemyFiredProjectile.FiredProjectiles.AddLast(projectile);
        }
    }
}
