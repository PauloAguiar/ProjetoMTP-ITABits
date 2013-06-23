using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public static partial class GameLogic
    {
        public static double timeCreateEnemies;
        const int MIN_TIME_CREATE_ENEMIES = 10000;
        const int MAX_TIME_CREATE_ENEMIES = 20000;
        const int MIN_NUMBER_ENEMIES = 50;
        const int MAX_NUMBER_ENEMIES = 100;
        static int numberEnemies;
        static EnemyManager enemyManager;

        private static void createEnemies()
        {
            numberEnemies = rand.Next(MIN_NUMBER_ENEMIES, MAX_NUMBER_ENEMIES);
            for (int i = 0; i < numberEnemies; i++)
            {
                int j = rand.Next(3);
                int x = rand.Next(-MAX_MAP_SIZE, 2 * MAX_MAP_SIZE);
                int y = rand.Next(-MAX_MAP_SIZE, 2 * MAX_MAP_SIZE);

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

        static void checkTimeEnemies(double dt)
        {
            timeCreateEnemies -= dt;
            if (timeCreateEnemies <= 0)
            {
                createEnemies();
                timeCreateEnemies = rand.Next(MIN_TIME_CREATE_ENEMIES, MAX_TIME_CREATE_ENEMIES);
            }
        }

    }
}
