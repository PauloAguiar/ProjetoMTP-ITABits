using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    /* This class will create a title screen for out game.
     * Author: Paulo Henrique
     * Created 21/03/2013
     */
    public class TitleScreen : BaseGameState
    {
        /* Fields */
        Texture2D backgroundImage;
        LinkLabel startLabel;

        /* Constructor */
        public TitleScreen(Game game, GameStateManager manager) 
            : base(game, manager)
        {
        }

        /* XNA Methods */
        protected override void LoadContent()
        {
            backgroundImage = content.Load<Texture2D>("Menus\\Backgrounds\\menuBackground");
            base.LoadContent();

            startLabel = new LinkLabel();
            startLabel.Position = new Vector2(180, 400);
            startLabel.Text = "Press ENTER to continue";
            startLabel.Color = Color.White;
            startLabel.TabStop = true;
            startLabel.HasFocus = true;
            startLabel.Selected += new EventHandler(startLabel_Selected);

            controlManager.Add(startLabel);

        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin();

            base.Draw(gameTime);

            systemRef.spriteBatch.Draw(backgroundImage, systemRef.screenRectangle, Color.White);

            controlManager.Draw(systemRef.spriteBatch);

            systemRef.spriteBatch.End();
        }

        /* Class Methods */
        private void startLabel_Selected(object sender, EventArgs e)
        {
            stateManager.PushState(systemRef.startMenuScreen);
        }

    }
}
