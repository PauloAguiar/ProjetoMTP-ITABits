using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Input = Microsoft.Xna.Framework.Input;
using SlimDX.DirectInput;

namespace Projeto_Apollo_16
{
    public static class GameLogic
    {
        static public Random rand = new Random();
        static int numberEnemies;
        const int maxMapSize = 6000;
        const int minNumberEnemies = 50;
        const int maxNumberEnemies = 100;
        static double timeCreateEnemies;
        static EnemyManager enemyManager;
        static ContentManager content;
        static PlayerClass player;

        public static void Initialize(EnemyManager eM, ContentManager cont, PlayerClass plr)
        {
            enemyManager = eM;
            content = cont;
            player = plr;
            createEnemies();
        }

        private static void createEnemies()
        {
            numberEnemies = rand.Next(minNumberEnemies, maxNumberEnemies);
            for (int i = 0; i < numberEnemies; i++)
            {
                int j = rand.Next(3);
                int x = rand.Next(-maxMapSize, maxMapSize);
                int y = rand.Next(-maxMapSize, 2*maxMapSize);

                if (j == 0)
                {
                    Ghost ghost = new Ghost(new Vector2(x, y), content);
                    enemyManager.createEnemy(ghost);
                }
                else if (j == 1)
                {
                    Poligon poligon = new Poligon(new Vector2(x, y), content);
                    enemyManager.createEnemy(poligon);
                }
                else if (j == 2)
                {
                    Sun sun = new Sun(new Vector2(x, y), content);
                    enemyManager.createEnemy(sun);
                }
                else
                {
                    Chaser chaser = new Chaser(new Vector2(x, y), content, player);
                    enemyManager.createEnemy(chaser);
                }

            }
        }

        public static void Update()
        {

        }
    }
}
