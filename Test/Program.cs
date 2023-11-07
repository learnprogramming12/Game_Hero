using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode(1000,1000), "SFML Window");

            Texture texture = new Texture("../../../resources/sprite.png");
            Sprite sprite = new Sprite(texture);

            Vector2f prefferedProjectileSize = new Vector2f(30f, 15f);
            FloatRect spriteBounds = sprite.GetGlobalBounds();
            Vector2f projectilePos = new Vector2f(spriteBounds.Left + (spriteBounds.Width - prefferedProjectileSize.X) / 2f, spriteBounds.Top + (spriteBounds.Height - prefferedProjectileSize.Y) / 2f);
            // Vector2f targetPos = new Vector2f(projectilePos.X, 0);
            /*            Vector2f speed = Tools.GetVectorSpeed(0.1f, projectilePos, attackTargetPos);
            */
            Console.WriteLine("local: " + sprite.GetLocalBounds() + "  TextureRect: " + sprite.TextureRect.Width + "  global: " + sprite.GetGlobalBounds());

            sprite.Scale = new Vector2f(prefferedProjectileSize.X / sprite.Texture.Size.X, prefferedProjectileSize.Y / sprite.Texture.Size.Y);

            sprite.Origin = new Vector2f(-10f, -10f);//new Vector2f(sprite.TextureRect.Width / 2f, sprite.TextureRect.Height / 2f);

            // sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2f, sprite.TextureRect.Height / 2f);
            sprite.Position = new Vector2f(100f, 100f/*prefferedProjectileSize.X / 2f, prefferedProjectileSize.Y / 2f*/);
            
            Console.WriteLine("local: " + sprite.GetLocalBounds() + "  TextureRect: " + sprite.TextureRect.Width + "  global: " + sprite.GetGlobalBounds());
            Console.WriteLine("Origin: " + sprite.Origin);
            //sprite.Rotation = 45;

            Sprite sprite2 = new Sprite(texture);
            sprite2.Scale = new Vector2f(prefferedProjectileSize.X / sprite.Texture.Size.X, prefferedProjectileSize.Y / sprite.Texture.Size.Y);

            sprite2.Origin = new Vector2f(-10f, -10f);//new Vector2f(sprite.TextureRect.Width / 2f, sprite.TextureRect.Height / 2f);

            // sprite.Origin = new Vector2f(sprite.TextureRect.Width / 2f, sprite.TextureRect.Height / 2f);
            sprite2.Position = new Vector2f(100f, 115f/*prefferedProjectileSize.X / 2f, prefferedProjectileSize.Y / 2f*/);

            RectangleShape rectangle = new RectangleShape(new Vector2f(100, 100));
            rectangle.Position = new Vector2f(1, 1);
            rectangle.FillColor = Color.Transparent;
            rectangle.OutlineColor = Color.Red;
            rectangle.OutlineThickness = 1.0f;


            window.MouseButtonReleased += (s, e) => { sprite.Rotation += 45; sprite2.Rotation += 45; };
            window.Closed += (s, e) => { window.Close(); };
            
            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear();


                window.Draw(sprite);
                window.Draw(sprite2);   
                window.Draw(rectangle);
                

                window.Display();
            }
        }
    }
}
