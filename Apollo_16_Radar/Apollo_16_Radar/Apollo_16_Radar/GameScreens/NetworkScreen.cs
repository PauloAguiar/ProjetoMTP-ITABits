using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Lidgren.Network;

namespace Apollo_16_Radar
{
    public class NetworkScreen : BaseGameState
    {
        Texture2D backgroundTexture;
        PictureBox background;
        Label titleLabel;
        LinkLabel connect;
        Label statusLabel;

        TimeSpan updateNetworkStatus = TimeSpan.Zero;
        TimeSpan lastUpdateNetworkStatus = TimeSpan.Zero;
        public Boolean networkStatus { get;  set; }

        Boolean isOnline = false;
        float maxItemWidth = 0f;
        
        /* Constructor */
        public NetworkScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            networkStatus = false;
        }

        /* XNA Methods */
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();


            backgroundTexture = content.Load<Texture2D>(@"nave_amarela_1024x768");
            background = new PictureBox(backgroundTexture, new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height));
            controlManager.Add(background);

            titleLabel = new Label();
            titleLabel.Position = Vector2.Zero;
            titleLabel.Text = "TELA DE CONEXAO, AGUARDE!";
            titleLabel.Color = Color.Red;
            titleLabel.Size = titleLabel.SpriteFont.MeasureString(titleLabel.Text);
            controlManager.Add(titleLabel);

            statusLabel = new Label();
            statusLabel.Text = "Estado do Servidor: Offline";
            statusLabel.Color = Color.Red;
            statusLabel.Size = statusLabel.SpriteFont.MeasureString(statusLabel.Text);
            statusLabel.Position = new Vector2(180, 400 - statusLabel.Size.Y - 5f);
            controlManager.Add(statusLabel);

            connect = new LinkLabel();
            connect.Text = "Aguarde";
            connect.Size = connect.SpriteFont.MeasureString(connect.Text);
            connect.Selected += new EventHandler(menuItem_Selected);

            controlManager.Add(connect);

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

            ControlManager_FocusChanged(connect, null);
        }

        public override void Update(GameTime gameTime)
        {
            controlManager.Update(gameTime);
            updateNetworkStatus += gameTime.ElapsedGameTime;
            lastUpdateNetworkStatus += gameTime.ElapsedGameTime;
            if (updateNetworkStatus > TimeSpan.FromSeconds(2))
            {
                systemRef.networkManager.DiscoverServer();
                updateNetworkStatus = TimeSpan.Zero;
            }

            if (lastUpdateNetworkStatus > TimeSpan.FromSeconds(2))
            {
                if (networkStatus == true)
                {
                    statusLabel.Text = "Estado do Servidor: ONLINE";
                    connect.Text = "Conectar";
                    statusLabel.Color = Color.Green;
                    isOnline = true;
                    networkStatus = false;
                }
                else
                {
                    statusLabel.Text = "Estado do Servidor: OFFLINE";
                    connect.Text = "Aguarde";
                    statusLabel.Color = Color.Red;
                    isOnline = false;

                }
                lastUpdateNetworkStatus = TimeSpan.Zero;
            }
            systemRef.networkManager.ReadLobbyPackets();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin();

            controlManager.Draw(systemRef.spriteBatch);
            
            systemRef.spriteBatch.End();

            base.Draw(gameTime);
        }

        /* Event Methods */
        void ControlManager_FocusChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
        }

        private void menuItem_Selected(object sender, EventArgs e)
        {
            if (sender == connect)
            {
                if (isOnline)
                {
                    systemRef.networkManager.ConnectToServer();
                }
            }
        }
    }
}
