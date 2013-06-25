using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Xml.Linq;
using System.IO;
using Microsoft.Xna.Framework.Storage;
using System.Xml;

namespace Apollo_16_Piloto
{
    /* This class will create a title screen for out game.
     * Author: Paulo Henrique
     * Created 21/03/2013
     */
    public class TitleScreen : BaseGameState
    {
        /* Fields */
        Texture2D backgroundTexture;
        PictureBox title;
        PictureBox background;
        Texture2D titleTexture;
        LinkLabel startLabel;

        /* Constructor */
        public TitleScreen(Game game, GameStateManager manager) 
            : base(game, manager)
        {
            
        }

        /* XNA Methods */
        protected override void LoadContent()
        {
            base.LoadContent();
            XML_TitleScreenData titleScreen = content.Load<XML_TitleScreenData>(@"TitleScreen\TitleScreen");

            backgroundTexture = content.Load<Texture2D>(@"Backgrounds\" + titleScreen.background_assetName);
            background = new PictureBox(backgroundTexture, new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height));
            controlManager.Add(background);
            background.SetPosition(new Vector2(titleScreen.background_positionX, titleScreen.background_positionY));

            titleTexture = content.Load<Texture2D>(@"TitleScreen\" + titleScreen.title_assetName);
            title = new PictureBox(titleTexture, new Rectangle(0, 0, titleTexture.Width, titleTexture.Height));
            controlManager.Add(title);
            title.SetPosition(new Vector2(titleScreen.title_positionX, titleScreen.title_positionY));

            startLabel = new LinkLabel();
            startLabel.Position = new Vector2(titleScreen.start_positionX, titleScreen.start_positionY);
            startLabel.Text = titleScreen.start_text;
            startLabel.SelectedColor = new Color(titleScreen.start_color_red, titleScreen.start_color_green, titleScreen.start_color_blue);
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
