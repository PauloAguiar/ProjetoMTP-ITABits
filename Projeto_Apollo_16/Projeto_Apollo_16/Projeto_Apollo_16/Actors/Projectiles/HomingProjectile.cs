using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


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
            ttl = 1000; //ttl milisegundos de vida
            globalPosition = initialPosition;
            this.enemy = enemy;
        }

        public override void  Update(GameTime gameTime)
        {
            float dt = (float)(gameTime.ElapsedGameTime.TotalMilliseconds);

            velocity = enemy.GlobalPosition - globalPosition;
            velocity.Normalize();
            velocity *= speed;

            globalPosition += velocity * dt;

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
