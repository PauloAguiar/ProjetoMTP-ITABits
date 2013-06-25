using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    class AreaProjectile : ProjectileClass
    {
        private const float SPEED = 2 / 3.0f;
        private const float ACCELERATION_MODULE = 0.01f;
        private Vector2 velocity;
        private Vector2 velocityAux;
        private Vector2 acceleration;
        private bool shooted = false;
        private float rot = 0;

        public AreaProjectile(Vector2 initialPosition, Vector2 velocity, ContentManager content)
            : base(initialPosition, content)
        {
            this.velocity = velocity;
            velocity.Normalize();
            this.acceleration = velocity;
            ttl = 2000;

            velocity *= SPEED;
            acceleration *= ACCELERATION_MODULE;
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Shoots\bullet");

        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }


        public override void LoadSound(ContentManager content)
        {
            sound = content.Load<SoundEffect>(@"Sounds\Pow");
        }


        public override void Update(GameTime gameTime)
        {
            float dt = (float)(gameTime.ElapsedGameTime.TotalMilliseconds);

            if (!shooted)
            {
                sound.Play();
                shooted = true;
            }
                velocityAux = velocity;
                velocity.X = (float)Math.Cos(rot*Math.PI/2) * velocityAux.X - (float)Math.Sin(rot*Math.PI/2) * velocityAux.Y ;
                velocity.Y = (float)Math.Sin(rot * Math.PI / 2) * velocityAux.X + (float)Math.Cos(rot * Math.PI / 2) * velocityAux.Y;             
                globalPosition += velocity * dt + 0.5f * dt * dt * acceleration;
                velocity += acceleration * dt;

        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(texture, GlobalPosition, texture.Bounds, Color.White, (float)-Math.Atan2(velocity.X, velocity.Y) - rot * (float)Math.PI / 2, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
                rot += 0.5f;
        }
    }
}  

