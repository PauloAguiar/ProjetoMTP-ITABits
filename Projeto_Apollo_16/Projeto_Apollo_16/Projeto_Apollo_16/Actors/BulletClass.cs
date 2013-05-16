using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public sealed class BulletClass : ActorClass
    {
        private Vector2 targetPosition;
        private bool isActive;
        private Vector2 moveSpeed;
        private Vector2 moveAcceleration;
        private int lifeTime;

        public bool IsActive
        {
            get { return isActive; } 
        }

        public BulletClass()
        {
            isActive = false;
        }

        public BulletClass(Vector2 initialPosition, Vector2 speed)
        {
            globalPosition = initialPosition;
            moveSpeed = speed;
        }

        public void Activate()
        {
            isActive = true;
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"bullet");
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)(gameTime.ElapsedGameTime.TotalMilliseconds);

            globalPosition.X += (moveSpeed.X * dt) + (0.5f * dt * dt * moveAcceleration.X);
            globalPosition.Y += (moveSpeed.Y * dt) + (0.5f * dt * dt * moveAcceleration.Y);

            moveSpeed.X += moveAcceleration.X * dt;
            moveSpeed.Y += moveAcceleration.Y * dt;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GlobalPosition, texture.Bounds, Color.White, (float)Math.Atan2(moveSpeed.Y, moveSpeed.X), new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
            spriteBatch.DrawString(spriteFont, "Pos:" + globalPosition.ToString(), globalPosition - new Vector2((texture.Width / 2) - 1, (texture.Height / 2) - 1), Color.Red);
            spriteBatch.DrawString(spriteFont, "Speed:" + moveSpeed.ToString(), globalPosition - new Vector2((texture.Width / 2) - 1, (texture.Height / 2) - 14), Color.Red);
        }
    }
}
