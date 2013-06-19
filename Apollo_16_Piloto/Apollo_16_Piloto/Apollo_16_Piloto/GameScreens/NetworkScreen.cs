using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Lidgren.Network;

namespace Apollo_16_Piloto
{
    public class NetworkScreen : BaseGameState
    {
        Label titleLabel;
        Label statusLabel;

        public PilotClass pilot;
        /* Constructor */
        public NetworkScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            pilot = new PilotClass();
            
        }

        /* XNA Methods */
        public override void Initialize()
        {
            systemRef.networkManager.ConnectToServer();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            pilot.LoadFont(content);

            titleLabel = new Label();
            titleLabel.Position = Vector2.Zero;
            titleLabel.Text = "TELA DE CONEXAO, AGUARDE!";
            titleLabel.Color = Color.Red;
            titleLabel.Size = titleLabel.SpriteFont.MeasureString(titleLabel.Text);
            controlManager.Add(titleLabel);

            statusLabel = new Label();
            statusLabel.Position = Vector2.Zero + 3 * (new Vector2(0.0f, 25.0f));
            statusLabel.Text = "";
            statusLabel.Color = Color.Red;
            statusLabel.Size = statusLabel.SpriteFont.MeasureString(statusLabel.Text);
            controlManager.Add(statusLabel);
        }

        public override void Update(GameTime gameTime)
        {
            systemRef.networkManager.ReadLobbyPackets();
            InputDataClass inputData = new InputDataClass();

            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                inputData.spaceBar = true;
            }

            systemRef.networkManager.SendPackets(inputData);
            statusLabel.Text = systemRef.networkManager.status;
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin();

            controlManager.Draw(systemRef.spriteBatch);
            pilot.Draw(systemRef.spriteBatch);

            systemRef.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
