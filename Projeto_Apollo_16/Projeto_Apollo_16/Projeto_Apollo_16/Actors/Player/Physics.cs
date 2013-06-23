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

        void UpdateThrottle(int yState)
        {
            if (joystickState.Y < 0)
            {
                throttle = -joystickState.Y * maxThrottle / joystickRange;
            }
            else if (joystickState.Y > 0)
            {
                throttle = joystickState.Y * minThrottle / joystickRange;
            }
        }
    }
}
