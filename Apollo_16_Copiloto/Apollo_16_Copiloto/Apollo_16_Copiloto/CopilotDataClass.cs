using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Apollo_16_Copiloto
{
    public class CopilotDataClass
    {
        Int32 size;
        public List<ItemClass> inventory;

        public CopilotDataClass()
        {
            inventory = new List<ItemClass>();
        }

        public void DecodeCopilotData(NetIncomingMessage incmsg, SystemClass systemRef)
        {
            size = incmsg.ReadInt32();
            General.Log("SIze: " + size);
            for (int i = 0; i < size; i++)
            {
                Int32 amount = incmsg.ReadInt32();
                Byte type = incmsg.ReadByte();
                inventory.Add(new ItemClass(amount, type, systemRef));
                General.Log("Amount: " + amount + " Type: " + type);
            }

        }
    }
}
