using System.Linq;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Input = Microsoft.Xna.Framework.Input;

namespace Projeto_Apollo_16
{
    public static partial class GameLogic
    {
        static void CheckCollision()
        {
            ///checa colisão entre todos os tiros e inimigos
            for (int i = 0; i < projectilesManager.Count; i++)
            {
                for (int j = 0; j < enemyManager.Count; j++)
                {
                    if (CollisionManager.CircularCollision(projectilesManager.ElementAt(i), enemyManager.ElementAt(j)))
                    {
                        enemyManager.ElementAt(j).Destroy(projectilesManager.ElementAt(i).GlobalPosition, content, explosionManager);
                        projectilesManager.RemoveAt(i);
                        enemyManager.RemoveAt(j);
                        i--;
                        break;
                    }
                }
            }

            //checa colisao entre o player e os items
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

    }
}
