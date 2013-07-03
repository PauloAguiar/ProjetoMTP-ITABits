using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Lidgren.Network;

namespace Projeto_Apollo_16
{
    public class InitGameScreen : BaseGameState
    {
        /* Basic Style */
        Color textColor = Color.Black;
        Vector2 titlePosition = new Vector2(200, 0);
        Vector2 clientLabels = new Vector2(650, 550);
        Vector2 menuLabels = new Vector2(400, 400);

        /* Controls */
        Texture2D backgroundTexture;
        PictureBox background;
        Texture2D titleTexture;
        PictureBox title;
        Label serverLbl;
        Label serverStatusLbl;
        Label pilotLbl;
        Label pilotStatusLbl;
        Label radarLbl;
        Label radarStatusLbl;
        Label shooterLbl;
        Label shooterStatusLbl;
        Label copilotLbl;
        Label copilotStatusLbl;
        LinkLabel startServerLnk;
        LinkLabel startGameLnk;
        LinkLabel optionsLnk;
        LinkLabel exitGameLnk;

        public InitGameScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            systemRef = (SystemClass)game;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            /* Background */
            backgroundTexture = content.Load<Texture2D>(@"Backgrounds\nave_amarela_1024x768");
            background = new PictureBox(backgroundTexture, new Rectangle(0, 0, Globals.SCREEN_RESOLUTION_WIDTH, Globals.SCREEN_RESOLUTION_HEIGHT));
            controlManager.Add(background);

            /* Title */
            titleTexture = content.Load<Texture2D>(@"Screens\title");
            title = new PictureBox(titleTexture, new Rectangle(0, 0, titleTexture.Width, titleTexture.Height));
            controlManager.Add(title);
            title.SetPosition(titlePosition);

            /* Connection Label Entries */
            Vector2 position = clientLabels;

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

            pilotLbl = new Label();
            pilotLbl.Text = "Piloto:";
            pilotLbl.Color = textColor;
            pilotLbl.Size = pilotLbl.SpriteFont.MeasureString(pilotLbl.Text);
            pilotStatusLbl = new Label();
            pilotStatusLbl.Text = "Offline";
            pilotStatusLbl.Color = Color.Red;
            pilotStatusLbl.Size = pilotStatusLbl.SpriteFont.MeasureString(pilotStatusLbl.Text);
            pilotLbl.Position = position;
            pilotStatusLbl.Position = position + new Vector2(pilotLbl.Size.X, 0);
            position.Y += pilotLbl.Size.Y;
            controlManager.Add(pilotLbl);
            controlManager.Add(pilotStatusLbl);

            shooterLbl = new Label();
            shooterLbl.Text = "Atirador:";
            shooterLbl.Color = textColor;
            shooterLbl.Size = shooterLbl.SpriteFont.MeasureString(shooterLbl.Text);
            shooterStatusLbl = new Label();
            shooterStatusLbl.Text = "Offline";
            shooterStatusLbl.Color = Color.Red;
            shooterStatusLbl.Size = shooterStatusLbl.SpriteFont.MeasureString(shooterStatusLbl.Text);
            shooterLbl.Position = position;
            shooterStatusLbl.Position = position + new Vector2(shooterLbl.Size.X, 0);
            position.Y += shooterLbl.Size.Y;
            controlManager.Add(shooterLbl);
            controlManager.Add(shooterStatusLbl);

            copilotLbl = new Label();
            copilotLbl.Text = "Co-piloto:";
            copilotLbl.Color = textColor;
            copilotLbl.Size = copilotLbl.SpriteFont.MeasureString(copilotLbl.Text);
            copilotStatusLbl = new Label();
            copilotStatusLbl.Text = "Offline";
            copilotStatusLbl.Color = Color.Red;
            copilotStatusLbl.Size = copilotStatusLbl.SpriteFont.MeasureString(copilotStatusLbl.Text);
            copilotLbl.Position = position;
            copilotStatusLbl.Position = position + new Vector2(copilotLbl.Size.X, 0);
            position.Y += copilotLbl.Size.Y;
            controlManager.Add(copilotLbl);
            controlManager.Add(copilotStatusLbl);

            radarLbl = new Label();
            radarLbl.Text = "Radar:";
            radarLbl.Color = textColor;
            radarLbl.Size = radarLbl.SpriteFont.MeasureString(radarLbl.Text);
            radarStatusLbl = new Label();
            radarStatusLbl.Text = "Offline";
            radarStatusLbl.Color = Color.Red;
            radarStatusLbl.Size = radarStatusLbl.SpriteFont.MeasureString(radarStatusLbl.Text);
            radarLbl.Position = position;
            radarStatusLbl.Position = position + new Vector2(radarLbl.Size.X, 0);
            position.Y += radarLbl.Size.Y;
            controlManager.Add(radarLbl);
            controlManager.Add(radarStatusLbl);

            /* Menu Entries */
            startServerLnk = new LinkLabel();
            startServerLnk.Text = "Start Server";
            startServerLnk.Size = startServerLnk.SpriteFont.MeasureString(startServerLnk.Text);
            startServerLnk.Selected += new EventHandler(menuItem_Selected);
            controlManager.Add(startServerLnk);

            startGameLnk = new LinkLabel();
            startGameLnk.Text = "Start Game";
            startGameLnk.Size = startGameLnk.SpriteFont.MeasureString(startGameLnk.Text);
            startGameLnk.Selected += menuItem_Selected;
            controlManager.Add(startGameLnk);

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

        public override void Update(GameTime gameTime)
        {
            controlManager.Update(gameTime);
            /* Update Server Status */
            #region UpdateServerStatus
            if (systemRef.networkManager.GetStatus() == NetPeerStatus.Running)
            {
                serverStatusLbl.Text = "Running";
                serverStatusLbl.Color = Color.Green;
            }
            else if (systemRef.networkManager.GetStatus() == NetPeerStatus.Starting)
            {
                serverStatusLbl.Text = "Starting";
                serverStatusLbl.Color = Color.Yellow;
            }
            else if (systemRef.networkManager.GetStatus() == NetPeerStatus.ShutdownRequested)
            {
                serverStatusLbl.Text = "Shutting Down";
                serverStatusLbl.Color = Color.Orange;
            }
            else
            {
                serverStatusLbl.Text = "Not Running";
                serverStatusLbl.Color = Color.Red;
            }

            if (systemRef.networkManager.GetConnectionStatudByID(ConnectionID.PILOT) == NetConnectionStatus.Connected)
            {
                pilotStatusLbl.Text = "Online";
                pilotStatusLbl.Color = Color.Green;
            }
            else
            {
                pilotStatusLbl.Text = "Offline";
                pilotStatusLbl.Color = Color.Red;
            }
            #endregion

            #region UpdatePilotStatus
            if (systemRef.networkManager.GetConnectionStatudByID(ConnectionID.PILOT) == NetConnectionStatus.Connected)
            {
                pilotStatusLbl.Text = "Online";
                pilotStatusLbl.Color = Color.Green;
            }
            else
            {
                pilotStatusLbl.Text = "Offline";
                pilotStatusLbl.Color = Color.Red;
            }
            #endregion

            #region UpdateRadarStatus
            if (systemRef.networkManager.GetConnectionStatudByID(ConnectionID.RADAR) == NetConnectionStatus.Connected)
            {
                radarStatusLbl.Text = "Online";
                radarStatusLbl.Color = Color.Green;
            }
            else
            {
                radarStatusLbl.Text = "Offline";
                radarStatusLbl.Color = Color.Red;
            }
            #endregion

            #region UpdateCopilotStatus
            if (systemRef.networkManager.GetConnectionStatudByID(ConnectionID.COPILOT) == NetConnectionStatus.Connected)
            {
                copilotStatusLbl.Text = "Online";
                copilotStatusLbl.Color = Color.Green;
            }
            else
            {
                copilotStatusLbl.Text = "Offline";
                copilotStatusLbl.Color = Color.Red;
            }
            #endregion
            if (systemRef.networkManager.GetStatus() == NetPeerStatus.Running)
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
            if (sender == startServerLnk)
            {
                if (systemRef.networkManager.GetStatus() == NetPeerStatus.NotRunning)
                {
                    systemRef.networkManager.StartServer();
                    startServerLnk.Text = "Stop Server";
                }
                else
                {
                    systemRef.networkManager.StopServer();
                }

            }
            else if (sender == startGameLnk)
            {
                stateManager.ChangeState(systemRef.gamePlayScreen);
            }
            else if (sender == exitGameLnk)
            {
                systemRef.Exit();
            }
        }
    }
}
