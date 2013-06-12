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
        ProjectileManager projectilesManager;
        ExplosionManager explosionManager;
        EnemyManager enemyManager;

        WorldEngine engine;

        Label sectorLabel;
        Label positionLabel;
        Label cameraLabel;
        Label networkLabel;
        CameraClass camera;

        /* Constructor */
        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            engine = new WorldEngine(game);
            projectilesManager = new ProjectileManager(game);
            explosionManager = new ExplosionManager(game);
            enemyManager = new EnemyManager(game);
        }

        /* XNA Methods */
        public override void Initialize()
        {
            engine.Initialize();
            NetworkClass.StartServer();

            player = new PlayerClass(Vector2.Zero);
            camera = new CameraClass(systemRef.GraphicsDevice.Viewport);
            
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

            networkLabel = new Label();
            networkLabel.Position = Vector2.Zero + 3 * (new Vector2(0.0f, 25.0f));
            networkLabel.Text = NetworkClass.GetStatus();
            networkLabel.Color = Color.Red;
            networkLabel.Size = networkLabel.SpriteFont.MeasureString(networkLabel.Text);
            controlManager.Add(networkLabel);

            player.LoadTexture(systemRef.Content);
            player.LoadFont(systemRef.Content);
            
        }

        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;

            player.Update(gameTime);

            Globals.playerPosition = player.GlobalPosition;
            Globals.playerVelocity = player.Velocity;
            
            
            sectorLabel.Text = "Zoom:" + player.Zoom;
            positionLabel.Text = "Position:" + player.GlobalPosition.X + " " + player.GlobalPosition.Y;
            cameraLabel.Text = "Camera:" + player.CameraPosition.X + " " + player.CameraPosition.Y;
            networkLabel.Text = NetworkClass.GetStatus();
            camera.Zoom = player.Zoom;
            camera.Position = player.GlobalPosition;
            camera.Offset = player.CameraPosition;

            controlManager.Update(gameTime);
            projectilesManager.Update(gameTime);
            explosionManager.Update(gameTime);
            enemyManager.Update(gameTime);

            //só pra testar os inimigos
            if (Keyboard.GetState().IsKeyDown(Keys.Z) && enemyManager.spawnTime >= EnemyManager.tts)
            {
                Ghost g = new Ghost(player.GlobalPosition, content);
                enemyManager.createEnemy(g);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.X) && enemyManager.spawnTime >= EnemyManager.tts)
            {
                Sun s = new Sun(player.GlobalPosition, content);
                enemyManager.createEnemy(s);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.C) && enemyManager.spawnTime >= EnemyManager.tts)
            {
                Poligon p = new Poligon(player.GlobalPosition, content);
                enemyManager.createEnemy(p);
            }



            if (Keyboard.GetState().IsKeyDown(Keys.Space) && projectilesManager.bulletSpawnTime > ProjectileManager.tts)
            {
                //de acordo com o angulo
                Vector2 v = new Vector2((float)Math.Sin(player.Angle), -(float)Math.Cos(player.Angle));
                
                v.Normalize();
                v *= 2/3.0f;

                LinearProjectile p = new LinearProjectile(player.GlobalPosition, v, Vector2.Zero, content);

                //sem aceleração
                projectilesManager.CreateBullet(p);
                //com aceleração
                //projectilesManager.CreateBullet(player.GlobalPosition, v, new Vector2(-0.001f, 0.001f));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.M) && projectilesManager.bulletSpawnTime > ProjectileManager.tts)
            {
                CircularProjectile p = new CircularProjectile(player.GlobalPosition, content, player);
                projectilesManager.CreateBullet(p);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.N) && projectilesManager.bulletSpawnTime > ProjectileManager.tts)
            {
                if (enemyManager.Count > 0)
                {

                    HomingProjectile p = new HomingProjectile(player.GlobalPosition, content, CollisionManager.findNearest(player, enemyManager));
                    projectilesManager.CreateBullet(p);
                }
            }

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.TransformMatrix);
            
            engine.Draw(systemRef.spriteBatch, player);

            ///checa colisão entre todos os tiros e inimigos
            for (int i = 0; i < projectilesManager.Count; i++)
            {
                for (int j = 0; j < enemyManager.Count; j++)
                {

                    if (CollisionManager.checkCollisionCircular(projectilesManager.ElementAt(i), enemyManager.ElementAt(j)))
                    {
                        //cria uma explosão diferente pra cada tipo de inimigo
                        if (enemyManager.ElementAt(j) is Sun)
                        {
                            Explosion2 e = new Explosion2(projectilesManager.ElementAt(i).GlobalPosition, content);
                            explosionManager.createExplosion(e);

                        }
                        else
                        {
                            ExplosionSimple e = new ExplosionSimple(projectilesManager.ElementAt(i).GlobalPosition, content);
                            explosionManager.createExplosion(e);
                        }

                        projectilesManager.RemoveAt(i);
                        enemyManager.RemoveAt(j);
                        i--;
                        break;
                    }    
                }
            }

            player.Draw(systemRef.spriteBatch);
            projectilesManager.Draw(systemRef.spriteBatch);
            explosionManager.Draw(systemRef.spriteBatch);
            enemyManager.Draw(systemRef.spriteBatch);

            systemRef.spriteBatch.End();

            //câmera diferente pra desenhar o HUD
            systemRef.spriteBatch.Begin();
            controlManager.Draw(systemRef.spriteBatch);
            systemRef.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
