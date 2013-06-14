using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public class CircularProjectile : ProjectileClass
    {
        private const float speed = 2f;
        private const int radius = 400;
        private Vector2 centralPosition;
        private Vector2 velocity;
        PlayerClass player;

        public CircularProjectile(Vector2 initialPosition, ContentManager content, PlayerClass player)
            : base(initialPosition, content)
        {
            ttl = 4000;
            globalPosition = initialPosition + new Vector2(radius, 0);
            centralPosition = initialPosition;
            this.player = player;
        }

        public override void  Update(GameTime gameTime)
        {
 	        //movimento circular
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            centralPosition = player.GlobalPosition;
            Vector2 r = globalPosition - centralPosition;

            Vector2 v = new Vector2(-r.Y, r.X);

            v.Normalize();
            velocity = v;
            v *= speed;

            globalPosition += v * (float)dt;

            r = globalPosition - centralPosition;
            r.Normalize();
            r *= radius;

            globalPosition = centralPosition + r;
            globalPosition += player.Velocity * (float)dt;

        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GlobalPosition, texture.Bounds, Color.White, (float)-Math.Atan2(velocity.X, velocity.Y)-(float)Math.PI/2, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);

        }
    }
}
