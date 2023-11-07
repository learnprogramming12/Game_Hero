using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class CollisionTest
    {

        public static void Test()
        {
            RectangleShape rectangle1 = new RectangleShape(new Vector2f(100, 50));
            rectangle1.Position = new Vector2f(100, 100);
            rectangle1.Rotation = 45.0f;

            RectangleShape rectangle2 = new RectangleShape(new Vector2f(10, 10));
            rectangle2.Position = new Vector2f(90, 170.72f);
            // rectangle2.Rotation = -30.0f;

            bool intersects = CheckRectangleIntersection(rectangle1, rectangle2);

            if (intersects)
            {
                Console.WriteLine("Rectangles intersect!");
            }
            else
            {
                Console.WriteLine("Rectangles do not intersect.");
            }
        }
        //This function is from internet in order to solve intersection checking problem after rotation. For example, a rectangular with (2, 10) size will
        //represent a larger area after rotating 45 degree. Because after rotation, it will be similar to the diagonal line of a rectangular.
        //havn't used it whether it works --20230618.
        public static bool CheckRectangleIntersection(RectangleShape rect1, RectangleShape rect2)
        {
            Transform transform1 = rect1.Transform;
            Transform transform2 = rect2.Transform;

            Vector2f[] axes = new Vector2f[4];

            axes[0] = transform1.TransformPoint(rect1.GetPoint(0)) - transform1.TransformPoint(rect1.GetPoint(1));
            axes[1] = transform1.TransformPoint(rect1.GetPoint(1)) - transform1.TransformPoint(rect1.GetPoint(2));
            axes[2] = transform2.TransformPoint(rect2.GetPoint(0)) - transform2.TransformPoint(rect2.GetPoint(1));
            axes[3] = transform2.TransformPoint(rect2.GetPoint(1)) - transform2.TransformPoint(rect2.GetPoint(2));

            foreach (Vector2f axis in axes)
            {
                float proj1Min = float.MaxValue, proj1Max = float.MinValue;
                float proj2Min = float.MaxValue, proj2Max = float.MinValue;

                ProjectRectangle(axis, transform1, rect1, ref proj1Min, ref proj1Max);
                ProjectRectangle(axis, transform2, rect2, ref proj2Min, ref proj2Max);

                if (proj1Max < proj2Min || proj2Max < proj1Min)
                    return false;
            }

            return true;
        }

        private static void ProjectRectangle(Vector2f axis, Transform transform, RectangleShape rect, ref float min, ref float max)
        {
            Vector2f[] points = new Vector2f[4];

            points[0] = transform.TransformPoint(rect.GetPoint(0));
            points[1] = transform.TransformPoint(rect.GetPoint(1));
            points[2] = transform.TransformPoint(rect.GetPoint(2));
            points[3] = transform.TransformPoint(rect.GetPoint(3));

            /*            float dotProduct = Vector2f.Dot(axis, points[0]);
            */
            float dotProduct = Dot(axis, points[0]);
            ;
            min = max = dotProduct;

            for (int i = 1; i < 4; i++)
            {
                /*                dotProduct = Vector2f.Dot(axis, points[i]);
                */
                dotProduct = Dot(axis, points[i]);

                if (dotProduct < min)
                    min = dotProduct;
                else if (dotProduct > max)
                    max = dotProduct;
            }
        }
        private static float Dot(Vector2f vector1, Vector2f vector2)
        {
            return vector1.X * vector2.X + vector1.Y * vector2.Y;
        }
    }
}
