using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Input = Microsoft.Xna.Framework.Input;
using SlimDX.DirectInput;

namespace Projeto_Apollo_16
{
    public sealed partial class PlayerClass : ActorClass
    {
        public const int MIN_TIME_CHANGE_ITEM = 300;
        public static double timeChangedItem = MIN_TIME_CHANGE_ITEM;
        public const int MIN_TIME_USE_ITEM = 300;
        public static double timeUsedItem = MIN_TIME_USE_ITEM;

        public enum Items
        {
            Shield = 0,
            Health,
        }
        private static int NUMBER_TYPE_ITEMS = Enum.GetNames(typeof(Items)).Length;
        public int[] inventory = new int[NUMBER_TYPE_ITEMS];
        public static Items items = Items.Shield;
        void UpdateInventory(double dt)
        {
            timeChangedItem += dt;
            timeUsedItem += dt;
        }

        public void UseItem()
        {
            timeUsedItem = 0;
            if (items == Items.Health)
            {
                if (inventory[(int)items] > 0)
                {
                    Life += 40;
                    inventory[(int)Items.Health]--;
                }
            }
            else if (items == Items.Shield)
            {
                if (inventory[(int)items] > 0)
                {
                    damageTime = 0;
                    inventory[(int)Items.Shield]--;
                }
            }
        }

        public static void ShiftItemsRight()
        {
            timeChangedItem = 0;
            items = (Items)(((int)items + 1) % NUMBER_TYPE_ITEMS);
        }

        public static void ShiftItemsLeft()
        {
            timeChangedItem = 0;
            items = (Items)(((int)items - 1 + NUMBER_TYPE_ITEMS) % NUMBER_TYPE_ITEMS);
        }


    }
}
