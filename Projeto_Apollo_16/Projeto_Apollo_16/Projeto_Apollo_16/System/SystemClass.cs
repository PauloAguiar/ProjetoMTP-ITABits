using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Projeto_Apollo_16
{
    public class SystemClass : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        Viewport viewport;
        public SpriteBatch spriteBatch;
        MapClass map;
        GameStateManager stateManager;
        PlayerClass player;

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
            map = new MapClass();
        }

        protected override void Initialize()
        {
            
            viewport = GraphicsDevice.Viewport;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D naveTexture = Content.Load<Texture2D>("Sprites\\Nave\\nave");
            map.LoadMapContent(Content);

            player = new PlayerClass(new Vector2(viewport.Width/2, viewport.Height/2), Math.PI/600, naveTexture);
            
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            map.Draw(spriteBatch, player);
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
