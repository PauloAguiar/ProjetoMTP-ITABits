using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public sealed class Ghost : EnemyClass
    {
        public double HorizontalSpeed { get; private set; }
        public double VerticalSpeed { get; private set; }
        public Vector2 Velocity { get; set; }
        public Vector2 centralPosition { get; set; }
        private bool isFliped;
        private int horizontalAmplitude = 300;
        private int verticalAmplitude = 300;
        private const float epsilon = 0.0001f;

        public Ghost(Vector2 position, ContentManager content) : base(position, content)
        {
            life = 2;
            globalPosition = position;
            centralPosition = position;

            HorizontalSpeed = GameLogic.rand.Next(4, 20);
            VerticalSpeed = GameLogic.rand.Next(6, 20);

            horizontalAmplitude = GameLogic.rand.Next(300, 1000);
            verticalAmplitude = GameLogic.rand.Next(200, 500);
            if (GameLogic.rand.Next(2) == 0)
            {
                Velocity = new Vector2(1, 0) * (float)HorizontalSpeed;
            }
            else
            {
                Velocity = -new Vector2(1, 0) * (float)HorizontalSpeed;
            }

            if (GameLogic.rand.Next(2) == 0)
            {
                Velocity += new Vector2(0, 1) * (float)VerticalSpeed;
            }
            else
            {
                Velocity += -new Vector2(0, 1) * (float)VerticalSpeed;
            }

            isFliped = true;
        }


        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Enemies\ghost");
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void LoadSound(ContentManager content)
        {
            hitSound = content.Load<SoundEffect>(@"Sounds\hit");
        }

        public override void Destroy(Vector2 playerPosition, Vector2 position, ContentManager content, ExplosionManager explosionManager)
        {
            SimpleExplosion e = new SimpleExplosion(playerPosition, position, content);
            explosionManager.createExplosion(e);
        }

        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;
            globalPosition += Velocity;

            if (globalPosition.X - centralPosition.X >= horizontalAmplitude || globalPosition.X - centralPosition.X <= -horizontalAmplitude)
            {
                globalPosition.X = MathHelper.Clamp(globalPosition.X, centralPosition.X - horizontalAmplitude, centralPosition.X + horizontalAmplitude);
                Velocity = new Vector2(-Velocity.X, Velocity.Y);
                isFliped = !isFliped;
            }

            if (globalPosition.Y - centralPosition.Y >= verticalAmplitude || globalPosition.Y - centralPosition.Y <= -verticalAmplitude)
            {
                globalPosition.Y = MathHelper.Clamp(globalPosition.Y, centralPosition.Y - verticalAmplitude, centralPosition.Y + verticalAmplitude);
                Velocity = new Vector2(Velocity.X, -Velocity.Y);
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isFliped)
            {
                spriteBatch.Draw(texture, globalPosition, texture.Bounds, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.ENEMY_LAYER);
            }
            else
            {
                spriteBatch.Draw(texture, globalPosition, texture.Bounds, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.FlipHorizontally, Globals.ENEMY_LAYER);
            }
            
            
            spriteBatch.DrawString(spriteFont, "Pos:" + globalPosition.ToString(), globalPosition - new Vector2((texture.Width / 2) - 1, (texture.Height / 2) - 1), Color.Red);
        }
    }
}
