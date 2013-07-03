using Microsoft.Xna.Framework;
using Input = Microsoft.Xna.Framework.Input;

namespace Projeto_Apollo_16
{
    public static partial class GameLogic
    {
        static ItemManager itemManager;

        private static void CreateItem(Vector2 position)
        {
            
            int i = rand.Next(0, 2);
            if (i == 0)
            {
                itemManager.CreateItem(new RepairItem(position, content));
            }
            else if (i == 1)
            {
                itemManager.CreateItem(new FuelItem(position, content));
            }
            //else if (i == 2)
            //{
            //    Fuel fuel = new Fuel(100, player, position, content);
            //    itemManager.CreateItem(fuel);
            //}
            //else if (i == 3)
            //{
            //    AmmoCircular ammo = new AmmoCircular(100,player,position,content);
            //    itemManager.CreateItem(ammo);
            //}
            //else if (i == 4)
            //{
            //    AmmoHoming ammo = new AmmoHoming(100, player, position, content);
            //    itemManager.CreateItem(ammo);
            //}
            //else if (i == 5)
            //{
            //    AmmoArea ammo = new AmmoArea(100, player, position, content);
            //    itemManager.CreateItem(ammo);
            //}
        }

        private static void GetItem(ItemClass item)
        {
            item.Sound.Play();

            player.AddItemToInventory(item);

            itemManager.destroyItem(item);
        }

    }
}
