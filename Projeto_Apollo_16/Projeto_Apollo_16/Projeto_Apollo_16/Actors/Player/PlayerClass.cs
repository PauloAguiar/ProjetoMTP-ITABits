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
   
        #region constants
        //Keyboard Control
        private const float DELTA_THETA = (float)Math.PI / 400;
        private const float EPSILON_SPEED = 0.1f;
        private const float EPSILON_THROTTLE = 0.0001f;
        private const float DELTA_THROTTLE_UP = 0.0001f;
        private const float DELTA_THROTTLE_DOWN = 0.000005f;
        
        
        //Joystick Control
        private const float MAX_SPEED = 2.0f;
        private const float MIN_SPEED = -0.75f;
        private const float CAMERA_PROPORTION = (MAX_CAMERA_ZOOM - MIN_CAMERA_ZOOM) / MAX_MAX_THROTTLE;
        private const float MAX_SIDE_ACCELERATION = 0.007f;
        private const float MAX_SIDE_SPEED = 0.03f;
        private const float MAX_MAX_THROTTLE = 0.2f;
        private const float MIN_MIN_THROTTLE = -0.1f;

        const float MAX_ANGLE = (float)MathHelper.PiOver2;
        private const float PLAYER_AXIS_RANGE = 1000;

        const float maxAngle = (float)MathHelper.PiOver4;
        #endregion

        public float throttle { get; private set; }
        public float Speed { get; private set; }
        public float Angle { get; private set; }
        public Vector2 Velocity { get; private set; }

        float sideAcceleration;
        public Vector2 SideVelocity { get; private set; }
        public float SideSpeed { get; private set; }

        //dependem do throttle (alavanca do joystick)
        private float maxThrottle = 0.4f;
        private float minThrottle = -0.2f;

        //dpende do joystickRange
        float maxRotationZ;
        
        Texture2D naveRight;
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
            Velocity = Vector2.Zero;
            initializeStats();
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
        public override void Update(GameTime gameTime)
        {

        }

        public void Update(GameTime gameTime, JoystickState joystickState, int joystickRange)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            UpdateJoystick(joystickState, joystickRange);
            UpdateInput(gameTime);
            UpdatePosition(dt);
            UpdateInventory();
            UpdateStats(dt);

        }

        private void UpdatePosition(double dt)
        {
            throttle = MathHelper.Clamp(throttle, minThrottle, maxThrottle);
            Speed += throttle;
            Speed = MathHelper.Clamp(Speed, MIN_SPEED, MAX_SPEED);

            Velocity = MathFunctions.AngleToVector(Angle);

            Velocity.Normalize();
            SideVelocity = new Vector2(-Velocity.Y, Velocity.X);
            
            turboPosition =  - texture.Height/2*Velocity;
            turboBackPosition =  texture.Height / 2 * Velocity;

            gunPosition = 0 * Velocity;

            turboAngle = Angle + MathHelper.PiOver2;

            sideAcceleration *= Math.Abs(throttle);
            
            sideAcceleration = MathHelper.Clamp(sideAcceleration, -MAX_SIDE_ACCELERATION, MAX_SIDE_ACCELERATION);
            SideSpeed += sideAcceleration * (float)dt;
            SideSpeed = MathHelper.Clamp(SideSpeed, -MAX_SIDE_SPEED, MAX_SIDE_SPEED);
            SideVelocity *= SideSpeed * (float)dt +0.5f * sideAcceleration * (float)(dt * dt);
            SideSpeed /= 2.0f;

            Velocity = Velocity * Speed;
            
            globalPosition += Velocity * (float)dt;
            globalPosition += SideVelocity * (float)dt;
            turboPosition += globalPosition;
            turboBackPosition += globalPosition;
            gunPosition += globalPosition;
        }
        
        private void UpdateInput(GameTime gameTime)
        {
            UpdateCameraInput();
            UpdatePositionInput();
            UpdateInventoryInput();
        }

        private void UpdatePositionInput()
        {
            maxThrottle = MAX_MAX_THROTTLE * (-joystickState.Z + joystickRange) / joystickRange;
            minThrottle = MIN_MIN_THROTTLE * (-joystickState.Z + joystickRange) / joystickRange;

            velocityInput();
            angleInput();

            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.R))
            {
                globalPosition = new Vector2(0);
            }


            sideAcceleration = joystickState.X * MAX_SIDE_ACCELERATION / joystickRange;

        }

        private void angleInput()
        {
            Angle += 1/100.0f * 1/4.0f*(joystickState.RotationZ * MAX_ANGLE / maxRotationZ)/(float)MathHelper.TwoPi;

            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Left))
            {
                Angle -= DELTA_THETA;
                if (Angle < -MathHelper.TwoPi)
                {
                    Angle += MathHelper.TwoPi;
                }
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Right))
            {
                Angle += DELTA_THETA;
                if (Angle > MathHelper.TwoPi)
                {
                    Angle -= MathHelper.TwoPi;
                }

            }
            
        }

        private void velocityInput()
        {
            
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Up))
            {
                throttle += DELTA_THROTTLE_UP;
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Down))
            {
                throttle -= DELTA_THROTTLE_DOWN;
            }
            else
            {
                throttle *= 1.0f / 2;
                if (Math.Abs(throttle) <= EPSILON_THROTTLE)
                {
                    throttle = 0;
                }
            }
            
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
