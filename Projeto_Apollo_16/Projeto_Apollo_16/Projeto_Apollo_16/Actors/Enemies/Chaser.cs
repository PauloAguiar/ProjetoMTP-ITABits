using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public sealed class Chaser : EnemyClass
    {
        public float Speed { get; private set; }
        public Vector2 Velocity { get; set; }
        PlayerClass player;
        int initialDirection;
        const float DELTA = 0.1f;
        float angle;

        public Chaser(Vector2 position, ContentManager content, PlayerClass player)
            : base(position, content)
        {
            globalPosition = position;
            Speed = GameLogic.rand.Next(5, 9) * 0.1f;
            initialDirection = GameLogic.rand.Next(4);
            if (initialDirection == 0)
            {
                Velocity = new Vector2(1, 0) * (float)Speed; 
            }
            else if (initialDirection == 1)
            {
                Velocity = new Vector2(0, 1) * (float)Speed;
            }
            else if (initialDirection == 2)
            {
                Velocity = new Vector2(-1, 0) * (float)Speed;
            }
            else
            {
                Velocity = new Vector2(0, -1) * (float)Speed;
            }

            if (Velocity.Y < 0)
            {
                angle = MathFunctions.VectorToAngle(Velocity);
            }
            else
            {
                angle = MathFunctions.VectorToAngle(Velocity) + (float)Math.PI;
            }

            this.player = player;
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Enemies\enemy03");

        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void Destroy(Vector2 position, ContentManager content, ExplosionManager explosionManager)
        {
            MultipleExplosion e = new MultipleExplosion(position, content);
            explosionManager.createExplosion(e);
        }

        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;
            Vector2 r = (player.GlobalPosition - globalPosition);
            //float d = r.Length();
            r.Normalize();
            //r *= 1 / d;

            Velocity += r * DELTA;
            Velocity.Normalize();
            Velocity *= Speed;
            globalPosition += Velocity * (float)dt;
            if (Velocity.Y < 0)
            {
                angle = MathFunctions.VectorToAngle(Velocity);
            }
            else
            {
                angle = MathFunctions.VectorToAngle(Velocity) + (float)Math.PI;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, globalPosition, texture.Bounds, Color.White, angle, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.ENEMY_LAYER);
            spriteBatch.DrawString(spriteFont, "Pos:" + globalPosition.ToString(), globalPosition - new Vector2((texture.Width / 2) - 1, (texture.Height / 2) - 1), Color.Red);
        }
    }
}