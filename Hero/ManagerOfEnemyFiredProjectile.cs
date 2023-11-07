using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    //I don't think the fired projectiles should be maintained in enemy's aircraft or player.
    //For example, an enemy's aircraft fired a projectile, but this aircraft is hit after that. So the aircraft should be distroyed but
    //the fired projectile should still fly and may hit the target. So if the fired projectile is maintained in aircraft, it will be disappeared
    //when it should not.Remember to rethink it.---20230612
    internal class ManagerOfEnemyFiredProjectile
    {
        private static int _countOfBeHitByEnemy = 0;
        public static DoubleLinkedList<Projectile> FiredProjectiles { get; } = new DoubleLinkedList<Projectile>();
        public static void init()
        {
            _countOfBeHitByEnemy = 0;
            FiredProjectiles.Clear();
        }
        private static void Update2(Player player)
        {
            for (int i = 0; i < FiredProjectiles.Count;)
            {
                Projectile projectile = FiredProjectiles[i];
                if (player.Sprite.GetGlobalBounds().Intersects(projectile.Sprite.GetGlobalBounds()))
                {
                    FiredProjectiles.Remove(projectile);
                    player.LifePoints -= projectile.DamagePower;
                    //for test
                    _countOfBeHitByEnemy++;
                    Console.WriteLine("be hit: " + _countOfBeHitByEnemy.ToString() + "  life points: " + player.LifePoints.ToString());
                    if (player.LifePoints <= 0)
                    {
                        return;
                    }
                }
                else
                {
                    ++i;
                }
            }
        }
        public static void Update(Player player)
        {
            for (int i = 0; i < FiredProjectiles.Count;)
            {
                FiredProjectiles[i].Update();
                if (FiredProjectiles[i].Valid == false)
                {
                    FiredProjectiles.Remove(FiredProjectiles[i]);
                }
                else
                {
                    ++i;
                }
            }
            Update2(player);
           //Console.WriteLine("Enemy bullet: " +  FiredProjectiles.Count.ToString());
        }
        public static void Draw(RenderWindow window)
        {
            for (int i = 0; i < FiredProjectiles.Count; i++)
            {
                FiredProjectiles[i].Draw(window);
            }

        }
    }
}
