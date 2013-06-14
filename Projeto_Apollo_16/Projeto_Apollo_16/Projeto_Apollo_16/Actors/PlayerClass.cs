using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Input = Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using SlimDX.DirectInput;

namespace Projeto_Apollo_16
{
    public sealed class PlayerClass : ActorClass
    {
        #region constants
        private const float deltaTheta = (float)Math.PI / 400;
        private const float maxSpeed = 2.0f;
        private const float minSpeed = -0.75f;
        private const float epsilonSpeed = 0.1f;
        private const float maxThrottle = 0.4f;
        private const float minThrottle = -0.2f;
        private const float epsilonThrottle = 0.0001f;
        private const float deltaThrottleUp = 0.0001f;
        private const float deltaThrottleDown = 0.000005f;

        private const float maxCameraZoom = 1.0f;
        private const float minCameraZoom = 0.1f;
        private const int maxCameraOffset = 300;
        private const float deltaZoom = 0.015f;
        private const float deltaSlide = 4.0f;
        private const float initialCameraZoom = 1.0f;
        #endregion

        private float throttle = 0;
        public float Speed { get; private set; }
        public float Angle { get; private set; }
        public Vector2 Velocity { get; private set; }
        public int life { get; private set; }
        public List<ItemClass> invetary;
        public enum Bullets { linear, circular, homing };
        public Bullets bullets = Bullets.linear;

        private float cameraZoom;
        private Vector2 cameraOffset;


        public PlayerClass(Vector2 position, ContentManager content)
        {
            globalPosition = position;
            
            Speed = 0;
            Angle = 0;
            Velocity = Vector2.Zero;
            cameraZoom = initialCameraZoom;
            cameraOffset = Vector2.Zero;
            life = 100;
            invetary = new List<ItemClass>();

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

        public void Update(GameTime gameTime, JoystickState state)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            UpdateInput(gameTime, state);

            UpdatePosition(dt);

        }

        private void UpdatePosition(double dt)
        {
            throttle = MathHelper.Clamp(throttle, minThrottle, maxThrottle);
            Speed += throttle;
            Speed = MathHelper.Clamp(Speed, minSpeed, maxSpeed);
            
            Velocity = MathFunctions.AngleToVector(Angle);
            Velocity.Normalize();
            Velocity = Velocity * Speed;
            if (Math.Abs(Speed) <= epsilonSpeed)
            {
                Velocity = Vector2.Zero;
            }
            

            globalPosition += Velocity * (float)dt;
        }
        
        private void UpdateInput(GameTime gameTime, JoystickState state)
        {
            UpdateCameraInput(state);

            UpdatePositionInput(state);
        }

        private void UpdatePositionInput(JoystickState state)
        {
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Up) || state.Y < -100)
            {
                throttle += deltaThrottleUp;
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Down) || state.Y > 100)
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

            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Left) || state.X < -200)
            {
                Angle -= deltaTheta;
                if (Angle < -MathHelper.TwoPi)
                {
                    Angle += MathHelper.TwoPi;
                }
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Right) || state.X > 200)
            {
                Angle += deltaTheta;
                if (Angle > MathHelper.TwoPi)
                {
                    Angle -= MathHelper.TwoPi;
                }

            }
        }

        private void UpdateCameraInput(JoystickState state)
        {
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.T))
            {
                SetZoom(1 - Math.Abs((float)Speed));
            }

            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Q) || state.IsPressed(2))
            {
                ZoomIn(deltaZoom);
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.E) || state.IsPressed(3))
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
            spriteBatch.Draw(texture, GlobalPosition, texture.Bounds, Color.White, (float)Angle, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
            spriteBatch.DrawString(spriteFont, "Life = "+life.ToString(), globalPosition, Color.White);
        }

    }
}