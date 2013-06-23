using Input = Microsoft.Xna.Framework.Input;
using SlimDX.DirectInput;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public static partial class GameLogic
    {
        static Joystick joystick;
        static JoystickState joystickState = new JoystickState();
        public const int joystickRange = 10000;

        static void CreateDevice()
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
                        joystick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-joystickRange, joystickRange);
                }

                joystick.Acquire();
            }

        }

        static void ReleaseDevice()
        {
            if (joystick != null)
            {
                joystick.Unacquire();
                joystick.Dispose();
            }
            joystick = null;
        }

        static void updateJoystick()
        {
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.L))
            {
                ReleaseDevice();
                CreateDevice();
            }

            ReadImmediateData();
        }

        static void ReadImmediateData()
        {
            if (joystick == null)
            {
                return;
            }
            joystickState = joystick.GetCurrentState();

            checkJoystickStatus();
        }

        static void checkJoystickStatus()
        {
            if (((Input.Keyboard.GetState().IsKeyDown(Input.Keys.F2)) || joystickState.IsPressed(5)) && timeChangedWeapon > minTimeChangeWeapon)
            {
                shiftBulletsRight();
            }
            if (((Input.Keyboard.GetState().IsKeyDown(Input.Keys.F1)) || joystickState.IsPressed(4)) && timeChangedWeapon > minTimeChangeWeapon)
            {
                shiftBulletsLeft();
            }
            if (((Input.Keyboard.GetState().IsKeyDown(Input.Keys.Space)) || joystickState.IsPressed(0)) && projectilesManager.bulletSpawnTime > ProjectileManager.tts)
            {
                createBullets();
            }

            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.D1))
            {
                createItem1();
            }
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.D2))
            {
                createItem2();
            }
        }
    }
}
