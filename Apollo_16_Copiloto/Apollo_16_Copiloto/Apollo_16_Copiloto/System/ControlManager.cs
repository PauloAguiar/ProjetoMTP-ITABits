using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Apollo_16_Copiloto
{
    /* The class inherits from List<T> so its easier to add and remove controls to the manager and handle those controls.
     * Author: Paulo Henrique
     * Created: 23/01/2013
     */
    public class ControlManager : List<Control>
    {
        /* Fields */
        int selectedControl = 0;
        static SpriteFont spriteFont;
        public event EventHandler FocusChanged;

        /* Getters and Setters */
        public static SpriteFont SpriteFont
        {
            get { return spriteFont; }
        }

        /* Constructor */
        public ControlManager(SpriteFont spriteFont)
            : base()
        {
            ControlManager.spriteFont = spriteFont;
        }

        public ControlManager(SpriteFont spriteFont, int capacity)
            : base(capacity)
        {
            ControlManager.spriteFont = spriteFont;
        }

        public ControlManager(SpriteFont spriteFont, IEnumerable<Control> collection) :
            base(collection)
        {
            ControlManager.spriteFont = spriteFont;
        }

        /* Methods */
        public void Update(GameTime gameTime)
        {
            if (Count == 0)
                return;

            foreach (Control c in this)
            {
                if (c.Enabled)
                    c.Update(gameTime);

                if (c.HasFocus)
                    c.HandleInput();
            }

            if (InputHandler.KeyPressed(Keys.Up))
            {
                PreviousControl();
            }

            if (InputHandler.KeyPressed(Keys.Down))
            {
                NextControl();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Control c in this)
            {
                if (c.Visible)
                    c.Draw(spriteBatch);
            }
        }

        public void NextControl()
        {
            if (Count == 0)
                return;

            int currentControl = selectedControl;

            this[selectedControl].HasFocus = false;

            do
            {
                selectedControl++;

                if (selectedControl == Count)
                    selectedControl = 0;

                if (this[selectedControl].TabStop && this[selectedControl].Enabled)
                {
                    if (FocusChanged != null)
                    {
                        FocusChanged(this[selectedControl], null);
                    }
                    break;
                }
            } while (currentControl != selectedControl);

            this[selectedControl].HasFocus = true;
        }

        public void PreviousControl()
        {
            if (Count == 0)
                return;

            int currentControl = selectedControl;

            this[selectedControl].HasFocus = false;

            do
            {
                selectedControl--;

                if (selectedControl < 0)
                    selectedControl = Count - 1;

                if (this[selectedControl].TabStop && this[selectedControl].Enabled)
                {
                    if (FocusChanged != null)
                    {
                        FocusChanged(this[selectedControl], null);
                    }
                    break;
                }

            } while (currentControl != selectedControl);

            this[selectedControl].HasFocus = true;
        }
    }
}
