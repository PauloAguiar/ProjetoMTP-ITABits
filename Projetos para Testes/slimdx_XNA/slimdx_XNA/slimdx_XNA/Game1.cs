using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

//novo using
using SlimDX.DirectInput;

namespace slimdx_XNA
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Viewport viewport;
        SpriteFont font;
        Texture2D tex;
        Vector2 position = new Vector2(0);

        //variáveis para ler o input do joystick
        Joystick joystick;
        JoystickState state;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            viewport = GraphicsDevice.Viewport;
            while (joystick == null)
            {
                CreateDevice();
            }
            base.Initialize();
        }

        void CreateDevice()
        {
            // make sure that DirectInput has been initialized
            DirectInput dinput = new DirectInput();

            // search for devices
            foreach (DeviceInstance device in dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
            {
                // create the device
                try
                {
                    joystick = new Joystick(dinput, device.InstanceGuid);
                    break;
                }
                catch (DirectInputException)
                {
                }
            }

            int range = 10000;
            if (joystick != null)
            {
                foreach (DeviceObjectInstance deviceObject in joystick.GetObjects())
                {
                    if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                         joystick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-range, range);
                }

                // acquire the device
                joystick.Acquire();
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            tex = Content.Load<Texture2D>("ghost");
        }
    
        void ReadImmediateData()
        {
            if (joystick.Acquire().IsFailure)
                return;

            if (joystick.Poll().IsFailure)
                return;

            state = joystick.GetCurrentState();
        }

        /*
        void ReleaseDevice()
        {
            if (joystick != null)
            {
                joystick.Unacquire();
                joystick.Dispose();
            }
            joystick = null;
        }
        */

        protected override void Update(GameTime gameTime)
        {
            ReadImmediateData();

            Microsoft.Xna.Framework.Input.KeyboardState keyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            if (keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                this.Exit();
            
            position.X = state.X + viewport.Width / 2 - tex.Width / 2;
            position.Y = state.Y + viewport.Height / 2 - tex.Height / 2;
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(tex, position, Color.White);
            
            spriteBatch.DrawString(font, "AccelerationX: " + state.AccelerationX, new Microsoft.Xna.Framework.Vector2(0), Color.White);
            spriteBatch.DrawString(font, "AccelerationY: " + state.AccelerationY, new Microsoft.Xna.Framework.Vector2(0, 20), Color.White);
            spriteBatch.DrawString(font, "AccelerationZ: " + state.AccelerationZ, new Microsoft.Xna.Framework.Vector2(0, 40), Color.White);
            spriteBatch.DrawString(font, "AngularAccelerationX: " + state.AngularAccelerationX, new Microsoft.Xna.Framework.Vector2(0, 60), Color.White);
            spriteBatch.DrawString(font, "AngularAccelerationY: " + state.AngularAccelerationY, new Microsoft.Xna.Framework.Vector2(0, 80), Color.White);
            spriteBatch.DrawString(font, "AngularAccelerationZ: " + state.AngularAccelerationZ, new Microsoft.Xna.Framework.Vector2(0, 100), Color.White);
            spriteBatch.DrawString(font, "AngularVelocityX: " + state.AngularVelocityX, new Microsoft.Xna.Framework.Vector2(0, 120), Color.White);
            spriteBatch.DrawString(font, "AngularVelocityY: " + state.AngularVelocityY, new Microsoft.Xna.Framework.Vector2(0, 140), Color.White);
            spriteBatch.DrawString(font, "AngularVelocityZ: " + state.AngularVelocityZ, new Microsoft.Xna.Framework.Vector2(0, 160), Color.White);
            spriteBatch.DrawString(font, "ForceX: " + state.ForceX, new Microsoft.Xna.Framework.Vector2(0, 180), Color.White);
            spriteBatch.DrawString(font, "ForceY: " + state.ForceY, new Microsoft.Xna.Framework.Vector2(0, 200), Color.White);
            spriteBatch.DrawString(font, "ForceZ: " + state.ForceZ, new Microsoft.Xna.Framework.Vector2(0, 220), Color.White);
            spriteBatch.DrawString(font, "RotationX: " + state.RotationX, new Microsoft.Xna.Framework.Vector2(0, 240), Color.White);
            spriteBatch.DrawString(font, "RotationY: " + state.RotationY, new Microsoft.Xna.Framework.Vector2(0, 260), Color.White);

            spriteBatch.DrawString(font, "RotationZ: " + state.RotationZ/400.0, new Microsoft.Xna.Framework.Vector2(0, 280), Color.White);
            
            spriteBatch.DrawString(font, "TorqueX: " + state.TorqueX, new Microsoft.Xna.Framework.Vector2(0, 300), Color.White);
            spriteBatch.DrawString(font, "TorqueY: " + state.TorqueY, new Microsoft.Xna.Framework.Vector2(0, 320), Color.White);
            spriteBatch.DrawString(font, "TorqueY: " + state.TorqueZ, new Microsoft.Xna.Framework.Vector2(0, 340), Color.White);
            spriteBatch.DrawString(font, "VelocityX: " + state.VelocityX, new Microsoft.Xna.Framework.Vector2(0, 360), Color.White);
            spriteBatch.DrawString(font, "VelocityY: " + state.VelocityY, new Microsoft.Xna.Framework.Vector2(0, 380), Color.White);
            spriteBatch.DrawString(font, "VelocityZ: " + state.VelocityZ, new Microsoft.Xna.Framework.Vector2(0, 400), Color.White);

            spriteBatch.DrawString(font, "X: " + state.X, new Microsoft.Xna.Framework.Vector2(0, 420), Color.White);
            spriteBatch.DrawString(font, "Y: " + state.Y, new Microsoft.Xna.Framework.Vector2(0, 440), Color.White);
            spriteBatch.DrawString(font, "Z: " + state.Z, new Microsoft.Xna.Framework.Vector2(0, 460), Color.White);

            spriteBatch.DrawString(font, "tipo: " + state.GetType().ToString(), new Microsoft.Xna.Framework.Vector2(0, 480), Color.White);

            for (int i = 0; i < state.GetButtons().Length; i++)
            {
                spriteBatch.DrawString(font, "Button + " + (i+1) + ": " + state.GetButtons()[i].ToString(), new Microsoft.Xna.Framework.Vector2(600, 0 +20*i), Color.White);
            }

            for (int i = 0; i < state.GetVelocitySliders().Length; i++)
            {
                spriteBatch.DrawString(font,"VelocitySliders " + i + ": " + state.GetVelocitySliders()[i].ToString(), new Microsoft.Xna.Framework.Vector2(900, 200 + 20 * i), Color.White);
            }

            for (int i = 0; i < state.GetForceSliders().Length; i++)
            {
                spriteBatch.DrawString(font,"ForceSliders " + i + ": " + state.GetForceSliders()[i].ToString(), new Microsoft.Xna.Framework.Vector2(900, 240 + 20 * i), Color.White);
            }

            for (int i = 0; i < state.GetAccelerationSliders().Length; i++)
            {
                spriteBatch.DrawString(font,"AccelerationSliders " + i + ": " + state.GetAccelerationSliders()[i].ToString(), new Microsoft.Xna.Framework.Vector2(900, 280 + 20 * i), Color.White);
            }

            for (int i = 0; i < state.GetPointOfViewControllers().Length; i++)
            {
                spriteBatch.DrawString(font, "PointOfViewControllers " + i + ": " + ((state.GetPointOfViewControllers()[i])/100.0).ToString(), new Microsoft.Xna.Framework.Vector2(900, 320 + 20 * i), Color.White);
            }

            for (int i = 0; i < state.GetSliders().Length; i++)
            {
                spriteBatch.DrawString(font, "Sliders " + i + ": " + state.GetSliders()[i].ToString(), new Microsoft.Xna.Framework.Vector2(900, 400 + 20 * i), Color.White);
            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
