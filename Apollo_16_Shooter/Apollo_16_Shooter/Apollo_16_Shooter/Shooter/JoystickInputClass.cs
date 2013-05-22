using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimDX.DirectInput;
using Input = Microsoft.Xna.Framework.Input;

namespace Apollo_16_Shooter
{
    public class JoystickInputClass
    {
        public JoystickState joystickState;
        Joystick joystick;

        public JoystickInputClass()
        {
            joystickState = new JoystickState();
        }

        public void CreateDevice()
        {
            DirectInput dinput = new DirectInput();

            foreach (DeviceInstance device in dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
            {
                try
                {
                    joystick = new Joystick(dinput, device.InstanceGuid);
                    break;
                }
                catch (DirectInputException)
                {
                }
            }

            if (joystick != null)
            {
                foreach (DeviceObjectInstance deviceObject in joystick.GetObjects())
                {
                    if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                        joystick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-Globals.JOYSTICK_RANGE, Globals.JOYSTICK_RANGE);
                }

                joystick.Acquire();
            }

        }

        void ReleaseDevice()
        {
            if (joystick != null)
            {
                joystick.Unacquire();
                joystick.Dispose();
            }
            joystick = null;
        }

        void ReadImmediateData()
        {
            if (joystick == null)
            {
                return;
            }
            joystickState = joystick.GetCurrentState();
        }

        public InputDataClass Update()
        {
            InputDataClass inputData = new InputDataClass();

            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.J))
            {
                ReleaseDevice();
                CreateDevice();
            }

            ReadImmediateData();

            if (joystick != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (joystickState.IsPressed(i))
                    {
                        inputData.buttons[i] = true;
                    }
                }
                inputData.position[0] = joystickState.X;
                inputData.position[1] = joystickState.Y;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    inputData.buttons[i] = false;
                }
                inputData.position[0] = 0;
                inputData.position[1] = 0;

            }

            return inputData;
        }

    }
}
