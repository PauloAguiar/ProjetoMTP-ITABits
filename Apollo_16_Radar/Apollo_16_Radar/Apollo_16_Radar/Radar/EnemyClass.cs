using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Apollo_16_Radar
{
    public enum EnemyTypes
    {
        GHOST = 0,
        POLYGON,
        SUN,
        CHASER,
    }

    public class EnemyClass
    {
        public Int32 type;
        protected Vector2 globalPosition;
       
        public EnemyClass()
        {
        }

        public EnemyClass(Int32 type, Vector2 globalPosition)
        {
            this.type = type;
            this.globalPosition = globalPosition;
        }
    }
}
