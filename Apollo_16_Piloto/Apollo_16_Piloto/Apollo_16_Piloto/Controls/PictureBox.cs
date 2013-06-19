using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Apollo_16_Piloto
{
    public class PictureBox : Control
    {
        /* Fields */
        Texture2D image;
        Rectangle sourceRect;
        Rectangle destRect;

        /* Getters and Setters */
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }

        public Rectangle SourceRectangle
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        public Rectangle DestinationRectangle
        {
            get { return destRect; }
            set { destRect = value; }
        }

        /* Constructors */
        public PictureBox(Texture2D image, Rectangle destination)
        {
            Image = image;
            DestinationRectangle = destination;
            SourceRectangle = new Rectangle(0, 0, image.Width, image.Height);
            Color = Color.White;
        }

        public PictureBox(Texture2D image, Rectangle destination, Rectangle source)
        {
            Image = image;
            DestinationRectangle = destination;
            SourceRectangle = source;
            Color = Color.White;
        }

        /* XNA Methods */
        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, destRect, sourceRect, color);
        }

        public override void HandleInput()
        {
        }

        /* Class Methods */
        public void SetPosition(Vector2 newPosition)
        {
            destRect = new Rectangle((int)newPosition.X, (int)newPosition.Y, sourceRect.Width, sourceRect.Height);
        }
    }
}
