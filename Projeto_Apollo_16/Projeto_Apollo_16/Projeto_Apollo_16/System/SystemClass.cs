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
<<<<<<< HEAD
        public WaitForPeersScreen waitForPeersScreen;

||||||| merged common ancestors


=======

>>>>>>> e898d2bc07e9244c23cfcba6c6ebe10be7c62226
        const int screenWidth = 800;
        const int screenHeight = 600;

        public readonly Rectangle screenRectangle;

        public SystemClass()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.IsFullScreen = false;

<<<<<<< HEAD
            // Create new instance of configs. Parameter is "application Id". It has to be same on client and server.
            networkConfig = new NetPeerConfiguration("apollo");

            networkConfig.Port = 14242;
            networkConfig.MaximumConnections = 10;
            networkConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            networkConfig.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);

            // Create new server based on the configs just defined
            networkServer = new NetServer(networkConfig);

||||||| merged common ancestors
=======
            
>>>>>>> e898d2bc07e9244c23cfcba6c6ebe10be7c62226
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
            waitForPeersScreen = new WaitForPeersScreen(this, stateManager);

            stateManager.ChangeState(titleScreen);
        }

<<<<<<< HEAD
        public void StartServer()
        {
            networkServer.Start();
        }

        public NetServer GetServer()
        {
            return networkServer;
        }

||||||| merged common ancestors
=======
        
>>>>>>> e898d2bc07e9244c23cfcba6c6ebe10be7c62226
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
