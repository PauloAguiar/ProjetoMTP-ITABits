using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    class LinearProjectile : ProjectileClass
    {
        private Vector2 velocity;
        private Vector2 acceleration;
        bool shooted = false;

        public LinearProjectile(Vector2 initialPosition, Vector2 velocity, Vector2 acceleration, ContentManager content)
            : base(initialPosition, content)
        {
            this.velocity = velocity;
            this.acceleration = acceleration;
            ttl = 6000; //ttl milisegundos de vida
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"bullet");

        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void LoadSound(ContentManager content)
        {
            sounds.Add(content.Load<SoundEffect>(@"Sounds/Pow"));
        }

        public override void  Update(GameTime gameTime)
        {
 	        float dt = (float)(gameTime.ElapsedGameTime.TotalMilliseconds);

            globalPosition.X += (velocity.X * dt) +(0.5f * dt * dt * acceleration.X);
            globalPosition.Y += (velocity.Y * dt) +(0.5f * dt * dt * acceleration.Y);

            velocity.X += acceleration.X * dt;
            velocity.Y += acceleration.Y * dt;
            
            //reproduzir o soundEffect do disparo apenas 1 vez
            if (!shooted) 
            {
                sounds.Last().Play();
                shooted = true;
            }
            
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GlobalPosition, texture.Bounds, Color.White, (float)-Math.Atan2(velocity.X, velocity.Y)-(float)Math.PI/2, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);

            //buga se a velocidade for 0
            //depois que mudei o tiro de acordo com o angle buga mais.
            //spriteBatch.DrawString(spriteFont, "Speed: " + moveSpeed.ToString(), globalPosition - new Vector2((texture.Width / 2) - 1, (texture.Height / 2) - 14), Color.Red);
            
        }
    }
}
