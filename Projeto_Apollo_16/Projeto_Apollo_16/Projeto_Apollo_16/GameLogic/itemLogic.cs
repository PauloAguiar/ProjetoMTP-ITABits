using Microsoft.Xna.Framework;
using Input = Microsoft.Xna.Framework.Input;

namespace Projeto_Apollo_16
{
    public static partial class GameLogic
    {
        static ItemManager itemManager;

        static void createItem1()
        {
            Health i = new Health(100, player, new Vector2(player.GlobalPosition.X + 300, player.GlobalPosition.Y), content);
            itemManager.CreateItem(i);
        }

        static void createItem2()
        {
            Shield i = new Shield(100, player, new Vector2(player.GlobalPosition.X, player.GlobalPosition.Y - 300), content);
            itemManager.CreateItem(i);
        }
    }
}
