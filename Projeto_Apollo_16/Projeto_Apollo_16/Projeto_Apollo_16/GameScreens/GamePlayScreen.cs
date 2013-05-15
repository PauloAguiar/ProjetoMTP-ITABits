using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Projeto_Apollo_16.Actors;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Projeto_Apollo_16
{
    public class GamePlayScreen : BaseGameState
    {
        PlayerClass player;
        Ghost ghost;
        WorldEngine engine;
<<<<<<< HEAD

        List<Shoot> shoots = new List<Shoot>(10);
        List<Shoot> shoots2 = new List<Shoot>(10);

        
        Shoot shoot;

        Label sectorLabel;
        Label positionLabel;
        Label cameraLabel;
        CameraClass camera;
>>>>>>> 2ae1c103da7b3f7f22e5207ac4316b69bf3ab9ff
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
<<<<<<< HEAD
            player = new PlayerClass(new Vector2(400));
            //player = new PlayerClass(new Vector2(systemRef.GraphicsDevice.Viewport.Width / 2, systemRef.GraphicsDevice.Viewport.Height / 2));

            camera = new CameraClass(systemRef.GraphicsDevice.Viewport);
>>>>>>> 2ae1c103da7b3f7f22e5207ac4316b69bf3ab9ff
            ghost = new Ghost(new Vector2(systemRef.GraphicsDevice.Viewport.Width / 2, systemRef.GraphicsDevice.Viewport.Height / 2-200));
<<<<<<< HEAD
            
            shoot = new Shoot(new Vector2(400));


            //Texture2D shootTexture;

>>>>>>> 2ae1c103da7b3f7f22e5207ac4316b69bf3ab9ff
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            sectorLabel = new Label();
            sectorLabel.Position = Vector2.Zero;
            sectorLabel.Text = "Zoom:" + player.Zoom;
            sectorLabel.Color = Color.Blue;
            sectorLabel.Size = sectorLabel.SpriteFont.MeasureString(sectorLabel.Text);
            controlManager.Add(sectorLabel);

            positionLabel = new Label();
            positionLabel.Position = Vector2.Zero + 1 * (new Vector2(0.0f, 25.0f));
            positionLabel.Text = "Position:" + player.GlobalPosition.X + " " + player.GlobalPosition.Y;
            positionLabel.Color = Color.Yellow;
            positionLabel.Size = positionLabel.SpriteFont.MeasureString(positionLabel.Text);
            controlManager.Add(positionLabel);

            cameraLabel = new Label();
            cameraLabel.Position = Vector2.Zero + 2 * (new Vector2(0.0f, 25.0f));
            cameraLabel.Text = "Camera:" + player.CameraPosition.X + " " + player.CameraPosition.Y;
            cameraLabel.Color = Color.Green;
            cameraLabel.Size = cameraLabel.SpriteFont.MeasureString(cameraLabel.Text);
            controlManager.Add(cameraLabel);

            player.LoadTexture(systemRef.Content);
            ghost.LoadTexture(systemRef.Content);
<<<<<<< HEAD

            shoot.LoadTexture(systemRef.Content);



            base.LoadContent();
>>>>>>> 2ae1c103da7b3f7f22e5207ac4316b69bf3ab9ff
        }

        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            player.Update(gameTime);
            
            ghost.GlobalPosition -= new Vector2(0, player.Velocity.Y) * (float)dt;
            
            ghost.centralPosition -= player.Velocity * (float)dt;
            ghost.Update(gameTime);

<<<<<<< HEAD
            sectorLabel.Text = "Zoom:" + player.Zoom;
            positionLabel.Text = "Position:" + player.GlobalPosition.X + " " + player.GlobalPosition.Y;
            cameraLabel.Text = "Camera:" + player.CameraPosition.X + " " + player.CameraPosition.Y;
            camera.Zoom = player.Zoom;
            camera.Position = player.CameraPosition;
            camera.LookAt(player.GlobalPosition);

            controlManager.Update(gameTime);
>>>>>>> ccb9968a978581b5dde206cdf5893af9095021e2
            base.Update(gameTime);
        }


        int i = 0;
        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin(SpriteSortMode.BackToFront,
                    null, null, null, null, null,
                    camera.TransformMatrix);

            engine.Draw(systemRef.spriteBatch, player);

<<<<<<< HEAD
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
            
            controlManager.Draw(systemRef.spriteBatch);

>>>>>>> 2ae1c103da7b3f7f22e5207ac4316b69bf3ab9ff
            systemRef.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
