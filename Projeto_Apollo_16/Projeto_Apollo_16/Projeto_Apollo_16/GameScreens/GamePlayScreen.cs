using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Input = Microsoft.Xna.Framework.Input;

namespace Projeto_Apollo_16
{
    public partial class GamePlayScreen : BaseGameState
    {
        PlayerClass player;
        WorldEngine engine;
        CameraClass camera;

        #region managers
        ProjectileManager projectilesManager;
        ExplosionManager explosionManager;
        EnemyManager enemyManager;
        ItemManager itemManager;
        #endregion

        #region labels
        Label sectorLabel;
        Label positionLabel;
        Label cameraLabel;
        Label statusLabel;
        Label weaponLabel;
        Label timeLabel;
        #endregion

        #region ctor
        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            engine = new WorldEngine(game);
            projectilesManager = new ProjectileManager(game);
            explosionManager = new ExplosionManager(game);
            enemyManager = new EnemyManager(game);
            itemManager = new ItemManager(game);
        }
        #endregion

        #region initialize
        public override void Initialize()
        {
            player = new PlayerClass(Vector2.Zero, content);
            camera = new CameraClass(systemRef.GraphicsDevice.Viewport);
            GameLogic.Initialize(explosionManager, itemManager, projectilesManager, enemyManager, content, player);
            base.Initialize();
        }
        #endregion

        #region load
        protected override void LoadContent()
        {
            base.LoadContent();
            engine.LoadContent();
            loadLabels();
        }
        
        private void loadLabels()
        {
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

            statusLabel = new Label();
            statusLabel.Position = Vector2.Zero + 3 * (new Vector2(0.0f, 25.0f));
            statusLabel.Text = "Nao Conectado!";
            statusLabel.Color = Color.Red;
            statusLabel.Size = statusLabel.SpriteFont.MeasureString(statusLabel.Text);
            controlManager.Add(statusLabel);

            weaponLabel = new Label();
            weaponLabel.Position = Vector2.Zero + 4 * (new Vector2(0.0f, 25.0f));
            weaponLabel.Text = "Weapon:" + player.bullets;
            weaponLabel.Color = Color.Purple;
            weaponLabel.Size = weaponLabel.SpriteFont.MeasureString(weaponLabel.Text);
            controlManager.Add(weaponLabel);

            timeLabel = new Label();
            timeLabel.Position = Vector2.Zero + 5 * (new Vector2(0.0f, 25.0f));
            timeLabel.Text = "Time: " + ((int)GameLogic.timeCreateEnemies).ToString();
            timeLabel.Color = Color.White;
            timeLabel.Size = timeLabel.SpriteFont.MeasureString(timeLabel.Text);
            controlManager.Add(timeLabel);
        }
        #endregion

        #region update
        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;
            
            if (systemRef.NETWORK_MODE)
                systemRef.networkManager.ReadInGamePackets();
            
            cameraUpdate();

            updateManagers(gameTime);
            
            if (systemRef.NETWORK_MODE)
                systemRef.networkManager.SendPackets(new PilotDataClass(player.throttle, player.Speed, player.Angle, player.Velocity));

            base.Update(gameTime);
        }

        private void updateManagers(GameTime gameTime)
        {
            GameLogic.Update(gameTime);
            controlManager.Update(gameTime);
        }

        private void cameraUpdate()
        {
            sectorLabel.Text = "Zoom:" + player.Zoom;
            positionLabel.Text = "Position:" + (int)player.GlobalPosition.X + " " + (int)player.GlobalPosition.Y;
            cameraLabel.Text = "Camera:" + player.CameraPosition.X + " " + player.CameraPosition.Y;
            weaponLabel.Text = "Weapon:" + player.bullets;
            timeLabel.Text = "Time: " + ((int)GameLogic.timeCreateEnemies).ToString();

            //statusLabel.Text = NetworkClass.status;
            if(systemRef.NETWORK_MODE)
                statusLabel.Text = systemRef.networkManager.status;

            camera.Zoom = player.Zoom;
            camera.Position = player.GlobalPosition;
            camera.Offset = player.CameraPosition;
        }

        #endregion

        #region draw
        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.TransformMatrix);
            
            engine.Draw(systemRef.spriteBatch, player);
            drawActors();
            
            systemRef.spriteBatch.End();

            //câmera diferente pra desenhar o HUD
            systemRef.spriteBatch.Begin();
            controlManager.Draw(systemRef.spriteBatch);
            systemRef.spriteBatch.End();

            base.Draw(gameTime);
        }
        
        private void drawActors()
        {
            player.Draw(systemRef.spriteBatch);
            projectilesManager.Draw(systemRef.spriteBatch);
            explosionManager.Draw(systemRef.spriteBatch);
            enemyManager.Draw(systemRef.spriteBatch);
            itemManager.Draw(systemRef.spriteBatch);
        }
        #endregion
    }
}
