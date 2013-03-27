using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public class PlayerClass : ActorClass
    {
        // Fields
        private double throttle;
        private double speed;
        private double angle;
        private double dTheta;
        private Vector2 position;
        private Vector2 velocity;
        private Texture2D texture;
        
        // Constructor
        public PlayerClass(Vector2 posicao)
        {
            throttle = 0;
            speed = 0;
            position = posicao;
            velocity = Vector2.Zero;
            angle = 0;
            dTheta = Math.PI/600;
        }

        //Getters ans Setters
        public Vector2 Position
        {
            get { return position; }
        }
        public Vector2 Velocity
        {
            get { return velocity; }
        }
        public double Angle
        {
            get { return angle; }
        }

        public void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Nave\nave");
        }
        

        public void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalSeconds;
            
            if(Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                throttle += 0.4;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                throttle -= 0.1;
            }
            else
            {
                throttle = 0;
            }
            
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                angle -= dTheta;
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                angle += dTheta; 
            }

            double velocityAngle = (angle - Math.PI / 2);
            velocity = new Vector2((float)Math.Cos(velocityAngle), (float)Math.Sin(velocityAngle));
            
            
            if (throttle < -2)
            {
                throttle = -2;
            }
            else if (throttle > 3)
            {
                throttle = 3;
            }

            if (speed > 4000)
            {
                speed = 4000;
            }
            else if (speed < -1500)
            {
                speed = -1500;
            }

            speed += throttle;
            position += velocity * (float)speed * (float)dt;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(400, 400), null, Color.White, (float)angle, new Vector2(texture.Width / 2, (float)texture.Height / 2), (float)1, SpriteEffects.None, (float)0); 
        }


    }
}
