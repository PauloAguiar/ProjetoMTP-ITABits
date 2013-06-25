using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Input = Microsoft.Xna.Framework.Input;
using SlimDX.DirectInput;

namespace Projeto_Apollo_16
{
    public sealed partial class PlayerClass : ActorClass
    {
        public enum Bullets
        {
            linear = 0,
            circular,
            homing,

        }
        public Bullets bullets = Bullets.linear;
        public const int NUMBER_TYPE_BULLETS = 3;

    }
}
