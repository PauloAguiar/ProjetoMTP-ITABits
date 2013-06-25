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
        public enum Items
        {
            Shield = 0,
            Health,
        }
        const int NUMBER_TYPE_ITEMS = 2;
        public int[] inventory = new int[NUMBER_TYPE_ITEMS];

        void UpdateInventory()
        {
            while (inventory[1] > 0)
            {
                Life += 10;
                inventory[1]--;
            }

            UpdateInventoryInput();
        }

        void UpdateInventoryInput()
        {
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.D0) && inventory[0] > 0)
            {
                Life += 40;
                inventory[0]--;
            }
        }

    }
}
