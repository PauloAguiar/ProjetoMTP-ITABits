using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Input = Microsoft.Xna.Framework.Input;

namespace Projeto_Apollo_16
{
    public static partial class GameLogic
    {
        const int MAX_MAP_SIZE = 6000;
        static public Random rand = new Random();
        static ContentManager content;
        static PlayerClass player;
        
        public static void Initialize(ExplosionManager expM, ItemManager iM, ProjectileManager pM, EnemyManager eM, ContentManager cont, PlayerClass plr)
        {
            explosionManager = expM;
            itemManager = iM;
            projectilesManager = pM;
            enemyManager = eM;
            content = cont;
            player = plr;
            timeCreateEnemies = INITIAL_TIME_CREATE_ENEMIES;
            //timeCreateEnemies = rand.Next(MIN_TIME_CREATE_ENEMIES, MAX_TIME_CREATE_ENEMIES);
            CreateEnemies();
            CreateDevice();
        }

        public static void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;
            checkPauseKey(Input.Keyboard.GetState());

            if (!isPaused)
            {
                projectilesManager.Update(gameTime);
                explosionManager.Update(gameTime);
                enemyManager.Update(gameTime);
                itemManager.Update(gameTime);

                checkTimeEnemies(dt);
                player.Update(gameTime, joystickState);
                
                timeChangedWeapon += dt;

                CheckCollision();

                UpdateJoystick();

            }
        }


    }
}
