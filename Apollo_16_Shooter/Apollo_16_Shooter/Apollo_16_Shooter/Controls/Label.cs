using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apollo_16_Shooter
{
    public class Label : Control
    {
        /* Constructor */
        public Label()
        {
            tabStop = false;
        }

        /* Implements Abstract Methods */
        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(SpriteFont, Text, Position, Color);
        }

        public override void HandleInput()
        {
        }

    }
}
