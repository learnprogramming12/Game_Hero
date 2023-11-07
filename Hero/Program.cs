/*
 * Ideas of the game are from a VCD game many years ago.They are not mine. But I don't remeber the name of the original game.
 * This is just a simple unfinished game, still get much work to do. And by doing it, I found I really need to learn many things
 * a lot, especially architecture, OOP and so on.--Cui 20230618.
 */
//Xiupeng Cui 1566716
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;


namespace Hero
{
    internal class Program
    {
        static void Main(string[] args)
        {



            Game game = new Game();
            game.Run();

            /*            Vector2f vector = new Vector2f(-3f, 0.01f);

                        float angleInRadians = (float)Math.Atan2(vector.Y, vector.X);
                        float angleInDegrees = angleInRadians * (180.0f / (float)Math.PI);
                        if (angleInDegrees < 0)
                            angleInDegrees += 360;
                        double radians = Math.PI * angleInDegrees / 180.0;
                        float speed = 3f;
                        double xSpeed = Math.Cos(radians) * speed;
                        double ySpeed = Math.Sin(radians) * speed;

                        Console.WriteLine("Degree: " + angleInDegrees.ToString() + "  xSpeed: " + xSpeed.ToString() + "  ySpeed: " + ySpeed.ToString());*/
            /*            double angleInDegrees = 225;
                        double radians = Math.PI * angleInDegrees / 180.0;

                        double cos = Math.Cos(radians);
                        Console.WriteLine(cos.ToString());*/


            /*            RectangleShape rectangle = new RectangleShape(new SFML.System.Vector2f(100, 100));
                        rectangle.Position = new SFML.System.Vector2f(100, 100);

                        FloatRect bounds = rectangle.GetGlobalBounds();
                        Console.WriteLine("Before rotation: " + bounds.Left + "  " + bounds.Top + "  " + bounds.Width + " x " + bounds.Height);
                        rectangle.Origin = new Vector2f(50f, 50f);

                        rectangle.Rotation = 45.0f;

                        bounds = rectangle.GetGlobalBounds();
                        Console.WriteLine("After rotation: " + bounds.Left + "  " + bounds.Top + "  " + bounds.Width + " x " + bounds.Height);
                        bool b = bounds.Contains(70f, 99.99999f);
                        Console.WriteLine(b);
                        Console.WriteLine("-----------------------");*/




            /*            DoubleLinkedList<int> list = new DoubleLinkedList<int>();
                        list.AddLast(1);
                        list.AddLast(2);
                        list.AddLast(3);
                        for (int i = 0; i < list.Count; i++)
                        {
                            Console.WriteLine(list[i]);
                        }
                        list.RemoveElement(2);
                        list.AddFirst(5);
                        list.RemoveElement(0);
                        list.RemoveElement(2);
                        Console.WriteLine("count: " + list.Count.ToString());
                        for (int i = 0; i < list.Count; i++)
                        {
                            Console.WriteLine(list[i]);
                        }*/
            /*            DoubleLinkedList<Plane> list = new DoubleLinkedList<Plane>();
                        Plane plane1 = new Plane(0, 0);
                        Plane plane2 = plane1;
                        Plane plane3 = new Plane(1, 1);

                        if (plane1 == plane2)
                            Console.WriteLine("plane1 == plane2");
                        if (plane1 == plane3)
                            Console.WriteLine("plane1 == plane3");

                        list.AddLast(plane1);
                        list.AddLast(plane2);
                        list.AddLast(plane3);

                        for(int i  = 0; i < list.Count; i++)
                        {
                            if(plane1 == list[i])
                            {
                                Console.WriteLine($"list[{i}] == plane1");
                            }
                        }
                        list.Remove(plane1);
                        Console.WriteLine("---------------------");
                        Console.WriteLine($"Count == {list.Count}");
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (plane1 == list[i])
                            {
                                Console.WriteLine($"list[{i}] == plane1");
                            }
                        }*/

            Console.ReadLine();
        }
    

  


    }
}
