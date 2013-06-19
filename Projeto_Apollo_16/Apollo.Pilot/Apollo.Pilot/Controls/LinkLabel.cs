﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Apollo.Pilot
{
    public class LinkLabel : Control
    {
        /* Fields */
        Color selectedColor = Color.Red;

        /* Getters and Setters */
        public Color SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; }
        }

        /* Constructor */
        public LinkLabel()
        {
            TabStop = true;
            HasFocus = false;
            Position = Vector2.Zero;
        }

        /* XNA Methods */
        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (hasFocus)
                spriteBatch.DrawString(SpriteFont, Text, Position, selectedColor);
            else
                spriteBatch.DrawString(SpriteFont, Text, Position, Color);
        }

        public override void HandleInput()
        {
            if (!HasFocus)
                return;

            if (InputHandler.KeyReleased(Keys.Enter))
                base.OnSelected(null);
        }

    }
}
