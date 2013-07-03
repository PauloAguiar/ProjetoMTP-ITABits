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
        USABLE
    }

    class ItemClass
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
    }
}
