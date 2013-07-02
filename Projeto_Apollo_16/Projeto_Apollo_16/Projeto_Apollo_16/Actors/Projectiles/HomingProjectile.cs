using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Projeto_Apollo_16
{
    public class HomingProjectile : ProjectileClass
    {
        private const float SPEED = 1f;
        private const float DELTA = 0.1f;
        private Vector2 velocity;
        private bool shooted = false;
        EnemyClass enemy;
        Texture2D SmokeTexture;
        List<Vector2> SmokeList = new List<Vector2>();
        public static int ammo = 10;

        public HomingProjectile(Vector2 initialPosition, ContentManager content, EnemyClass enemy, Vector2 direction)
            : base(initialPosition, content)
        {
            ttl = 1500;
            globalPosition = initialPosition;
            velocity = direction * SPEED * 10;
            this.enemy = enemy;
        }
        
        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Shoots\bullet");
            SmokeTexture = content.Load<Texture2D>(@"Sprites\Shoots\smoke2");
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void LoadSound(ContentManager content)
        {
            sound = content.Load<SoundEffect>(@"Sounds\homingBullet");
        }

        public override void  Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;
            Vector2 d = (enemy.GlobalPosition - globalPosition);
            d.Normalize();
            velocity += d*DELTA;
            velocity.Normalize();
            velocity *= SPEED;

            globalPosition += velocity * (float)dt;

            if (enemy == null)
            {
                IsActive = false;
            }

            if (!shooted)
            {
                sound.Play();
                shooted = true;
            }

            CreateSmoke();
            
        }

        private void CreateSmoke()
        {
            
            for (int i = 0; i < 5; i++)
            {
                Vector2 smokePosition = globalPosition;
                smokePosition.X -= GameLogic.rand.Next(8, 20);
                smokePosition.Y -= GameLogic.rand.Next(8, 20);
                SmokeList.Add(smokePosition);
            }
            
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GlobalPosition, texture.Bounds, Color.White, (float)-Math.Atan2(velocity.X, velocity.Y)-(float)Math.PI/2, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);

            DrawSmoke(spriteBatch);
        }

        private void DrawSmoke(SpriteBatch spriteBatch)
        {
            foreach (Vector2 v in SmokeList)
            {
                spriteBatch.Draw(SmokeTexture, v, Color.White);
            }
        }
    }
}
