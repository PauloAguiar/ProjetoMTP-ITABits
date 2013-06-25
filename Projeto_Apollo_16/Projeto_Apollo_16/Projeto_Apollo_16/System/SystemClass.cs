using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Lidgren.Network;

using XNAGameConsole;

namespace Projeto_Apollo_16
{
    public class SystemClass : Game
    {
        GraphicsDeviceManager graphics;
        Viewport viewport;
        public SpriteBatch spriteBatch;
        GameStateManager stateManager;
        GameConsole console;
        public NetworkManager networkManager;

        /* Screens */
        public TitleScreen titleScreen;
        public StartMenuScreen startMenuScreen;
        public GamePlayScreen gamePlayScreen;
        public NetworkScreen networkScreen;

        public bool NETWORK_MODE;
		
        const int SCREEN_WIDTH = 800;
        const int SCREEN_HEIGHT = 600;

        public readonly Rectangle screenRectangle;

        public SystemClass()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.IsFullScreen = false;

            screenRectangle = new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);

            Content.RootDirectory = "Content";

            /* Add the input handler component to our list of components */
            Components.Add(new InputHandler(this));

            stateManager = new GameStateManager(this);
            networkManager = new NetworkManager(this);
            
            /* Add the gameState manager component to our list of components */
            Components.Add(stateManager);

            titleScreen = new TitleScreen(this, stateManager);
            startMenuScreen = new StartMenuScreen(this, stateManager);
            gamePlayScreen = new GamePlayScreen(this, stateManager);
            networkScreen = new NetworkScreen(this, stateManager);

            stateManager.ChangeState(titleScreen);
        }

        protected override void Initialize()
        {
            viewport = GraphicsDevice.Viewport;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            console = new GameConsole((Game)this, spriteBatch);
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
