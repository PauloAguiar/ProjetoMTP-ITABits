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
        private const float PLAYER_AXIS_RANGE = 1;
        public Boolean isLoaded = false;
        private Boolean hasShotPrimaryWeapon = false;

        #region ctor
        public PlayerClass(Vector2 position, ContentManager content)
        {
            globalPosition = position;
            Speed = 0;
            throttle = 0;
            Speed = 0.001f;
            Angle = 0;
            Direction = Vector2.Zero;
            initializeStats();
            initializeCamera();
            this.LoadFont(content);
            this.LoadTexture(content);
            isLoaded = true;
        }
        #endregion

        public Boolean HasShotPrimaryWeapon()
        {
            if (!hasShotPrimaryWeapon)
                return false;

            hasShotPrimaryWeapon = false;
            return true;
        }

        #region loadContent
        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Nave\nave02");
            naveRight = content.Load<Texture2D>(@"Sprites\Nave\naveright");
            LoadTurboTexture(content);
            LoadGunTexture(content);
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }
        #endregion

        #region update
        public override void Update(GameTime gameTime) { } //not implemented

        public void Update(GameTime gameTime, JoystickState joystickState, int joystickRange)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            UpdateJoystick(joystickState, joystickRange);
            UpdateInput(gameTime);
            UpdatePosition(dt);
            UpdateInventory();
            UpdateStats(dt);
        }

        private void UpdateInput(GameTime gameTime)
        {
            //just for debug
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.R))
            {
                globalPosition = new Vector2(0);
            }

            UpdateCameraInput();
            UpdateJoystickInput();
            UpdateInventoryInput();
        }

        public void ParseInput(InputDataClass input)
        {
            const int range = 1000;

            if (input.buttons[(int)ButtonStates.BTN_1])
            {
                PrimaryShot();
            }

            if (input.buttons[(int)ButtonStates.BTN_2])
            {
                SecondaryShot();
            }

            sideAcceleration = ((input.position[(int)StickPosition.X_AXIS] * MAX_SIDE_ACCELERATION) * PLAYER_AXIS_RANGE) / range;

            if (input.position[(int)StickPosition.Y_AXIS] < 0)
            {
                throttle = ((-input.position[(int)StickPosition.Y_AXIS] * maxThrottle) * PLAYER_AXIS_RANGE) / range;
            }
            else if (input.position[(int)StickPosition.Y_AXIS] > 0)
            {
                throttle = ((input.position[(int)StickPosition.Y_AXIS] * minThrottle) * PLAYER_AXIS_RANGE) / range;
            }

            maxThrottle = (MAX_MAX_THROTTLE * (-input.position[(int)StickPosition.Z_AXIS] + range) * PLAYER_AXIS_RANGE) / range;
            minThrottle = (MIN_MIN_THROTTLE * (-input.position[(int)StickPosition.Z_AXIS] + range) * PLAYER_AXIS_RANGE) / range;

            Angle += 1 / 100.0f * 1 / 4.0f * (input.rotationZ * maxAngle / maxRotationZ) / (float)MathHelper.TwoPi;
        }

        private void SecondaryShot()
        {
            throw new NotImplementedException();
        }

        private void PrimaryShot()
        {
            hasShotPrimaryWeapon = true;
        }
        #endregion

        #region draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawShip(spriteBatch);
            DrawTurbo(spriteBatch);
            DrawGun(spriteBatch);
            DrawStats(spriteBatch);
        }

        private void DrawShip(SpriteBatch spriteBatch)
        {
            if (SideSpeed == 0)
            {
                spriteBatch.Draw(texture, GlobalPosition, null, Color.White, (float)Angle, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
            }
            else if (SideSpeed < 0)
            {
                spriteBatch.Draw(naveRight, GlobalPosition, null, Color.White, (float)Angle, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
            }
            else
            {
                spriteBatch.Draw(naveRight, GlobalPosition, null, Color.White, (float)Angle, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.FlipHorizontally, Globals.PLAYER_LAYER);
            }
        }
        #endregion

        
    }
}
