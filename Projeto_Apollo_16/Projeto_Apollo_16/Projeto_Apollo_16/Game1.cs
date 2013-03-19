using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Projeto_Apollo_16
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Nave nave;

        GraphicsDeviceManager graphics;
        Viewport viewport;
        SpriteBatch spriteBatch;

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            
            Content.RootDirectory = "Content";
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

            nave = new Nave(new Vector2(viewport.Width/2, viewport.Height/2), Math.PI/600, naveTexture);
            
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            nave.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            nave.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
