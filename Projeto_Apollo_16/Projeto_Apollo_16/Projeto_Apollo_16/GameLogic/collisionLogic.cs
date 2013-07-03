using System.Linq;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Input = Microsoft.Xna.Framework.Input;

namespace Projeto_Apollo_16
{
    public static partial class GameLogic
    {
        //static Texture2D ponto = content.Load<Texture2D>(@"Sprites\Shoots\ponto");

        static void CheckCollision()
        {
            //checa colisão entre todos os tiros e inimigos
            for (int i = 0; i < projectilesManager.Count; i++)
            {
                for (int j = 0; j < enemyManager.Count; j++)
                {
                    if (projectilesManager.ElementAt(i) is LightSaber)
                    {
                        
                        LightSaber lightSaber = (LightSaber)projectilesManager.ElementAt(i);
                        if( CollisionManager.LightSaberCollision(lightSaber, enemyManager.ElementAt(j)))
	                    {
                            CreateItem(enemyManager.ElementAt(j).GlobalPosition);
                            enemyManager.GetHit(player.GlobalPosition, j, projectilesManager.ElementAt(i).GlobalPosition, explosionManager);
                            projectilesManager.destroyBullet(projectilesManager.ElementAt(i));

                            i--;
                            break;    
	                    }
                        
                        
                    }

                    if (!(projectilesManager.ElementAt(i) is LightSaber) &&  CollisionManager.CircularCollision(projectilesManager.ElementAt(i), enemyManager.ElementAt(j)))
                    {
                        CreateItem(enemyManager.ElementAt(j).GlobalPosition);
                        enemyManager.GetHit(player.GlobalPosition, j, projectilesManager.ElementAt(i).GlobalPosition, explosionManager);
                        //enemyManager.DestroyEnemy(j, projectilesManager.ElementAt(i).GlobalPosition, explosionManager);
                        projectilesManager.destroyBullet(projectilesManager.ElementAt(i));
                        
                        i--;
                        break;
                    }
                }
            }

            //checa colisao entre o player e os items
            for (int i = 0; i < itemManager.Count; i++)
            {
                if (CollisionManager.ItemCollision(itemManager.ElementAt(i), player))
                {
                    GetItem(itemManager.ElementAt(i));
                    i--;
                }
            }

            //checa colisão entre o player e os inimigos
            for (int i = 0; i < enemyManager.Count; i++)
            {
                if (CollisionManager.CircularCollision(player, enemyManager.ElementAt(i)))
                {
                    player.UpdateLifeCollision();
                    //enemyManager.destroyEnemy(i, enemyManager.ElementAt(i).GlobalPosition, explosionManager);
                    //i--;
                }
            }

        }

    }
}
