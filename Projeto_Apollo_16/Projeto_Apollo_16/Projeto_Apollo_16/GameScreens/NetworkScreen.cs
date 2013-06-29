using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Projeto_Apollo_16
{
    public class NetworkScreen : BaseGameState
    {
        Label titleLabel;
        Label statusLabel;
        Label infoLabel;

        /* Constructor */
        public NetworkScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        /* XNA Methods */
        public override void Initialize()
        {
            systemRef.networkManager.StartServer();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            titleLabel = new Label();
            titleLabel.Position = Vector2.Zero;
            titleLabel.Text = "SERVIDOR INICIALIZADO, AGUARDE!";
            titleLabel.Color = Color.Red;
            titleLabel.Size = titleLabel.SpriteFont.MeasureString(titleLabel.Text);
            controlManager.Add(titleLabel);

            infoLabel = new Label();
            infoLabel.Position = Vector2.Zero + 2 * (new Vector2(0.0f, 25.0f));
            infoLabel.Text = "Aperte Espaco para continuar no modo Offline!";
            infoLabel.Color = Color.Red;
            infoLabel.Size = infoLabel.SpriteFont.MeasureString(infoLabel.Text);
            controlManager.Add(infoLabel);

            statusLabel = new Label();
            statusLabel.Position = Vector2.Zero + 3 * (new Vector2(0.0f, 25.0f));
            statusLabel.Text = "Aperte Enter para iniciar no modo online";
            statusLabel.Color = Color.Red;
            statusLabel.Size = statusLabel.SpriteFont.MeasureString(statusLabel.Text);
            controlManager.Add(statusLabel);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                systemRef.NETWORK_MODE = false;
                stateManager.PushState(systemRef.gamePlayScreen);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                systemRef.NETWORK_MODE = true;
                stateManager.PushState(systemRef.gamePlayScreen);
            }

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin();

            controlManager.Draw(systemRef.spriteBatch);

            systemRef.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
