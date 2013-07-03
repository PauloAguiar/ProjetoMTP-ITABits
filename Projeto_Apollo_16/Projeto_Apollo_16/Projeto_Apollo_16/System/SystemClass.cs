using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Lidgren.Network;

using XNAGameConsole;

namespace Projeto_Apollo_16
{
    public class SystemClass : Game
    {
        public GraphicsDeviceManager graphics;
        Viewport viewport;
        public SpriteBatch spriteBatch;
        GameStateManager stateManager;
        public NetworkManager networkManager;

        /* Screens */
        private InitGameScreen initGameScreen;
        public GamePlayScreen gamePlayScreen;

        public bool NETWORK_MODE;
		
        const int SCREEN_WIDTH = 1920;
        const int SCREEN_HEIGHT = 1080;

        public readonly Rectangle screenRectangle;

        public SystemClass()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.IsFullScreen = true;

            screenRectangle = new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);

            Content.RootDirectory = "Content";

            /* Add the input handler component to our list of components */
            Components.Add(new InputHandler(this));

            stateManager = new GameStateManager(this);
            networkManager = new NetworkManager(this);
            
            /* Add the gameState manager component to our list of components */
            Components.Add(stateManager);

            initGameScreen = new InitGameScreen(this, stateManager);
            gamePlayScreen = new GamePlayScreen(this, stateManager);
            

            stateManager.ChangeState(initGameScreen);
        }

        protected override void Initialize()
        {
            viewport = GraphicsDevice.Viewport;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
