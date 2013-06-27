using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public sealed class Sun : EnemyClass
    {
        private float speed = 0.7f;
        private Vector2 centralPosition;
        private int radius = 300;

        public Sun(Vector2 position, ContentManager content) : base(position, content)
        {
            centralPosition = position;
            globalPosition = position + new Vector2(radius, 0);
            speed = GameLogic.rand.Next(7, 15)/10.0f;
            radius = GameLogic.rand.Next(200, 400);
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Enemies\enemy02");
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void Destroy(Vector2 position, ContentManager content, ExplosionManager explosionManager)
        {
            AnimatedExplosion e = new AnimatedExplosion(position, content);
            explosionManager.createExplosion(e);
        }

        public override void Update(GameTime gameTime)
        {
            //movimento circular
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            Vector2 r = globalPosition - centralPosition;

            Vector2 v = new Vector2(-r.Y, r.X);

            v.Normalize();

            v *= speed;
            
            globalPosition += v*(float)dt;
            
            r = globalPosition - centralPosition;
            r.Normalize();
            r *= radius;

            globalPosition = centralPosition + r;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, globalPosition, texture.Bounds, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, Globals.ENEMY_LAYER);
            
            spriteBatch.DrawString(spriteFont, "Pos:" + globalPosition.ToString(), globalPosition - new Vector2((texture.Width / 2) - 1, (texture.Height / 2) - 1), Color.Red);
        }
    }
}
