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
        public List<ItemClass> inventoryList = new List<ItemClass>(24);

        public void AddItemToInventory(ItemClass item)
        {
            foreach (ItemClass i in inventoryList)
            {
                if (item.type == i.type)
                {
                    i.amount += item.amount;
                    General.Log("Item amount of " + (int)item.type + " increase to " + i.amount);
                    return;
                }
                
            }
            General.Log("Item added to inventory: " + (int)item.type);
            inventoryList.Add(item);
        }

        public CopilotDataClass GetCopilotImmediateData()
        {
            CopilotDataClass copilotData = new CopilotDataClass(inventoryList);
            return copilotData;
        }
    }
}
