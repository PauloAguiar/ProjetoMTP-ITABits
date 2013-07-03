using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Lidgren.Network;

namespace Projeto_Apollo_16
{
    public partial class GamePlayScreen : BaseGameState
    {
        WorldEngine engine;
        public PlayerClass player;

        //Texture2D ponto;


        #region managers
        ProjectileManager projectilesManager;
        ExplosionManager explosionManager;
        EnemyManager enemyManager;
        ItemManager itemManager;
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
            LoadLabels();
            //ponto = content.Load<Texture2D>(@"Sprites\Shoots\ponto");
        }
        #endregion

        #region update
        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            if (systemRef.networkManager.GetStatus() == NetPeerStatus.Running)
                systemRef.networkManager.ReadPackets(this);
            
            CameraUpdate();
            UpdateManagers(gameTime);

            if (systemRef.networkManager.GetStatus() == NetPeerStatus.Running)
                systemRef.networkManager.SendPackets(gameTime, 
                                                     new PilotDataClass(player.throttle, player.Speed, player.Angle, player.Direction),
                                                     enemyManager.GetRadarData(player), 
                                                     player.GetRadarImmediateData(player), 
													 ProjectileClass.GetShooterData(),
                                                     player.GetCopilotImmediateData()
                                                     );

            base.Update(gameTime);
        }

        private void UpdateManagers(GameTime gameTime)
        {
            GameLogic.Update(gameTime);
            controlManager.Update(gameTime);
        }
        #endregion

        #region draw
        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.TransformMatrix);
            engine.Draw(systemRef.spriteBatch, player);
            DrawActors();
            //foreach (ProjectileClass p in projectilesManager)
            //{
            //    if(p is LightSaber)
            //    {
            //        CollisionManager.DrawLightSaberCircles(systemRef.spriteBatch, (LightSaber)p, ponto);
            
            //    }
            //}

            systemRef.spriteBatch.End();



            //câmera diferente pra desenhar o HUD
            systemRef.spriteBatch.Begin();
            controlManager.Draw(systemRef.spriteBatch);
            systemRef.spriteBatch.End();

            base.Draw(gameTime);
        }
        
        private void DrawActors()
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
