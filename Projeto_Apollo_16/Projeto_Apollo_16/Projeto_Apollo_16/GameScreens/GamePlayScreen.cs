﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public class GamePlayScreen : BaseGameState
    {
        PlayerClass player;
        Ghost ghost;
        ProjectileManager projectilesManager;
        WorldEngine engine;

        List<Shoot> shoots = new List<Shoot>(10);
        List<Shoot> shoots2 = new List<Shoot>(10);

        Shoot shoot;

        Label sectorLabel;
        Label positionLabel;
        Label cameraLabel;
        CameraClass camera;

        /* Constructor */
        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            engine = new WorldEngine(game);
            projectilesManager = new ProjectileManager(game);
        }

        /* XNA Methods */
        public override void Initialize()
        {
            engine.Initialize();
            
            player = new PlayerClass(Vector2.Zero);
            //player = new PlayerClass(new Vector2(systemRef.GraphicsDevice.Viewport.Width / 2, systemRef.GraphicsDevice.Viewport.Height / 2));

            camera = new CameraClass(systemRef.GraphicsDevice.Viewport);
            ghost = new Ghost(new Vector2(systemRef.GraphicsDevice.Viewport.Width / 2, systemRef.GraphicsDevice.Viewport.Height / 2-200));
            
            shoot = new Shoot(new Vector2(400));

            projectilesManager.CreateBullet(Vector2.Zero, new Vector2(1.0f, 1.0f));
            projectilesManager.First.Value.Activate();
            //Texture2D shootTexture;

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
            player.LoadFont(systemRef.Content);
            ghost.LoadTexture(systemRef.Content);
            ghost.LoadFont(systemRef.Content);
            shoot.LoadTexture(systemRef.Content);
        }

        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            player.Update(gameTime);
            
            //ghost.GlobalPosition -= new Vector2(0, player.Velocity.Y) * (float)dt;
            
            //ghost.centralPosition -= player.Velocity * (float)dt;
            ghost.Update(gameTime);

            sectorLabel.Text = "Zoom:" + player.Zoom;
            positionLabel.Text = "Position:" + player.GlobalPosition.X + " " + player.GlobalPosition.Y;
            cameraLabel.Text = "Camera:" + player.CameraPosition.X + " " + player.CameraPosition.Y;
            camera.Zoom = player.Zoom;
            camera.Position = player.GlobalPosition;
            camera.Offset = player.CameraPosition;

            controlManager.Update(gameTime);
            projectilesManager.Update(gameTime);
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.TransformMatrix);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            engine.Draw(systemRef.spriteBatch, player);

            if (player.createShoot())
            {
                shoots.Add(shoot);

            }

            foreach (Shoot s in shoots)
            {
                s.Draw(systemRef.spriteBatch);
            }

            
            if(!ghost.checkCollision(player.GlobalPosition, player.Texture))
            {
                player.Draw(systemRef.spriteBatch);
            }

            ghost.Draw(systemRef.spriteBatch);
            projectilesManager.Draw(systemRef.spriteBatch);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            systemRef.spriteBatch.End();

            systemRef.spriteBatch.Begin();
            controlManager.Draw(systemRef.spriteBatch);
            systemRef.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
