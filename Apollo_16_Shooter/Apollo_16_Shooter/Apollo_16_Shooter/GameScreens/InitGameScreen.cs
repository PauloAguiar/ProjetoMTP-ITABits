using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Lidgren.Network;

namespace Apollo_16_Shooter
{
    public class InitGameScreen : BaseGameState
    {
        /* Basic Style */
        Color textColor = Color.Black;
        Vector2 titlePosition = new Vector2(100, 0);
        Vector2 serverLabel = new Vector2(680, 700);
        Vector2 menuLabels = new Vector2(400, 400);

        /* Controls */
        Texture2D backgroundTexture;
        PictureBox background;
        Texture2D titleTexture;
        PictureBox title;
        Label serverLbl;
        Label serverStatusLbl;
        LinkLabel connectLnk;
        LinkLabel optionsLnk;
        LinkLabel exitGameLnk;

        /* Other */
        TimeSpan updateServerStatus = TimeSpan.Zero;

        public InitGameScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            systemRef = (SystemClass)game;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            /* Background */
            backgroundTexture = content.Load<Texture2D>(@"Backgrounds\pilot");
            background = new PictureBox(backgroundTexture, new Rectangle(0, 0, Globals.SCREEN_RESOLUTION_WIDTH, Globals.SCREEN_RESOLUTION_HEIGHT));
            controlManager.Add(background);

            /* Title */
            titleTexture = content.Load<Texture2D>(@"Screens\title");
            title = new PictureBox(titleTexture, new Rectangle(0, 0, titleTexture.Width, titleTexture.Height));
            controlManager.Add(title);
            title.SetPosition(titlePosition);

            /* Connection Label Entries */
            Vector2 position = serverLabel;

            serverLbl = new Label();
            serverLbl.Text = "Servidor:";
            serverLbl.Color = textColor;
            serverLbl.Size = serverLbl.SpriteFont.MeasureString(serverLbl.Text);
            serverStatusLbl = new Label();
            serverStatusLbl.Text = "Not Running";
            serverStatusLbl.Color = Color.Red;
            serverStatusLbl.Size = serverStatusLbl.SpriteFont.MeasureString(serverStatusLbl.Text);
            serverLbl.Position = position;
            serverStatusLbl.Position = position + new Vector2(serverLbl.Size.X, 0);
            position.Y += serverLbl.Size.Y;
            controlManager.Add(serverLbl);
            controlManager.Add(serverStatusLbl);

            /* Menu Entries */
            connectLnk = new LinkLabel();
            connectLnk.Text = "Connect to Server";
            connectLnk.Size = connectLnk.SpriteFont.MeasureString(connectLnk.Text);
            connectLnk.Selected += new EventHandler(menuItem_Selected);
            controlManager.Add(connectLnk);

            optionsLnk = new LinkLabel();
            optionsLnk.Text = "Options";
            optionsLnk.Size = optionsLnk.SpriteFont.MeasureString(optionsLnk.Text);
            optionsLnk.Selected += menuItem_Selected;
            controlManager.Add(optionsLnk);

            exitGameLnk = new LinkLabel();
            exitGameLnk.Text = "Exit";
            exitGameLnk.Size = exitGameLnk.SpriteFont.MeasureString(exitGameLnk.Text);
            exitGameLnk.Selected += menuItem_Selected;
            controlManager.Add(exitGameLnk);

            controlManager.NextControl();

            position = menuLabels;
            foreach (Control c in controlManager)
            {
                if (c is LinkLabel)
                {
                    c.Position = position;
                    position.Y += c.Size.Y;
                }
            }
        }
        public override void Initialize()
        {
            updateServerStatus = new TimeSpan(5);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update(gameTime);
            updateServerStatus += gameTime.ElapsedGameTime;
            if (updateServerStatus > TimeSpan.FromSeconds(1) && !systemRef.networkManager.IsOnline())
            {
                systemRef.networkManager.DiscoverServer();
                updateServerStatus = TimeSpan.Zero;
            }

            #region UpdateServerStatus
            if (systemRef.networkManager.IsOnline())
            {
                serverStatusLbl.Text = "Online";
                serverStatusLbl.Color = Color.Green;
            }
            else
            {
                serverStatusLbl.Text = "Offline";
                serverStatusLbl.Color = Color.Red;
            }
            #endregion

            systemRef.networkManager.ReadPackets(this);
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
        private void menuItem_Selected(object sender, EventArgs e)
        {
            if (sender == connectLnk)
            {
                //if (systemRef.networkManager.IsOnline())
                //{
                    //systemRef.networkManager.ConnectToServer();
                    stateManager.ChangeState(systemRef.gamePlayScreen);
                //}
            }
            else if (sender == exitGameLnk)
            {
                systemRef.Exit();
            }
        }

    }
}
