using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public class HomingProjectile : ProjectileClass
    {
        private const float speed = 2f;
        private Vector2 velocity;
        EnemyClass enemy;

        public HomingProjectile(Vector2 initialPosition, ContentManager content, EnemyClass enemy)
            : base(initialPosition, content)
        {
            ttl = 1000;
            globalPosition = initialPosition;
            this.enemy = enemy;
        }
        
        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Shoots\bullet");
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void  Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            velocity = enemy.GlobalPosition - globalPosition;
            velocity.Normalize();
            velocity *= speed;

            globalPosition += velocity * (float)dt;

            if (enemy == null)
            {
                IsActive = false;
            }
            
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GlobalPosition, texture.Bounds, Color.White, (float)-Math.Atan2(velocity.X, velocity.Y)-(float)Math.PI/2, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);

        }
    }
}
