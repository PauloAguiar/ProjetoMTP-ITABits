using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public sealed class PlayerClass : ActorClass , IMoveable
    {
        // Fields

        private const double dTheta = Math.PI / 600;
        private double throttle = 0;
        public double Speed { get; private set; }
        public double Angle { get; private set; }
        public Vector2 Velocity { get; private set; }
        public Vector2 initialPosition { get; set; }

        private float cameraZoom;
        private Vector2 cameraOffset;

        // Constructor
        public PlayerClass(Vector2 position)
        {
            initialPosition = position;
            globalPosition = position;
            Speed = 0;
            Angle = 0;
            Velocity = Vector2.Zero;
            cameraZoom = 1.0f;
            cameraOffset = Vector2.Zero;
        }

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
            if (cameraZoom > 1.0f) cameraZoom = 1.0f;
        }

        void ZoomOut(float z)
        {
            cameraZoom -= z;
            if (cameraZoom < 0.1f) cameraZoom = 0.1f;
        }

        void SetZoom(float z)
        {
            cameraZoom = z;
            if (cameraZoom < 0.1f) cameraZoom = 0.1f;
            else if (cameraZoom > 1.0f) cameraZoom = 1.0f;
        }

        void SlideTop(float a)
        {
            cameraOffset.Y -= a;
            if (cameraOffset.Y < -100) cameraOffset.Y = -100;
        }
        void SlideDown(float a)
        {
            cameraOffset.Y += a;
            if (cameraOffset.Y > 100) cameraOffset.Y = 100;
        }
        void SlideLeft(float a)
        {
            cameraOffset.X -= a;
            if (cameraOffset.X < -100) cameraOffset.X = -100;
        }
        void SlideRight(float a)
        {
            cameraOffset.X += a;
            if (cameraOffset.X > 100) cameraOffset.X = 100;
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Nave\nave02");
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            UpdateInput(gameTime);
            
            Velocity = MathFunctions.AngleToVector(Angle);

            throttle = MathFunctions.Clamp(throttle, -0.002, 0.004);
            Speed = MathFunctions.Clamp(Speed, -1.5, 4);
            
            Speed += throttle;
            Velocity = Velocity * (float)Speed;
            globalPosition += Velocity * (float)dt;


            //SetZoom(1 - Math.Abs((float)Speed));

        }
        
        private void UpdateInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                createShoot();
            }

            
            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                SetZoom(1 - Math.Abs((float)Speed));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                ZoomIn(0.01f);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                ZoomOut(0.01f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                SlideTop(1.0f);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                SlideLeft(1.0f);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                SlideDown(1.0f);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                SlideRight(1.0f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                throttle += 0.0004;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                throttle -= 0.0001;
            }
            else
            {
                throttle = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Angle -= dTheta;
                if (Angle < MathHelper.TwoPi)
                {
                    Angle += MathHelper.TwoPi;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Angle += dTheta;
                if (Angle > MathHelper.TwoPi)
                {
                    Angle -= MathHelper.TwoPi;
                }

            }
        }

        public void createShoot()
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GlobalPosition, texture.Bounds, Color.White, (float)Angle, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
        }
    }
}

