using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Projeto_Apollo_16
{
    public class CopilotDataClass
    {
        List<ItemClass> invList = new List<ItemClass>();

        public CopilotDataClass(List<ItemClass> invList)
        {
            this.invList = invList;
        }

        public void EncodeCopilotData(NetOutgoingMessage outmsg)
        {
            outmsg.Write((Int32)invList.Count);

            foreach (ItemClass i in invList)
            {
                outmsg.Write((Int32)i.amount);
                outmsg.Write((Byte)i.type);
                General.Log("Type" + (Byte)i.type + " Amount" + i.amount);
            }
        }
    }
}
