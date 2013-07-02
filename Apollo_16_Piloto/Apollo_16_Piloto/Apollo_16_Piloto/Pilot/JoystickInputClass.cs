using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimDX.DirectInput;
using Input = Microsoft.Xna.Framework.Input;

namespace Apollo_16_Piloto
{
    public class JoystickInputClass
    {
        public JoystickState joystickState;
        Joystick joystick;

        const float MAX_ROTATION_Z = Globals.JOYSTICK_RANGE / 25.0f;

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
                for (int i = 0; i < 7; i++)
                {
                    if (joystickState.IsPressed(i))
                    {
                        inputData.buttons[i] = true;
                    }
                }
                inputData.position[0] = joystickState.X;
                inputData.position[1] = joystickState.Y;
                inputData.position[2] = joystickState.Z;
                inputData.rotationZ = joystickState.RotationZ;
                inputData.pov = (Byte)(joystickState.GetPointOfViewControllers()[0] * 100 / Globals.JOYSTICK_RANGE);
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    inputData.buttons[i] = false;
                }
                inputData.position[0] = 0;
                inputData.position[1] = 0;
                inputData.position[2] = 0;
                inputData.rotationZ = 0;
                inputData.pov = (Byte)(0);
            }

            return inputData;
        }
    }
}
