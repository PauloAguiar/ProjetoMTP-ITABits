using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public class GamePlayScreen : BaseGameState
    {
        PlayerClass player;
        WorldEngine engine;

        /* Constructor */
        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            engine = new WorldEngine(game);
        }

        /* XNA Methods */
        public override void Initialize()
        {
            engine.Initialize();
            player = new PlayerClass(new Vector2(systemRef.GraphicsDevice.Viewport.Width / 2, systemRef.GraphicsDevice.Viewport.Height / 2));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            player.LoadTexture(systemRef.Content);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin();

            base.Draw(gameTime);

            engine.Draw(systemRef.spriteBatch, player);
            player.Draw(systemRef.spriteBatch);

            systemRef.spriteBatch.End();
        }
    }
}
