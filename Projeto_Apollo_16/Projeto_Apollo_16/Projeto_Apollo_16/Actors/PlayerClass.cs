using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Input = Microsoft.Xna.Framework.Input;
using SlimDX.DirectInput;

namespace Projeto_Apollo_16
{
    public sealed class PlayerClass : ActorClass
    {
        #region constants
        //Keyboard Control
        private const float deltaTheta = (float)Math.PI / 400;
        private const float epsilonSpeed = 0.1f;
        private const float epsilonThrottle = 0.0001f;
        private const float deltaThrottleUp = 0.0001f;
        private const float deltaThrottleDown = 0.000005f;
        
        //Camera Control
        private const float maxCameraZoom = 1.0f;
        private const float minCameraZoom = 0.1f;
        private const int maxCameraOffset = 300;
        private const float deltaZoom = 0.005f;
        private const float deltaSlide = 4.0f;
        private const float initialCameraZoom = 0.7f;

        //Joystick Control
        private const float maxSpeed = 2.0f;
        private const float minSpeed = -0.75f;
        private float maxThrottle = 0.4f;
        private float minThrottle = -0.2f;
        private const float cameraProportion = (maxCameraZoom - minCameraZoom) / maxMaxThrottle;
        private const float maxSideAcceleration = 0.007f;
        private const float maxSideSpeed = 0.03f;
        private const float maxMaxThrottle = 0.2f;
        private const float minMinThrottle = -0.1f;
        float maxRotationZ;
        const float maxAngle = (float)MathHelper.PiOver4;
        private float cameraZoom;
        private Vector2 cameraOffset;
        #endregion

        private float throttle = 0;
        public float Speed { get; private set; }
        public float Angle { get; private set; }
        public Vector2 Velocity { get; private set; }

        float sideAcceleration;
        public Vector2 SideVelocity { get; private set; }
        public float SideSpeed { get; private set; }

        
        //Items and stuff
        public int Life { get; private set; }
        const int maxLife = 300;
        const int minLife = 0;

        public float Fuel { get; private set; }
        const int maxFuel = 100;
        const int minFuel = 0;


        //items: shield, health
        public int[] inventory = new int[2];
        //0 -> shield
        //1 -> health

        public enum Bullets { linear, circular, homing };
        public Bullets bullets = Bullets.linear;

        JoystickState joystickState = new JoystickState();
        public int joystickRange = 400;

        Texture2D turboTexture;
        Vector2 turboPosition;
        Vector2 turboBackPosition;
        float turboAngle;

        Texture2D naveRight;
        Texture2D gun;
        Vector2 gunPosition;

        public PlayerClass(Vector2 position, ContentManager content)
        {
            globalPosition = position;
            
            Speed = 0;
            Angle = 0;
            Velocity = Vector2.Zero;
            cameraZoom = initialCameraZoom;
            cameraOffset = Vector2.Zero;
            Life = 100;
            Fuel = 100;
            
            this.LoadFont(content);
            this.LoadTexture(content);
        }

        #region cameraControl
        public float Zoom
        {
            get { return cameraZoom; }
        }

        public Vector2 CameraPosition
        {
            get { return cameraOffset; }
        }

        void ZoomIn(float z)
        {
            cameraZoom += z;
            if (cameraZoom > maxCameraZoom) cameraZoom = maxCameraZoom;
        }

        void ZoomOut(float z)
        {
            cameraZoom -= z;
            if (cameraZoom < minCameraZoom) cameraZoom = minCameraZoom;
        }

        void SetZoom(float z)
        {
            cameraZoom = z;
            if (cameraZoom < minCameraZoom) cameraZoom = minCameraZoom;
            else if (cameraZoom > maxCameraZoom) cameraZoom = maxCameraZoom;
        }

        void SlideTop(float a)
        {
            cameraOffset.Y -= a;
            if (cameraOffset.Y < -maxCameraOffset) cameraOffset.Y = -maxCameraOffset;
        }
        void SlideDown(float a)
        {
            cameraOffset.Y += a;
            if (cameraOffset.Y > maxCameraOffset) cameraOffset.Y = maxCameraOffset;
        }
        void SlideLeft(float a)
        {
            cameraOffset.X -= a;
            if (cameraOffset.X < -maxCameraOffset) cameraOffset.X = -maxCameraOffset;
        }
        void SlideRight(float a)
        {
            cameraOffset.X += a;
            if (cameraOffset.X > maxCameraOffset) cameraOffset.X = maxCameraOffset;
        }

        #endregion

        #region loadContent
        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Nave\nave02");
            turboTexture = content.Load<Texture2D>(@"Sprites\Nave\hadouken");
            naveRight = content.Load<Texture2D>(@"Sprites\Nave\naveright");
            gun = content.Load<Texture2D>(@"Sprites\Nave\gun");
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }
        #endregion

        #region update


        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime, JoystickState joystickState, int joystickRange)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            this.joystickState = joystickState;
            this.joystickRange = joystickRange;
            maxRotationZ = joystickRange / 25.0f;

            UpdateInput(gameTime);

            UpdatePosition(dt);

            while (inventory[1] > 0)
            {
                Life += 10;
                inventory[1]--;
            }

            Life = (int)MathHelper.Clamp(Life, minLife, maxLife);

            Fuel -= Math.Abs(throttle) * (float)dt;
            Fuel = MathHelper.Clamp(Fuel, minFuel, maxFuel);

        }

        private void UpdatePosition(double dt)
        {
            throttle = MathHelper.Clamp(throttle, minThrottle, maxThrottle);
            Speed += throttle;
            Speed = MathHelper.Clamp(Speed, minSpeed, maxSpeed);

            Velocity = MathFunctions.AngleToVector(Angle);

            Velocity.Normalize();
            SideVelocity = new Vector2(-Velocity.Y, Velocity.X);
            
            turboPosition =  - texture.Height/2*Velocity;
            turboBackPosition =  texture.Height / 2 * Velocity;
            gunPosition = 0 * Velocity;

            turboAngle = Angle + MathHelper.PiOver2;

            sideAcceleration *= Math.Abs(throttle);
            
            sideAcceleration = MathHelper.Clamp(sideAcceleration, -maxSideAcceleration, maxSideAcceleration);
            SideSpeed += sideAcceleration * (float)dt;
            SideSpeed = MathHelper.Clamp(SideSpeed, -maxSideSpeed, maxSideSpeed);
            SideVelocity *= SideSpeed * (float)dt +0.5f * sideAcceleration * (float)(dt * dt);
            SideSpeed /= 2.0f;

            Velocity = Velocity * Speed;
            /*if (Math.Abs(Speed) <= epsilonSpeed)
            {
                Velocity = Vector2.Zero;
            }*/
            
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

            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.D0) && inventory[0] > 0)
            {
                Life += 40;
                inventory[0]--;
            }
        }

        private void UpdatePositionInput()
        {
            maxThrottle = maxMaxThrottle * (-joystickState.Z + joystickRange) / joystickRange;
            minThrottle = minMinThrottle * (-joystickState.Z + joystickRange) / joystickRange;


            velocityInput();
            angleInput();

            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.R))
            {
                globalPosition = new Vector2(0);
            }


            sideAcceleration = joystickState.X * maxSideAcceleration / joystickRange;

        }

        private void angleInput()
        {
            
            Angle += 1/100.0f * 1/4.0f*(joystickState.RotationZ * maxAngle / maxRotationZ)/(float)MathHelper.TwoPi;


            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Left))
            {
                Angle -= deltaTheta;
                if (Angle < -MathHelper.TwoPi)
                {
                    Angle += MathHelper.TwoPi;
                }
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Right))
            {
                Angle += deltaTheta;
                if (Angle > MathHelper.TwoPi)
                {
                    Angle -= MathHelper.TwoPi;
                }

            }
            
        }

        private void velocityInput()
        {
            if (joystickState.Y < 0)
            {
                throttle = -joystickState.Y * maxThrottle / joystickRange;
            }
            else if (joystickState.Y > 0)
            {
                throttle = joystickState.Y * minThrottle / joystickRange;
            }

            
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Up))
            {
                throttle += deltaThrottleUp;
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Down))
            {
                throttle -= deltaThrottleDown;
            }
            else
            {
                throttle *= 1.0f / 2;
                if (Math.Abs(throttle) <= epsilonThrottle)
                {
                    throttle = 0;
                }
            }
            
        }

        private void UpdateCameraInput()
        {
            SetZoom(maxCameraZoom - cameraProportion * Math.Abs(throttle));

            
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Q))
            {
                ZoomIn(deltaZoom);
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.E))
            {
                ZoomOut(deltaZoom);
            }

            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.W))
            {
                SlideTop(deltaSlide);
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.A))
            {
                SlideLeft(deltaSlide);
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.S))
            {
                SlideDown(deltaSlide);
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.D))
            {
                SlideRight(deltaSlide);
            }
            
        }
        #endregion

        public override void Draw(SpriteBatch spriteBatch)
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
            
            if (Speed > 0)
            {
                spriteBatch.Draw(turboTexture, turboPosition, null, Color.White, (float)turboAngle, new Vector2(turboTexture.Width / 2, turboTexture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
            }
            else if (Speed < 0)
            {
                spriteBatch.Draw(turboTexture, turboBackPosition, null, Color.White, (float)turboAngle, new Vector2(turboTexture.Width / 2, turboTexture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
            }

            spriteBatch.Draw(gun, gunPosition, null, Color.White, (float)Angle, new Vector2(gun.Width / 2, gun.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER-0.1f);


            spriteBatch.DrawString(spriteFont, "Life = " + Life.ToString(), globalPosition, Color.White);
            spriteBatch.DrawString(spriteFont, "Fuel = " + Fuel.ToString(), globalPosition+new Vector2(0, 50), Color.White);
            
            
        }

    }
}
