using Microsoft.Xna.Framework;
using Input = Microsoft.Xna.Framework.Input;

namespace Projeto_Apollo_16
{
    public static partial class GameLogic
    {
        static ItemManager itemManager;

        private static void CreateItem(Vector2 position)
        {
            int i = rand.Next(0, 10);
            if (i == 0)
            {
                Health health = new Health(100, player, position, content);
                itemManager.CreateItem(health);
            }
            else if (i == 1)
            {
                Shield shield = new Shield(100, player, position, content);
                itemManager.CreateItem(shield);
            }
            else if (i == 2)
            {
                Fuel fuel = new Fuel(100, player, position, content);
                itemManager.CreateItem(fuel);
            }
            else if (i == 3)
            {
                AmmoCircular ammo = new AmmoCircular(100,player,position,content);
                itemManager.CreateItem(ammo);
            }
            else if (i == 4)
            {
                AmmoHoming ammo = new AmmoHoming(100, player, position, content);
                itemManager.CreateItem(ammo);
            }
            else if (i == 5)
            {
                AmmoArea ammo = new AmmoArea(100, player, position, content);
                itemManager.CreateItem(ammo);
            }
        }

        private static void GetItem(ItemClass item)
        {
            item.Sound.Play();

            if (item is Shield)
            {
                player.inventory[0]++;
            }
            else if (item is Health) 
            {
                player.inventory[1]++;
            }
            else if (item is Fuel)
            {
                player.inventory[2]++;            
            }
            else if (item is AmmoCircular) 
            {
                CircularProjectile.ammo += 40;
            }
            else if (item is AmmoHoming)
            {
                HomingProjectile.ammo += 20;
            }
            else if ( item is AmmoArea)
            {
                AreaProjectile.ammo += 80;
            }

            //player.inventory[(int)PlayerClass.item]++;
            itemManager.destroyItem(item);
        }

    }
}
