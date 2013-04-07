using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16.Actors
{
    public sealed class PlayerClass : ActorClass , IMoveable
    {
        // Fields
        private const double dTheta = Math.PI / 600;
        private double throttle = 0;
        public double Speed { get; private set; }
        public double Angle { get; private set; }
        public Vector2 Velocity { get; private set; }

        // Constructor
        public PlayerClass(Vector2 position)
        {
            globalPosition = position;
            Speed = 0;
            Angle = 0;
            Velocity = Vector2.Zero;
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Nave\nave");
        }
        
        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            UpdateInput(gameTime);
            
            Velocity = MathFunctions.AngleToVector(Angle);
            //Velocity = MathFunctions::AngleToVector(Angle);

            throttle = MathFunctions.Clamp(throttle, -0.002, 0.004);
            Speed = MathFunctions.Clamp(Speed, -1.5, 4);
            
            Speed += throttle;
            Velocity = Velocity * (float)Speed;
            globalPosition += Velocity * (float)dt;
        }
        
        private void UpdateInput(GameTime gameTime)
        {
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


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(400, 400), null, Color.White, (float)Angle, new Vector2(texture.Width / 2, texture.Height / 2), 1, SpriteEffects.None, 0); 
        }


    }
}
