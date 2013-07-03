using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Apollo_16_Copiloto
{
    public enum ItemType
    {
        PRIMARY_WEAPON,
        SECONDARY_WEAPON,
        SHIELD,
        HULL,
        ENGINE,
        REPAIR,
        FUEL
    }

    public class ItemClass
    {
        public String name;
        public Texture2D texture;
        public Int32 amount;
        public ItemType type;
        public Boolean countable;
        public ItemClass(String name, Int32 amount, ItemType type, Boolean countable)
        {
            this.name = name;
            this.amount = amount;
            this.type = type;
            this.countable = countable;
        }

        public ItemClass(Int32 amount, Byte type, SystemClass systemRef)
        {
            if (type == (Byte)ItemType.REPAIR)
            {
                this.name = "Repair";
                this.type = ItemType.REPAIR;
                this.amount = amount;
                this.countable = true;
                this.texture = systemRef.gamePlayScreen.repair;
            }
            else if (type == (Byte)ItemType.FUEL)
            {
                this.name = "Fuel";
                this.type = ItemType.FUEL;
                this.amount = amount;
                this.countable = true;
                this.texture = systemRef.gamePlayScreen.fuel;
            }
        }
    }
}
