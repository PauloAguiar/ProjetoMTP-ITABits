﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Lidgren.Network;

namespace Apollo_16_Radar
{
    public class SystemClass : Game
    {
        public GraphicsDeviceManager graphics;
        Viewport viewport;
        public SpriteBatch spriteBatch;

        public GameStateManager stateManager;
        public NetworkManager networkManager;

        /* Screens */
        public GamePlayScreen gamePlayScreen;
        public InitGameScreen initGameScreen;

		
        const int screenWidth = 1920;
        const int screenHeight = 1080;

        public readonly Rectangle screenRectangle;

        public SystemClass()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.IsFullScreen = true;

            screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

            Content.RootDirectory = "Content";

            /* Add the input handler component to our list of components */
            Components.Add(new InputHandler(this));

            stateManager = new GameStateManager(this);
            networkManager = new NetworkManager(this);
            /* Add the gameState manager component to our list of components */
            Components.Add(stateManager);

            gamePlayScreen = new GamePlayScreen(this, stateManager);
            initGameScreen = new InitGameScreen(this, stateManager);

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
            base.Draw(gameTime);
        }
    }
}
