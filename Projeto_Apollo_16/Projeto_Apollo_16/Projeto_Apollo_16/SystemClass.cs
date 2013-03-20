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
        /*Class que basicamente substitui a Game1.cs
         * Essa classe vai conter as diversas classes do nosso jogo, ela deve existir como um nível mais próximo do Hardware, isto é, fazendo chamadas às várias funções que 
         * de alguma forma interagem em um nível mais próximo da máquina, como Initializing, Loading, Unloading */
        Nave nave;

        GraphicsDeviceManager graphics;
        Viewport viewport;
        SpriteBatch spriteBatch;
        Texture2D mapChunkTexture;
        

        public SystemClass()
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
            mapChunkTexture = Content.Load<Texture2D>("Maps\\1-1");

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
            spriteBatch.Draw(mapChunkTexture, Vector2.Zero, Color.White);
            nave.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
