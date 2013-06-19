using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Input = Microsoft.Xna.Framework.Input;
using Lidgren.Network;

using SlimDX.DirectInput;

namespace Apollo_16_Piloto
{
    public class NetworkScreen : BaseGameState
    {
        Label titleLabel;
        Label statusLabel;

        Joystick joystick;
        JoystickState joystickState = new JoystickState();
        public const int joystickRange = 10000;


        public PilotClass pilot;
        /* Constructor */
        public NetworkScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            pilot = new PilotClass();
            
        }

        /* XNA Methods */
        public override void Initialize()
        {
            CreateDevice();
            systemRef.networkManager.ConnectToServer();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            pilot.LoadFont(content);

            titleLabel = new Label();
            titleLabel.Position = Vector2.Zero;
            titleLabel.Text = "TELA DE CONEXAO, AGUARDE!";
            titleLabel.Color = Color.Red;
            titleLabel.Size = titleLabel.SpriteFont.MeasureString(titleLabel.Text);
            controlManager.Add(titleLabel);

            statusLabel = new Label();
            statusLabel.Position = Vector2.Zero + 3 * (new Vector2(0.0f, 25.0f));
            statusLabel.Text = "";
            statusLabel.Color = Color.Red;
            statusLabel.Size = statusLabel.SpriteFont.MeasureString(statusLabel.Text);
            controlManager.Add(statusLabel);
        }

        public override void Update(GameTime gameTime)
        {
            systemRef.networkManager.ReadLobbyPackets();
            InputDataClass inputData = new InputDataClass();

            /*
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.K))
            {
                inputData.spaceBar = true;
            }
             */ 

            updateJoystick();
            ReadImmediateData();

            /*
            if (joystickState.IsPressed(0))
            {
                inputData.button0 = true;
            }
             */
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
            inputData.pov = (Byte)(joystickState.GetPointOfViewControllers()[0] * 100 / joystickRange);

            systemRef.networkManager.SendPackets(inputData);
            statusLabel.Text = systemRef.networkManager.status;
            base.Update(gameTime);
        }

        void CreateDevice()
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

        void ReleaseDevice()
        {
            if (joystick != null)
            {
                joystick.Unacquire();
                joystick.Dispose();
            }
            joystick = null;
        }

        private void updateJoystick()
        {
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.L))
            {
                ReleaseDevice();
                CreateDevice();
            }

            ReadImmediateData();
        }

        void ReadImmediateData()
        {
            if (joystick == null)
            {
                return;
            }
            joystickState = joystick.GetCurrentState();
        }



        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin();

            controlManager.Draw(systemRef.spriteBatch);
            pilot.Draw(systemRef.spriteBatch);
            
            systemRef.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
