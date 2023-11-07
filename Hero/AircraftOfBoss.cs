using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class AircraftOfBoss:AircraftOfEnemy
    {
        private Sprite _missleLauncher;
        private Sprite _missleLauncher2;
        private Sprite _cannon;
        public static Vector2f PrefferedAircraftSize = new Vector2f(256, 256);
        public AircraftOfBoss(int lifePoints, Vector2f speed, bool adjustHead) : base(lifePoints, speed, adjustHead)
        {
            _aircraftType = AircraftType.AircraftOfBoss;

            _sprite.Texture = TextureManager.Boss;
            FloatRect floatRectOfBoss = _sprite.GetLocalBounds();
            _sprite.Scale = new Vector2f(PrefferedAircraftSize.X / TextureManager.Boss.Size.X, PrefferedAircraftSize.Y / TextureManager.Boss.Size.Y);
            _sprite.Origin = new Vector2f(floatRectOfBoss.Width / 2f, floatRectOfBoss.Height / 2f);

            Vector2f prefferedLauncherSize = new Vector2f(60, 60);
            _missleLauncher = new Sprite();
            _missleLauncher.Texture = TextureManager.MissleLauncher;
            _missleLauncher.Scale = new Vector2f(prefferedLauncherSize.X / TextureManager.MissleLauncher.Size.X, prefferedLauncherSize.Y / TextureManager.MissleLauncher.Size.Y);
            _missleLauncher.Origin = new Vector2f(_missleLauncher.GetLocalBounds().Width / 2f, _missleLauncher.GetLocalBounds().Height / 2f);

            _missleLauncher2 = new Sprite();
            _missleLauncher2.Texture = TextureManager.MissleLauncher;
            _missleLauncher2.Origin = new Vector2f(_missleLauncher2.GetLocalBounds().Width / 2f, _missleLauncher2.GetLocalBounds().Height / 2f);
            _missleLauncher2.Scale = new Vector2f(prefferedLauncherSize.X / TextureManager.MissleLauncher.Size.X, prefferedLauncherSize.Y / TextureManager.MissleLauncher.Size.Y);

            _cannon = new Sprite();
            _cannon.Texture = TextureManager.Cannon;
            _cannon.Origin = new Vector2f(_cannon.GetLocalBounds().Width / 2f, _cannon.GetLocalBounds().Height / 2f);
            _cannon.Scale = new Vector2f(prefferedLauncherSize.X / TextureManager.Cannon.Size.X, prefferedLauncherSize.Y / TextureManager.Cannon.Size.Y);
            
            _healthSystem.Init(true, new Vector2f(0, 0), new Vector2f(60, 10), Color.Red);
        }
        public override void Fire(Vector2f attackTargetPos)
        {
            int firingCount = 4;
            CreateMissle(_missleLauncher, attackTargetPos, firingCount);
            CreateMissle(_missleLauncher2, attackTargetPos, firingCount);
            CreateCannonball(attackTargetPos);
        }
        private void CreateMissle(Sprite missleLauncher, Vector2f attackTargetPos, int firingCount)
        {
            Vector2f prefferedProjectileSize = new Vector2f(30f, 15f);
            FloatRect floatRectOfMissleLauncher = missleLauncher.GetGlobalBounds();
            float yFirstProjectilePos = floatRectOfMissleLauncher.Top + floatRectOfMissleLauncher.Height / 2f - firingCount / 2f * prefferedProjectileSize.Y;
            Vector2f speed = Tools.GetVectorSpeed(8, missleLauncher.Position, attackTargetPos);
            float rotation = Tools.GetDegree(missleLauncher.Position, attackTargetPos);

            for (int i = 0; i < firingCount; i++)
            {
                Vector2f projectilePos = new Vector2f(floatRectOfMissleLauncher.Left + floatRectOfMissleLauncher.Width / 2, yFirstProjectilePos + i * prefferedProjectileSize.Y);
                Projectile projectile = new Projectile(projectilePos, 1, speed);
                projectile.Sprite.Texture = TextureManager.EnemyMissle2;
                projectile.Sprite.Origin = missleLauncher.Position - new Vector2f(projectile.Sprite.GetGlobalBounds().Left, projectile.Sprite.GetGlobalBounds().Top);
                projectile.Sprite.Scale = new Vector2f(prefferedProjectileSize.X / TextureManager.EnemyMissle2.Size.X, prefferedProjectileSize.Y / TextureManager.EnemyMissle2.Size.Y);
                //adjust angle
                projectile.Sprite.Rotation = rotation;
                //Console.WriteLine("rotation: " + rotation + "   MissleLancherPosition: " + _missleLauncher.Position + "  GlobalLauncher: " + _missleLauncher.GetGlobalBounds() +  "  GlobalTopLeft: " + new Vector2f(projectile.Sprite.GetGlobalBounds().Left, projectile.Sprite.GetGlobalBounds().Top) + "  Position: " + projectile.Sprite.Position);
                ManagerOfEnemyFiredProjectile.FiredProjectiles.AddLast(projectile);
            }
        }
        //launch an attack with a sector shape
        private void CreateCannonball(Vector2f attackTargetPos)
        {
            Vector2f prefferedProjectileSize = new Vector2f(15f, 15f);
            FloatRect floatRectOfPlayer = Player.GetInstance().Sprite.GetGlobalBounds();
            //float yFirstProjectilePos = floatRectOfMissleLauncher.Top + floatRectOfMissleLauncher.Height / 2f - firingCount / 2f * prefferedProjectileSize.Y;
            // Vector2f speed = Tools.GetVectorSpeed(8, missleLauncher.Position, attackTargetPos);
            // float rotation = Tools.GetDegree(missleLauncher.Position, attackTargetPos);
            float xTargetOfFirstCannonball = floatRectOfPlayer.Left - floatRectOfPlayer.Width;
            for (int i = 0; i < 4; i++)
            {
                Vector2f target = new Vector2f(xTargetOfFirstCannonball + i * floatRectOfPlayer.Width, floatRectOfPlayer.Top + floatRectOfPlayer.Height / 2f);
                Vector2f speed = Tools.GetVectorSpeed(5, _cannon.Position, target);
                Projectile projectile = new Projectile(_cannon.Position, 1, speed);
                projectile.Sprite.Texture = TextureManager.BossBomb;
                projectile.Sprite.Origin = new Vector2f(projectile.Sprite.GetLocalBounds().Width / 2f, projectile.Sprite.GetLocalBounds().Height / 2f);
                projectile.Sprite.Scale = new Vector2f(prefferedProjectileSize.X / TextureManager.BossBomb.Size.X, prefferedProjectileSize.Y / TextureManager.BossBomb.Size.Y);
                //adjust angle
               // projectile.Sprite.Rotation = rotation;
                //Console.WriteLine("rotation: " + rotation + "   MissleLancherPosition: " + _missleLauncher.Position + "  GlobalLauncher: " + _missleLauncher.GetGlobalBounds() +  "  GlobalTopLeft: " + new Vector2f(projectile.Sprite.GetGlobalBounds().Left, projectile.Sprite.GetGlobalBounds().Top) + "  Position: " + projectile.Sprite.Position);
                ManagerOfEnemyFiredProjectile.FiredProjectiles.AddLast(projectile);
            }
        }
        private void CreateHoneyBadger(Vector2f attackTargetPos)
        {

        }
        public override void Update()
        {
            FloatRect floatRectOfBoss = _sprite.GetGlobalBounds();
            _sprite.Position = new Vector2f(_sprite.Position.X + _speed.X, _sprite.Position.Y + _speed.Y);
            _missleLauncher.Position = new Vector2f(floatRectOfBoss.Left + floatRectOfBoss.Width / 6f, floatRectOfBoss.Top + floatRectOfBoss.Height / 2f);
            _missleLauncher2.Position = new Vector2f(floatRectOfBoss.Left + floatRectOfBoss.Width / 6f * 5, floatRectOfBoss.Top + floatRectOfBoss.Height / 2f);
            _cannon.Position = new Vector2f(floatRectOfBoss.Left + floatRectOfBoss.Width / 2f, floatRectOfBoss.Top + floatRectOfBoss.Height / 2f);
           
            //adjust the angle
            Vector2f target = Player.GetInstance().Position;
            float degree = Tools.GetDegree(_missleLauncher.Position, target);
            _missleLauncher.Rotation = degree - 90;

            degree = Tools.GetDegree(_missleLauncher2.Position, target);
            _missleLauncher2.Rotation = degree - 90;

            degree = Tools.GetDegree(_cannon.Position, target);
            _cannon.Rotation = degree - 90;

            FloatRect globalRect = _sprite.GetGlobalBounds();
            _healthSystem.Update(new Vector2f(globalRect.Left + globalRect.Width / 2f, globalRect.Top));

/*            _speed = Tools.GetVectorSpeed(_speed, _sprite.Position, target);
            _sprite.Position = new Vector2f(_sprite.Position.X + _speed.X, _sprite.Position.Y + _speed.Y);*/
        }
        public override void Draw(RenderWindow window)
        {
            //base.Draw();
            window.Draw(_sprite);
            window.Draw(_missleLauncher);
            window.Draw(_missleLauncher2);
            window.Draw(_cannon);
            _healthSystem.Draw(window);
        }
    }
}
