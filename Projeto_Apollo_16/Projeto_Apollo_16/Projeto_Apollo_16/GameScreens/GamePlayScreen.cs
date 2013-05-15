using Microsoft.Xna.Framework;
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
        Label sectorLabel;
        Label positionLabel;
        Label cameraLabel;
        CameraClass camera;
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
            camera = new CameraClass(systemRef.GraphicsDevice.Viewport);
            player = new PlayerClass(new Vector2(systemRef.GraphicsDevice.Viewport.Width / 2, systemRef.GraphicsDevice.Viewport.Height / 2));
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
            ghost.LoadTexture(systemRef.Content);
        }

        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            player.Update(gameTime);
            ghost.GlobalPosition -= player.Velocity * (float)dt;
            ghost.Update(gameTime);

            sectorLabel.Text = "Zoom:" + player.Zoom;
            positionLabel.Text = "Position:" + player.GlobalPosition.X + " " + player.GlobalPosition.Y;
            cameraLabel.Text = "Camera:" + player.CameraPosition.X + " " + player.CameraPosition.Y;
            camera.Zoom = player.Zoom;
            camera.Position = player.CameraPosition;
            camera.LookAt(player.GlobalPosition);

            controlManager.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin(SpriteSortMode.BackToFront,
                    null, null, null, null, null,
                    camera.TransformMatrix);

            engine.Draw(systemRef.spriteBatch, player);
            player.Draw(systemRef.spriteBatch);
            ghost.Draw(systemRef.spriteBatch);

            controlManager.Draw(systemRef.spriteBatch);

            systemRef.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
