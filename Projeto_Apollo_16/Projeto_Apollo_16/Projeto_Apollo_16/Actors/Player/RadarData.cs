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
        public RadarDataImmediate GetRadarImmediateData(PlayerClass player)
        {
            return new RadarDataImmediate(player.Angle);
        }


    }
}
