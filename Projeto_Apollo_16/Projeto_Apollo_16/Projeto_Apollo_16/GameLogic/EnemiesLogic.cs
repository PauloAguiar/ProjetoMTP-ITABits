using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public static partial class GameLogic
    {
        public static double timeCreateEnemies;
        private const double INITIAL_TIME_CREATE_ENEMIES = 7000;
        private const double MIN_TIME_CREATE_ENEMIES = 5000;
        private static double actualTimeCreateEnemies = INITIAL_TIME_CREATE_ENEMIES;
        private static double deltaTimeCreateEnemies = 1000;
        //const int MIN_TIME_CREATE_ENEMIES = 10000;
        //const int MAX_TIME_CREATE_ENEMIES = 20000;
        const int MIN_NUMBER_ENEMIES = 50;
        const int MAX_NUMBER_ENEMIES = 100;
        static int numberEnemies;
        static EnemyManager enemyManager;

        private static void CreateEnemies()
        {
            numberEnemies = rand.Next(MIN_NUMBER_ENEMIES, MAX_NUMBER_ENEMIES);
            for (int i = 0; i < numberEnemies; i++)
            {
                int j = rand.Next(EnemyClass.numberTypeEnemies-1);  //tem que tirar o -1, porque é só pra não pegar o Chaser
                int x = rand.Next(-MAX_MAP_SIZE, 2 * MAX_MAP_SIZE);
                int y = rand.Next(-MAX_MAP_SIZE, 2 * MAX_MAP_SIZE);

                if (j == (int)EnemyClass.Enemies.Ghost)
                {
                    Ghost ghost = new Ghost(new Vector2(x, y), content);
                    enemyManager.createEnemy(ghost);
                }
                else if (j == (int)EnemyClass.Enemies.Polygon)
                {
                    Poligon poligon = new Poligon(new Vector2(x, y), content);
                    enemyManager.createEnemy(poligon);
                }
                else if (j == (int)EnemyClass.Enemies.Sun)
                {
                    Sun sun = new Sun(new Vector2(x, y), content);
                    enemyManager.createEnemy(sun);
                }
                else if (j == (int)EnemyClass.Enemies.Chaser)
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
                CreateEnemies();
                actualTimeCreateEnemies -= deltaTimeCreateEnemies;
                if (actualTimeCreateEnemies < 5000)
                {
                    actualTimeCreateEnemies = 5000;
                }
                timeCreateEnemies = actualTimeCreateEnemies;
                
                //timeCreateEnemies = rand.Next(MIN_TIME_CREATE_ENEMIES, MAX_TIME_CREATE_ENEMIES);
            }
        }

    }
}
