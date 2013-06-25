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
        JoystickState joystickState = new JoystickState();
        public int joystickRange = 400;
        public JoystickState state { get; set; }
        float maxRotationZ; //dpende do joystickRange
        
        void UpdateJoystick(JoystickState joystickState, int joystickRange)
        {
            this.joystickState = joystickState;
            this.joystickRange = joystickRange;
            maxRotationZ = joystickRange / 25.0f;
        }

        void UpdateJoystickInput()
        {
            if (joystickState.Y < 0)
            {
                throttle = -joystickState.Y * maxThrottle / joystickRange;
            }
            else if (joystickState.Y > 0)
            {
                throttle = joystickState.Y * minThrottle / joystickRange;
            }

            maxThrottle = MAX_MAX_THROTTLE * (-joystickState.Z + joystickRange) / joystickRange;
            minThrottle = MIN_MIN_THROTTLE * (-joystickState.Z + joystickRange) / joystickRange;

            Angle += 1 / 100.0f * 1 / 4.0f * (joystickState.RotationZ * ANGLE_MULTIPLIER / maxRotationZ) / (float)MathHelper.TwoPi;
            sideAcceleration = joystickState.X * MAX_SIDE_ACCELERATION / joystickRange;
        }

    }
}
