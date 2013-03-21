using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    class ActorClass
    {
        protected Point sector;
        protected Vector2 position;

        public Point Sector
        {
            get { return sector; }
        }
        public Vector2 Position
        {
            get { return position; }
        }
    }
}
