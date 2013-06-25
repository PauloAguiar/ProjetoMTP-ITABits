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
        public JoystickState state { get; set; }
        const float MAX_ROTATION_Z = Globals.JOYSTICK_RANGE / 25.0f;

        void UpdateJoystick(JoystickState joystickState)
        {
            this.joystickState = joystickState;
        }

        void UpdateJoystickInput()
        {
            if (joystickState.Y < 0)
            {
                throttle = -joystickState.Y * maxThrottle / Globals.JOYSTICK_RANGE;
            }
            else if (joystickState.Y > 0)
            {
                throttle = joystickState.Y * minThrottle / Globals.JOYSTICK_RANGE;
            }

            maxThrottle = MAX_MAX_THROTTLE * (-joystickState.Z + Globals.JOYSTICK_RANGE) / Globals.JOYSTICK_RANGE;
            minThrottle = MIN_MIN_THROTTLE * (-joystickState.Z + Globals.JOYSTICK_RANGE) / Globals.JOYSTICK_RANGE;

            Angle += ANGLE_MULTIPLIER * joystickState.RotationZ / MAX_ROTATION_Z;
            sideAcceleration = joystickState.X * MAX_SIDE_ACCELERATION / Globals.JOYSTICK_RANGE;
        }

    }
}
