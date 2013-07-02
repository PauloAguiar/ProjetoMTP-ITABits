using Input = Microsoft.Xna.Framework.Input;
using SlimDX.DirectInput;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public static partial class GameLogic
    {
        static Joystick joystick;
        static JoystickState joystickState = new JoystickState();
        public const int JOYSTICK_RANGE = 10000;

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
                        joystick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-JOYSTICK_RANGE, JOYSTICK_RANGE);
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

        static void UpdateJoystick()
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
            if (joystickState.IsPressed((int)ButtonStates.BTN_6) && timeChangedWeapon >= MIN_TIME_CHANGE_WEAPON)
            {
                ShiftBulletsRight();
            }
            if (joystickState.IsPressed((int)ButtonStates.BTN_5) && timeChangedWeapon >= MIN_TIME_CHANGE_WEAPON)
            {
                ShiftBulletsLeft();
            }
            if (joystickState.IsPressed((int)ButtonStates.BTN_4) && PlayerClass.timeChangedItem >= PlayerClass.MIN_TIME_CHANGE_ITEM)
            {
                PlayerClass.ShiftItemsRight();
            }
            if (joystickState.IsPressed((int)ButtonStates.BTN_3) && PlayerClass.timeChangedItem >= PlayerClass.MIN_TIME_CHANGE_ITEM)
            {
                PlayerClass.ShiftItemsLeft();
            }

            if (joystickState.IsPressed((int)ButtonStates.BTN_1) && projectilesManager.bulletSpawnTime > ProjectileManager.TTS)
            {
                CreateBullets();
            }

            if (joystickState.IsPressed((int)ButtonStates.BTN_7) && PlayerClass.timeUsedItem > PlayerClass.MIN_TIME_USE_ITEM)
            {
                player.UseItem();
            }
        }

    }
}
