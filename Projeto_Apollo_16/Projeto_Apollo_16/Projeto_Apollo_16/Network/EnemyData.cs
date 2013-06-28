using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16.Network
{
    public enum EnemyTypes
    {
        GHOST = 0,
        POLYGON,
        SUN,
        CHASER,
    }

    public class EnemyData
    {
        public Int32 type;
        protected Vector2 globalPosition;

        public EnemyData()
        {
        }

        public EnemyData(Int32 type, Vector2 globalPosition)
        {
            this.type = type;
            this.globalPosition = globalPosition;
        }
    }
}
