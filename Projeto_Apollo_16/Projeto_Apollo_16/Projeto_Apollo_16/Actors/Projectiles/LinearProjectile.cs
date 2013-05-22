using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projeto_Apollo_16
{
    class LinearProjectile : ProjectileClass
    {
        public LinearProjectile(Vector2 initialPosition, Vector2 speed, Vector2 acceleration)
            : base(initialPosition, speed, acceleration)
        {
        }

        public override void  Update(GameTime gameTime)
        {
 	        float dt = (float)(gameTime.ElapsedGameTime.TotalMilliseconds);

            globalPosition.X += (moveSpeed.X * dt) + (0.5f * dt * dt * moveAcceleration.X);
            globalPosition.Y += (moveSpeed.Y * dt) + (0.5f * dt * dt * moveAcceleration.Y);

            moveSpeed.X += moveAcceleration.X * dt;
            moveSpeed.Y += moveAcceleration.Y * dt;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GlobalPosition, texture.Bounds, Color.White, -(float)Math.Atan2(moveSpeed.Y, moveSpeed.X), new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
            spriteBatch.DrawString(spriteFont, "Pos:" + globalPosition.ToString(), globalPosition - new Vector2((texture.Width / 2) - 1, (texture.Height / 2) - 1), Color.Red);
            spriteBatch.DrawString(spriteFont, "Speed:" + moveSpeed.ToString(), globalPosition - new Vector2((texture.Width / 2) - 1, (texture.Height / 2) - 14), Color.Red);
        }
    }
}
