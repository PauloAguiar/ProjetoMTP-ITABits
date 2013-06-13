using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public sealed class PlayerClass : ActorClass
    {
        #region constants
        private const float deltaTheta = (float)Math.PI / 100;
        private const float maxSpeed = 4.0f;
        private const float minSpeed = -1.5f;
        private const float maxThrottle = 0.004f;
        private const float minThrottle = -0.002f;
        private const float deltaThrottleUp = 0.0004f;
        private const float deltaThrottleDown = 0.0001f;

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

        private float cameraZoom;
        private Vector2 cameraOffset;


        public PlayerClass(Vector2 position)
        {
            globalPosition = position;
            
            Speed = 0;
            Angle = 0;
            Velocity = Vector2.Zero;
            cameraZoom = initialCameraZoom;
            cameraOffset = Vector2.Zero;
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

        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            UpdateInput(gameTime);

            UpdatePosition(dt);

        }

        private void UpdatePosition(double dt)
        {
            Velocity = MathFunctions.AngleToVector(Angle);

            throttle = MathHelper.Clamp(throttle, minThrottle, maxThrottle);
            Speed = MathHelper.Clamp(Speed, minSpeed, maxSpeed);

            Speed += throttle;
            Velocity = Velocity * (float)Speed;

            globalPosition += Velocity * (float)dt;
        }
        
        private void UpdateInput(GameTime gameTime)
        {

            UpdateCameraInput();


            UpdatePositionInput();
        }

        private void UpdatePositionInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                throttle += deltaThrottleUp;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                throttle -= deltaThrottleDown;
            }
            else
            {
                throttle = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Angle -= deltaTheta;
                if (Angle < -MathHelper.TwoPi)
                {
                    Angle += MathHelper.TwoPi;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Angle += deltaTheta;
                if (Angle > MathHelper.TwoPi)
                {
                    Angle -= MathHelper.TwoPi;
                }

            }
        }

        private void UpdateCameraInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                SetZoom(1 - Math.Abs((float)Speed));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                ZoomIn(deltaZoom);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                ZoomOut(deltaZoom);
            }


            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                SlideTop(deltaSlide);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                SlideLeft(deltaSlide);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                SlideDown(deltaSlide);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                SlideRight(deltaSlide);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GlobalPosition, texture.Bounds, Color.White, (float)Angle, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
        }

    }
}

