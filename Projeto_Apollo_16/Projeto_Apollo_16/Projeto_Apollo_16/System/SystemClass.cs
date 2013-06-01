using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Lidgren.Network;

namespace Projeto_Apollo_16
{
    public class SystemClass : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        Viewport viewport;
        public SpriteBatch spriteBatch;
        GameStateManager stateManager;

        static NetServer networkServer;
        static NetPeerConfiguration networkConfig;

        /* Screens */
        public TitleScreen titleScreen;
        public StartMenuScreen startMenuScreen;
        public GamePlayScreen gamePlayScreen;

		
        const int screenWidth = 800;
        const int screenHeight = 600;

        public readonly Rectangle screenRectangle;

        public SystemClass()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.IsFullScreen = false;

            screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

            Content.RootDirectory = "Content";

            /* Add the input handler component to our list of components */
            Components.Add(new InputHandler(this));

            stateManager = new GameStateManager(this);
            /* Add the gameState manager component to our list of components */
            Components.Add(stateManager);

            titleScreen = new TitleScreen(this, stateManager);
            startMenuScreen = new StartMenuScreen(this, stateManager);
            gamePlayScreen = new GamePlayScreen(this, stateManager);
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
