using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    class LinearProjectile : ProjectileClass
    {
        private const float speed = 2 / 3.0f;
        private const float accelerationModule = 0.01f;
        private Vector2 velocity;
        private Vector2 acceleration;

        public LinearProjectile(Vector2 initialPosition, Vector2 velocity, ContentManager content)
            : base(initialPosition, content)
        {
            this.velocity = velocity;
            velocity.Normalize();
            this.acceleration = velocity;
            ttl = 2000;

            velocity *= speed;
            acceleration *= accelerationModule;
        }
        
        public override void  Update(GameTime gameTime)
        {
 	        float dt = (float)(gameTime.ElapsedGameTime.TotalMilliseconds);

            globalPosition += velocity * dt + 0.5f * dt * dt * acceleration;
            velocity += acceleration * dt;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GlobalPosition, texture.Bounds, Color.White, (float)-Math.Atan2(velocity.X, velocity.Y)-(float)Math.PI/2, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
        }
    }
}
