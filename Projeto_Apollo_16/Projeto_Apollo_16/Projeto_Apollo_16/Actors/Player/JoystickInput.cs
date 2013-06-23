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

        void UpdateJoystick(JoystickState joystickState, int joystickRange)
        {
            this.joystickState = joystickState;
            this.joystickRange = joystickRange;
            maxRotationZ = joystickRange / 25.0f;
            checkJoystickInput();
        }

        void checkJoystickInput()
        {

        }
    }
}
