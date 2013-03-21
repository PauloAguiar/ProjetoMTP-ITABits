using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projeto_Apollo_16
{
    class PlayerClass : ActorClass
    {
        /////////////
        //fields/////
        /////////////
        //private float Angulo;
        private double throttle;
        private double speed;
        private double angle;
        private double dTheta;
        private Vector2 position;
        private Vector2 velocity;
        private Texture2D texture;
        
        //////////
        //ctor////
        /////////
        public PlayerClass(Vector2 posicao, double theta ,Texture2D textura)
        {
            throttle = 0;
            speed = 0;
            position = posicao;
            velocity = Vector2.Zero;
            angle = 0;
            dTheta = theta;
            texture = textura;
        }

        ////////////
        //methods///
        ///////////
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        public double Angle
        {
            get { return angle; }
            set { angle = value; }
        }


        public void Update(GameTime gameTime)
        {
            
            if(Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                throttle += 0.02;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                throttle -= 0.05;
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
            
            
            if (throttle < -0.015)
            {
                throttle = -0.015;
            }
            else if (throttle > 0.1)
            {
                throttle = 0.1;
            }

            if (speed > 100)
            {
                speed = 100;
            }
            else if (speed < -40)
            {
                speed = -40;
            }

            speed += throttle;
            position += velocity * (float)speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, (float)angle, new Vector2(texture.Width / 2, (float)texture.Height / 2), (float)1, SpriteEffects.None, (float)0); 
        }


    }
}
