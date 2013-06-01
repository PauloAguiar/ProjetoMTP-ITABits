using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Projeto_Apollo_16
{
    public class GamePlayScreen : BaseGameState
    {
        PlayerClass player;
        Ghost ghost;
        ProjectileManager projectilesManager;
        ExplosionManager explosionManager;

        WorldEngine engine;

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
            explosionManager = new ExplosionManager(game);
        }

        /* XNA Methods */
        public override void Initialize()
        {
            engine.Initialize();
            
            player = new PlayerClass(Vector2.Zero);
            camera = new CameraClass(systemRef.GraphicsDevice.Viewport);
            ghost = new Ghost(new Vector2(systemRef.GraphicsDevice.Viewport.Width / 2, systemRef.GraphicsDevice.Viewport.Height / 2-200));

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
            
        }

        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            player.Update(gameTime);

            ghost.Update(gameTime);

            sectorLabel.Text = "Zoom:" + player.Zoom;
            positionLabel.Text = "Position:" + player.GlobalPosition.X + " " + player.GlobalPosition.Y;
            cameraLabel.Text = "Camera:" + player.CameraPosition.X + " " + player.CameraPosition.Y;
            camera.Zoom = player.Zoom;
            camera.Position = player.GlobalPosition;
            camera.Offset = player.CameraPosition;

            controlManager.Update(gameTime);
            projectilesManager.Update(gameTime);
            explosionManager.Update(gameTime);

            //só pra testar as explosion
            if(Keyboard.GetState().IsKeyDown(Keys.Y))
            {
                explosionManager.createExplosion(player.GlobalPosition);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && player.bulletSpawnTime > 3000) //3 segundos pra criar novo tiro
            {
                Vector2 v = new Vector2(-player.Velocity.X, player.Velocity.Y);
                v.Normalize();
                v /= 10;

                player.bulletSpawnTime = 0;

                if (player.Speed > 0)
                {
                   projectilesManager.CreateBullet(player.GlobalPosition, v, Vector2.Zero);
                 //com aceleração: projectilesManager.CreateBullet(player.GlobalPosition, new Vector2(-player.Velocity.X, player.Velocity.Y), new Vector2(-0.00001f, 0.00001f));
                }
                else
                {
                    projectilesManager.CreateBullet(player.GlobalPosition, -v, Vector2.Zero);
                    //projectilesManager.CreateBullet(player.GlobalPosition, new Vector2(player.Velocity.X, -player.Velocity.Y), new Vector2(-0.00001f, 0.00001f));
                }
                projectilesManager.First.Value.Activate();
            }


            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.TransformMatrix);
            
            engine.Draw(systemRef.spriteBatch, player);

            if (!ghost.checkCollision(player.GlobalPosition, player.Texture))
            {
                player.Draw(systemRef.spriteBatch);
            }
            else
            {
                explosionManager.createExplosion(player.GlobalPosition);
            }

            ghost.Draw(systemRef.spriteBatch);
            projectilesManager.Draw(systemRef.spriteBatch);
            explosionManager.Draw(systemRef.spriteBatch);

            systemRef.spriteBatch.End();

            //câmera diferente
            systemRef.spriteBatch.Begin();
            controlManager.Draw(systemRef.spriteBatch);
            systemRef.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
