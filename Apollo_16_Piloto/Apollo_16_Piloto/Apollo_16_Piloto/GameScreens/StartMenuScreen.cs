using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DataContent;

namespace Apollo_16_Piloto
{
    public class StartMenuScreen : BaseGameState
    {
        /* Constructor */
        Texture2D backgroundTexture;
        PictureBox background;
        PictureBox arrowImage;
        LinkLabel startGame;
        LinkLabel exitGame;

        float maxItemWidth = 0f;

        public StartMenuScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        /* XNA Methods */
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            XML_StartMenuScreenData startMenuScreen = content.Load<XML_StartMenuScreenData>(@"StartMenuScreen\StartMenuScreen");

            backgroundTexture = content.Load<Texture2D>(@"Backgrounds\" + startMenuScreen.background_assetName);
            background = new PictureBox(backgroundTexture, new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height));
            controlManager.Add(background);
            background.SetPosition(new Vector2(startMenuScreen.background_positionX, startMenuScreen.background_positionY));

            Texture2D arrowTexture = content.Load<Texture2D>(@"Menus\Icons\icon_menu");

            arrowImage = new PictureBox(arrowTexture, new Rectangle(0, 0, arrowTexture.Width, arrowTexture.Height));
            controlManager.Add(arrowImage);

            startGame = new LinkLabel();
            startGame.Text = "Start PILOTO";
            startGame.Size = startGame.SpriteFont.MeasureString(startGame.Text);
            startGame.Selected += new EventHandler(menuItem_Selected);

            controlManager.Add(startGame);

            exitGame = new LinkLabel();
            exitGame.Text = "Exit";
            exitGame.Size = exitGame.SpriteFont.MeasureString(exitGame.Text);
            exitGame.Selected += menuItem_Selected;

            controlManager.Add(exitGame);

            controlManager.NextControl();

            controlManager.FocusChanged += new EventHandler(ControlManager_FocusChanged);

            Vector2 position = new Vector2(180, 400);
            foreach (Control c in controlManager)
            {
                if (c is LinkLabel)
                {
                    if (c.Size.X > maxItemWidth)
                        maxItemWidth = c.Size.X;

                    c.Position = position;
                    position.Y += c.Size.Y + 5f;
                }
            }

            ControlManager_FocusChanged(startGame, null);
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

        /* Event Methods */
        void ControlManager_FocusChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Vector2 position = new Vector2(control.Position.X + maxItemWidth + 10f, control.Position.Y);
            arrowImage.SetPosition(position);
        }

        private void menuItem_Selected(object sender, EventArgs e)
        {
            if (sender == startGame)
            {
                stateManager.PushState(systemRef.networkScreen);
            }

            if (sender == exitGame)
            {
                systemRef.Exit();
            }
        }
    }
}
