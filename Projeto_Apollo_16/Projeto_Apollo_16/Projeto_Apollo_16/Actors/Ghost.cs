using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Projeto_Apollo_16.Actors
{
    public sealed class Ghost : ActorClass, IMoveable
    {
        public double Speed { get; private set; }
        public double Angle { get; private set; }
        public Vector2 Velocity { get; set; }
        public Vector2 centralPosition { get; set; }


        public Ghost(Vector2 position)
        {
            globalPosition = position;
            centralPosition = position;
            Speed = 3;
            Angle = 0;
            Velocity = new Vector2(1, 0) * (float)Speed;
        }


        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\ghost");
        }

        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;
            globalPosition += Velocity;
        
            if (globalPosition.X >= centralPosition.X + 100)
            {
                globalPosition.X = centralPosition.X + 99;
                Velocity = -Velocity;
            }
            if (globalPosition.X <= centralPosition.X - 300)
            {
                globalPosition.X = centralPosition.X - 299;
                Velocity = -Velocity;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, globalPosition, Color.White);5
            spriteBatch.Draw(texture, globalPosition, texture.Bounds, Color.White, (float)Angle, new Vector2(texture.Width / 2, texture.Height / 2), 1, SpriteEffects.None, Globals.PLAYER_LAYER);
        }
    }
}
