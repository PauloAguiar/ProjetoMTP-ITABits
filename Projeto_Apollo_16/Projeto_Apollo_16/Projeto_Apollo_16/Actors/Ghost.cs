using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Projeto_Apollo_16.Actors
{
    public sealed class Ghost : ActorClass, IMoveable
    {
        public double Speed { get; private set; }
        public double Angle { get; private set; }
        public Vector2 Velocity { get; private set; }
        private Vector2 centralPosition;
        private Vector2 initialPosition;


        public Ghost(Vector2 position)
        {
            globalPosition = position;
            centralPosition = position;
            initialPosition = position;
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
            centralPosition = globalPosition - centralPosition;
            globalPosition.X = (float)MathFunctions.Clamp((double)globalPosition.X, initialPosition.X - 300, initialPosition.X + 100);
            if (globalPosition.X >= initialPosition.X + 100 || globalPosition.X <= initialPosition.X - 300)
            {
                Velocity = -Velocity;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, globalPosition, Color.White);
        }


    }
}
