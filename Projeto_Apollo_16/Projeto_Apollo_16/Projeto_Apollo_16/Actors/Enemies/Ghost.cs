using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Projeto_Apollo_16
{
    public sealed class Ghost : EnemyClass
    {
        public double Speed { get; private set; }
        public Vector2 Velocity { get; set; }
        public Vector2 centralPosition { get; set; }
        private bool isFliped;
        private const int amplitude = 300;

        public Ghost(Vector2 position, ContentManager content) : base(position, content)
        {
            globalPosition = position;
            centralPosition = position;
            Speed = 3;
            Velocity = new Vector2(1, 0) * (float)Speed;
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

        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;
            globalPosition += Velocity;
        
            if (globalPosition.X >= centralPosition.X + amplitude)
            {
                globalPosition.X = centralPosition.X + amplitude - 1;
                Velocity = -Velocity;
                isFliped = !isFliped;
            }
            if (globalPosition.X <= centralPosition.X - amplitude)
            {
                globalPosition.X = centralPosition.X - amplitude + 1;
                Velocity = -Velocity;
                isFliped = !isFliped;
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
