using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public static partial class GameLogic
    {
        public static double timeCreateEnemies;
        private const double INITIAL_TIME_CREATE_ENEMIES = 50000;
        private const double MIN_TIME_CREATE_ENEMIES = 5000;
        private static double actualTimeCreateEnemies = INITIAL_TIME_CREATE_ENEMIES;
        private static double deltaTimeCreateEnemies = 500;
        //const int MIN_TIME_CREATE_ENEMIES = 10000;
        //const int MAX_TIME_CREATE_ENEMIES = 20000;
        const int MIN_NUMBER_ENEMIES_CREATE = 50;
        const int MAX_NUMBER_ENEMIES_CREATE = 100;
        static int numberEnemies;
        static EnemyManager enemyManager;

        private static void CreateEnemies()
        {
            numberEnemies = rand.Next(MIN_NUMBER_ENEMIES_CREATE, MAX_NUMBER_ENEMIES_CREATE);
            for (int i = 0; i < numberEnemies; i++)
            {
                Vector2 enemyPosition;
                int j, x, y;
                do
                {
                    j = rand.Next(EnemyClass.NUMBER_TYPE_ENEMIES);  //tem que tirar o -1, porque é só pra não pegar o Chaser
                    x = rand.Next(-MAX_MAP_SIZE, 2 * MAX_MAP_SIZE);
                    y = rand.Next(-MAX_MAP_SIZE, 2 * MAX_MAP_SIZE);
                    enemyPosition = new Vector2(x, y);
                } while (CollisionManager.CanCreateEnemy(enemyPosition, player));


                if (j == (int)EnemyClass.Enemies.Ghost)
                {
                    Ghost ghost = new Ghost(enemyPosition, content);
                    enemyManager.CreateEnemy(ghost);
                }
                else if (j == (int)EnemyClass.Enemies.Polygon)
                {
                    Poligon poligon = new Poligon(enemyPosition, content);
                    enemyManager.CreateEnemy(poligon);
                }
                else if (j == (int)EnemyClass.Enemies.Sun)
                {
                    Sun sun = new Sun(enemyPosition, content);
                    enemyManager.CreateEnemy(sun);
                }
                else if (j == (int)EnemyClass.Enemies.Chaser)
                {
                    Chaser chaser = new Chaser(enemyPosition, content, player);
                    enemyManager.CreateEnemy(chaser);
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
