using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class ManagerOfPlayerFiredProjectile
    {
        public static DoubleLinkedList<Projectile> FiredProjectiles { get; } = new DoubleLinkedList<Projectile>();
        public static void init()
        {
            FiredProjectiles.Clear();
        }
        public static void Update()
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
                    i++;
                }
            }
         //   Console.WriteLine("Player bullet: " + FiredProjectiles.Count);
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
