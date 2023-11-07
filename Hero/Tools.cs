using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero
{
    internal class Tools
    {
        public static Vector2f GetVectorSpeed(float speed, Vector2f position, Vector2f targetPostion)
        {
            Vector2f vectorSpeed = new Vector2f(0, 0);
            Vector2f vectorGap = targetPostion - position;
            double distance = Math.Sqrt(Math.Pow(vectorGap.X, 2) + Math.Pow(vectorGap.Y, 2));
            if (distance == 0)
                return vectorSpeed;
            vectorSpeed.X = speed * (vectorGap.X / Convert.ToSingle(distance));
            vectorSpeed.Y = speed * (vectorGap.Y / Convert.ToSingle(distance));

            return vectorSpeed;
        }
        public static Vector2f GetVectorSpeed(Vector2f speed, Vector2f position, Vector2f targetPostion)
        {
            double dSpeed = Math.Sqrt(Math.Pow(speed.X, 2) + Math.Pow(speed.Y, 2));

            return GetVectorSpeed((float)dSpeed, position, targetPostion);
        }

        public static float GetDegree(Vector2f original, Vector2f target)
        {
            Vector2f vecDistance = target - original;
            double Radians = Math.Atan2(vecDistance.Y, vecDistance.X);
            double Degrees = Radians * (180.0 / Math.PI);

            return (float)Degrees;
        }

        /* This function will return a vector value after specifying a rotatingDegree. There are several things to NOTE.
         * 1.The axis is not the same as mathmatis. As for degree, clockwise will become larger, otherwise, it will become less, just like the window axis
         * 2. Both parameters are positive or negative. This means, for example: 1, the vector (4, -4) will be in top-right direction.
         * (4, 4) will in bottom-right direction. 2, if rotating degree is positive, it will be clockwise rotating. These are based on the first point
         *3.The Altan2 will return (-180, 180) degree after converting its returned radians into degree. So I need to convert the degree into a positive value in order
            to calculate the vector. For example, vector (-4, -4) is in top-left and it will be -135 degree. So I need to convert it into 
            -135 + 360 = 225 degree. So in this way, I can calculate the actual vector component.
        Created by Cui 20230613.
         */
        public static Vector2f GetVectorAfterRotation(Vector2f vectorOriginal, float rotatingDegree)
        {
            double OriginalRadians = Math.Atan2(vectorOriginal.Y, vectorOriginal.X);
            double OriginalDegrees = OriginalRadians * (180.0 / Math.PI);
/*            if (OriginalDegrees < 0)
                OriginalDegrees += 360;*/

            double ResultDegrees = OriginalDegrees + rotatingDegree;
            double Resultadians = Math.PI * ResultDegrees / 180.0;

            double chord = Math.Sqrt(Math.Pow(vectorOriginal.X, 2) + Math.Pow(vectorOriginal.Y, 2));
            double xValue = Math.Cos(Resultadians) * chord;
            double yValue = Math.Sin(Resultadians) * chord;

            return  new Vector2f((float)(Math.Cos(Resultadians) * chord), (float)(Math.Sin(Resultadians) * chord));

        }
        
        public static int[] ProduceNonrepetitiveNumber(int iLow, int iHigh, int iNum)
        {
            int[] index = new int[iHigh - iLow + 1];
            int j = 0;
            for (int i = iLow; i < iHigh + 1; i++, j++)
                index[j] = i;
            Random r = new Random();
            int[] result = new int[iNum];
            int iSite = index.Length;
            int id;
            for (int i = 0; i < iNum; i++)
            {
                id = r.Next(0, iSite - 1);
                result[i] = index[id];
                index[id] = index[iSite - 1];
                iSite--;
            }
            return result;
        }
    }
}
