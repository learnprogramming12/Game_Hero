using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    //This is a very simple weapon firing system for player
    internal class WeaponSystem
    {
/*        public enum ProjectileType
        {
        }*/

        public enum FirepowerIntensity
        {
            Bullet,
            SingleMissle,
            DoubleMissle,
            FourMissle,
            ScatteredMissle
        }
        private Sprite _aircraft;
        private FirepowerIntensity _firepowerIntensity;
        // private ProjectileType _projectileType;
        private int _projectileSpeed = 10;
        public FirepowerIntensity FirePowerIntensity
        {
            get { return _firepowerIntensity; }
            set { _firepowerIntensity = value; }
        }
        public WeaponSystem(Sprite aircraft, FirepowerIntensity intensity)
        {
            _aircraft = aircraft;
            _firepowerIntensity = intensity;
        }
        public void UpgradeWeaponSystem(FirepowerIntensity intensity)
        {
            _firepowerIntensity = intensity;
        }
        public void Fire()
        {
            int firingCount = 0;
            Texture texture = TextureManager.Missile;
            Vector2u textureSize = TextureManager.Missile.Size;
            switch (_firepowerIntensity)
            {
                case FirepowerIntensity.Bullet:
                    firingCount = 1;
                    texture = TextureManager.Bullet;
                    textureSize = TextureManager.Bullet.Size;
                    break;
                case FirepowerIntensity.SingleMissle:
                    firingCount = 1;
                    break;
                case FirepowerIntensity.DoubleMissle:
                    firingCount = 2;
                    break;
                case FirepowerIntensity.FourMissle:
                    firingCount = 4;
                    break;
                case FirepowerIntensity.ScatteredMissle:
                    firingCount = 4; 
                    break;
            }

            Vector2f prefferedProjectileSize = new Vector2f(15, 25);
            FloatRect spriteBounds = _aircraft.GetGlobalBounds();
            float xFirstProjectilePos = spriteBounds.Left + spriteBounds.Width / 2f - firingCount / 2f * prefferedProjectileSize.X;
            for(int i = 0; i < firingCount; i++)
            {
                Vector2f projectilePos = new Vector2f(xFirstProjectilePos + i * prefferedProjectileSize.X, spriteBounds.Top + 1);
                Vector2f targetPos = new Vector2f(projectilePos.X, 0);
                Vector2f speed = Tools.GetVectorSpeed(_projectileSpeed, projectilePos, targetPos);
                Projectile projectile = new Projectile(projectilePos, 1, speed);

                projectile.Sprite.Texture = texture;//TextureManager.Missile;
                projectile.Sprite.Scale = new Vector2f(prefferedProjectileSize.X / textureSize.X, prefferedProjectileSize.Y / textureSize.Y);
                //projectile.Sprite.Scale = new Vector2f(prefferedProjectileSize.X / TextureManager.Missile.Size.X, prefferedProjectileSize.Y / TextureManager.Missile.Size.Y);

                ManagerOfPlayerFiredProjectile.FiredProjectiles.AddLast(projectile);
            }
/*            Projectile target = null;double minDistance = int.MaxValue;
            for (int i = 0; i < ManagerOfEnemyFiredProjectile.FiredProjectiles.Count; i++)
            {
                Vector2f pos = ManagerOfEnemyFiredProjectile.FiredProjectiles[i].Sprite.Position - Player.GetInstance().Position;
                double temp = Math.Sqrt(Math.Pow(pos.X, 2) + Math.Pow(pos.Y, 2));
                if (temp < minDistance)
                {
                    target = ManagerOfEnemyFiredProjectile.FiredProjectiles[i];
                    minDistance = temp;
                }
            }
            if(target != null)
            {
                Projectile cruise = new CruiseMissile(target, Player.GetInstance().Position, 2, new Vector2f(0, 10f));
                cruise.Sprite.Texture = TextureManager.Missile;
                cruise.Sprite.Scale = new Vector2f(prefferedProjectileSize.X / TextureManager.Missile.Size.X, prefferedProjectileSize.Y / TextureManager.Missile.Size.Y);
               
                ManagerOfPlayerFiredProjectile.FiredProjectiles.AddLast(cruise);
            }*/

            ManagerOfMusic.Missle.Play();
        }
    }
}
