using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Projeto_Apollo_16.Actors;

namespace Projeto_Apollo_16
{
    public class GamePlayScreen : BaseGameState
    {
        PlayerClass player;
        Ghost ghost;
        WorldEngine engine;

        List<Shoot> shoots = new List<Shoot>(10);
        List<Shoot> shoots2 = new List<Shoot>(10);

        
        Shoot shoot;



        

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
            player = new PlayerClass(new Vector2(400));
            //player = new PlayerClass(new Vector2(systemRef.GraphicsDevice.Viewport.Width / 2, systemRef.GraphicsDevice.Viewport.Height / 2));
            ghost = new Ghost(new Vector2(systemRef.GraphicsDevice.Viewport.Width / 2, systemRef.GraphicsDevice.Viewport.Height / 2-200));
            
            shoot = new Shoot(new Vector2(400));


            //Texture2D shootTexture;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            player.LoadTexture(systemRef.Content);
            ghost.LoadTexture(systemRef.Content);

            shoot.LoadTexture(systemRef.Content);



            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            player.Update(gameTime);
            
            ghost.GlobalPosition -= new Vector2(0, player.Velocity.Y) * (float)dt;
            
            ghost.centralPosition -= player.Velocity * (float)dt;
            ghost.Update(gameTime);

            base.Update(gameTime);
        }


        int i = 0;
        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin();

            base.Draw(gameTime);

            engine.Draw(systemRef.spriteBatch, player);

            if (player.createShoot())
            {
                shoots.Add(shoot);

            }

            foreach (Shoot s in shoots)
            {
                s.Draw(systemRef.spriteBatch);
            }

            
            if(!ghost.checkCollision(player.GlobalPosition, player.texture))
            {
                player.Draw(systemRef.spriteBatch);
            }

            ghost.Draw(systemRef.spriteBatch);
            
            systemRef.spriteBatch.End();
        }
    }
}
