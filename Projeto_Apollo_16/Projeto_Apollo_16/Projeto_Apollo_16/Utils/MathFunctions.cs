using System;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public static class MathFunctions
    {
        public static double Clamp(double value, double min, double max)
        {
            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }

        public static Vector2 AngleToVector(double angle)
        {
            double vectorAngle = (angle - Math.PI / 2);
            return  new Vector2((float)Math.Cos(vectorAngle), (float)Math.Sin(vectorAngle));
            
        }

        public static float VectorToAngle(Vector2 v)
        {
            v = RotateVector(v, -(float)Math.PI / 2);
            float angle = (float)Math.Atan(v.Y / v.X);
            return angle;
        }

        public static float Sqrt(double x)
        {
            return (float)Math.Sqrt(x);
        }

        public static Vector2 RotateVector(Vector2 v, float angle)
        {
            float x = v.X * (float)Math.Cos(angle) - v.Y * (float)Math.Sin(angle);
            float y = v.X * (float)Math.Sin(angle) + v.Y * (float)Math.Cos(angle);

            return new Vector2(x, y);
        }


    }

}
