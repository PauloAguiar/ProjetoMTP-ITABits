using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Input = Microsoft.Xna.Framework.Input;
using SlimDX.DirectInput;

namespace Projeto_Apollo_16
{
    public class GamePlayScreen : BaseGameState
    {
        PlayerClass player;
        Joystick joystick;
        JoystickState joystickState = new JoystickState();
        public const int joystickRange = 10000;
        const int minTimeChangeWeapon = 1000;
        double timeChangedWeapon = minTimeChangeWeapon;



        #region managers
        ProjectileManager projectilesManager;
        ExplosionManager explosionManager;
        EnemyManager enemyManager;
        ItemManager itemManager;
        #endregion

        WorldEngine engine;

        CameraClass camera;

        #region labels
        Label sectorLabel;
        Label positionLabel;
        Label cameraLabel;
        Label statusLabel;
        Label weaponLabel;
        #endregion

        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            engine = new WorldEngine(game);
            projectilesManager = new ProjectileManager(game);
            explosionManager = new ExplosionManager(game);
            enemyManager = new EnemyManager(game);
            itemManager = new ItemManager(game);
        }

        #region initialize
        public override void Initialize()
        {
            engine.Initialize();
            NetworkClass.StartServer();
            
            player = new PlayerClass(Vector2.Zero, content);
            camera = new CameraClass(systemRef.GraphicsDevice.Viewport);

            base.Initialize();
        }

        
        #endregion

        #region load
        protected override void LoadContent()
        {
            base.LoadContent();

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

            weaponLabel = new Label();
            weaponLabel.Position = Vector2.Zero + 1 * (new Vector2(0.0f, 75.0f));
            weaponLabel.Text = "Weapon:" + player.bullets;
            weaponLabel.Color = Color.Purple;
            weaponLabel.Size = weaponLabel.SpriteFont.MeasureString(weaponLabel.Text);
            controlManager.Add(weaponLabel);

            cameraLabel = new Label();
            cameraLabel.Position = Vector2.Zero + 2 * (new Vector2(0.0f, 25.0f));
            cameraLabel.Text = "Camera:" + player.CameraPosition.X + " " + player.CameraPosition.Y;
            cameraLabel.Color = Color.Green;
            cameraLabel.Size = cameraLabel.SpriteFont.MeasureString(cameraLabel.Text);
            controlManager.Add(cameraLabel);

            statusLabel = new Label();
            statusLabel.Position = Vector2.Zero + 3 * (new Vector2(0.0f, 25.0f));
            statusLabel.Text = "";
            statusLabel.Color = Color.Red;
            statusLabel.Size = statusLabel.SpriteFont.MeasureString(statusLabel.Text);
            controlManager.Add(statusLabel);
        }
        #endregion

        #region update

        public override void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;
            timeChangedWeapon += dt;

            updateJoystick();
            
            player.Update(gameTime, joystickState, joystickRange);

            cameraUpdate();

            updateManagers(gameTime);

            createEnemies();

            createBullets();

            createItems();

            checkCollision();

            base.Update(gameTime);
        }

        void CreateDevice()
        {
            DirectInput dinput = new DirectInput();

            foreach (DeviceInstance device in dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
            {
                try
                {
                    joystick = new Joystick(dinput, device.InstanceGuid);
                    break;
                }
                catch (DirectInputException)
                {
                }
            }

            if (joystick != null)
            {
                foreach (DeviceObjectInstance deviceObject in joystick.GetObjects())
                {
                    if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                        joystick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-joystickRange, joystickRange);
                }

                joystick.Acquire();
            }

        }

        void ReleaseDevice()
        {
            if (joystick != null)
            {
                joystick.Unacquire();
                joystick.Dispose();
            }
            joystick = null;
        }

        private void updateJoystick()
        {
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.L))
            {
                ReleaseDevice();
                CreateDevice();
            }
            
            ReadImmediateData();
        }

        void ReadImmediateData()
        {
            if (joystick == null)
            {
                return;
            }
            joystickState = joystick.GetCurrentState();
        }

        private void updateManagers(GameTime gameTime)
        {
            controlManager.Update(gameTime);
            projectilesManager.Update(gameTime);
            explosionManager.Update(gameTime);
            enemyManager.Update(gameTime);
            itemManager.Update(gameTime);
        }

        private void cameraUpdate()
        {
            sectorLabel.Text = "Zoom:" + player.Zoom;
            positionLabel.Text = "Position:" + player.GlobalPosition.X + " " + player.GlobalPosition.Y;
            cameraLabel.Text = "Camera:" + player.CameraPosition.X + " " + player.CameraPosition.Y;
            weaponLabel.Text = "Weapon:" + player.bullets;
            statusLabel.Text = NetworkClass.status;
            camera.Zoom = player.Zoom;
            camera.Position = player.GlobalPosition;
            camera.Offset = player.CameraPosition;
        }

        private void createEnemies()
        {
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Z) && enemyManager.spawnTime >= EnemyManager.tts)
            {
                Ghost g = new Ghost(player.GlobalPosition, content);
                enemyManager.createEnemy(g);
            }
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.X) && enemyManager.spawnTime >= EnemyManager.tts)
            {
                Sun s = new Sun(player.GlobalPosition, content);
                enemyManager.createEnemy(s);
            }
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.C) && enemyManager.spawnTime >= EnemyManager.tts)
            {
                Poligon p = new Poligon(player.GlobalPosition, content);
                enemyManager.createEnemy(p);
            }
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.V) && enemyManager.spawnTime >= EnemyManager.tts)
            {
                Chaser c = new Chaser(player.GlobalPosition, content, player);
                enemyManager.createEnemy(c);
            }
        }

        private void createBullets()
        {
            if ( ((Input.Keyboard.GetState().IsKeyDown(Input.Keys.F2)) || joystickState.IsPressed(5)) && timeChangedWeapon > minTimeChangeWeapon)
            {
                timeChangedWeapon = 0;
                if (player.bullets == PlayerClass.Bullets.linear)
                    player.bullets = PlayerClass.Bullets.circular;
                else if (player.bullets == PlayerClass.Bullets.circular)
                    player.bullets = PlayerClass.Bullets.homing;
                else
                    player.bullets = PlayerClass.Bullets.linear;
            }
            if (((Input.Keyboard.GetState().IsKeyDown(Input.Keys.F1)) || joystickState.IsPressed(4)) && timeChangedWeapon > minTimeChangeWeapon)
            {
                timeChangedWeapon = 0;
                if (player.bullets == PlayerClass.Bullets.linear)
                    player.bullets = PlayerClass.Bullets.homing;
                else if (player.bullets == PlayerClass.Bullets.circular)
                    player.bullets = PlayerClass.Bullets.linear;
                else
                    player.bullets = PlayerClass.Bullets.circular;
            }
            if (((Input.Keyboard.GetState().IsKeyDown(Input.Keys.Space)) || joystickState.IsPressed(0)) && projectilesManager.bulletSpawnTime > ProjectileManager.tts)
            {
                if (player.bullets == PlayerClass.Bullets.linear)
                {
                    Vector2 v = new Vector2((float)Math.Sin(player.Angle), -(float)Math.Cos(player.Angle));

                    LinearProjectile p = new LinearProjectile(player.GlobalPosition, v, content);
                    projectilesManager.CreateBullet(p);
                }
                else if (player.bullets == PlayerClass.Bullets.circular)
                {
                    CircularProjectile p = new CircularProjectile(player.GlobalPosition, content, player);
                    projectilesManager.CreateBullet(p);
                }
                else
                {
                    if (enemyManager.Count > 0)
                    {

                        HomingProjectile p = new HomingProjectile(player.GlobalPosition, content, CollisionManager.findNearest(player, enemyManager));
                        projectilesManager.CreateBullet(p);
                    }
                }

            }
            

            /*
            if ( (joystickState.IsPressed(4) || joystickState.IsPressed(0)) && projectilesManager.bulletSpawnTime > ProjectileManager.tts)
            {
                Vector2 v = new Vector2((float)Math.Sin(player.Angle), -(float)Math.Cos(player.Angle));

                LinearProjectile p = new LinearProjectile(player.GlobalPosition, v, content);
                projectilesManager.CreateBullet(p);
            }
            if ( ( joystickState.IsPressed(1)) && projectilesManager.bulletSpawnTime > ProjectileManager.tts)
            {
                CircularProjectile p = new CircularProjectile(player.GlobalPosition, content, player);
                projectilesManager.CreateBullet(p);
            }
            if ( (joystickState.IsPressed(5)) && projectilesManager.bulletSpawnTime > ProjectileManager.tts)
            {
                if (enemyManager.Count > 0)
                {

                    HomingProjectile p = new HomingProjectile(player.GlobalPosition, content, CollisionManager.findNearest(player, enemyManager));
                    projectilesManager.CreateBullet(p);
                }
            }
             */ 
        }

        private void createItems()
        {
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.D1))
            {
                Health i = new Health(100, player, new Vector2(player.GlobalPosition.X + 300, player.GlobalPosition.Y), content);
                itemManager.CreateItem(i);
            }
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.D2))
            {
                Shield i = new Shield(100, player, new Vector2(player.GlobalPosition.X, player.GlobalPosition.Y - 300), content);
                itemManager.CreateItem(i);
            }
        }

        private void checkCollision()
        {
            ///checa colisão entre todos os tiros e inimigos
            for (int i = 0; i < projectilesManager.Count; i++)
            {
                for (int j = 0; j < enemyManager.Count; j++)
                {

                    if (CollisionManager.CircularCollision(projectilesManager.ElementAt(i), enemyManager.ElementAt(j)))
                    {
                        //cria uma explosão diferente pra cada tipo de inimigo
                        if (enemyManager.ElementAt(j) is Sun)
                        {
                            AnimatedExplosion e = new AnimatedExplosion(projectilesManager.ElementAt(i).GlobalPosition, content);
                            explosionManager.createExplosion(e);
                        }

                        else if (enemyManager.ElementAt(j) is Chaser)
                        {
                            MultipleExplosion e = new MultipleExplosion(projectilesManager.ElementAt(i).GlobalPosition, content);
                            explosionManager.createExplosion(e);
                        }

                        else
                        {
                            SimpleExplosion e = new SimpleExplosion(projectilesManager.ElementAt(i).GlobalPosition, content);
                            explosionManager.createExplosion(e);
                        }

                        projectilesManager.RemoveAt(i);
                        enemyManager.RemoveAt(j);
                        i--;
                        break;
                    }
                }
            }


            for (int i = 0; i < itemManager.Count; i++)
            {
                if (CollisionManager.CircularCollision(player, itemManager.ElementAt(i)))
                {
                    ItemClass item = itemManager.ElementAt(i);
                    if (item is Shield)
                    {
                        player.inventory[0]++;
                    }
                    else if (item is Health)
                    {
                        player.inventory[1]++;
                    }
                    itemManager.destroyItem(item);
                    i--;
                }
            }

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
