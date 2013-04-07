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



    }

}
