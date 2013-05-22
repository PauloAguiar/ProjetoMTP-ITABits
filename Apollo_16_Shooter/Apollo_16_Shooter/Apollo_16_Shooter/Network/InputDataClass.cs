using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Apollo_16_Shooter
{
    public class InputDataClass
    {
        public bool[] buttons = new bool[4];
        public Int32[] position = new Int32[2];

        public InputDataClass()
        {
        }

        private void Decode(NetIncomingMessage incmsg)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                this.buttons[i] = incmsg.ReadBoolean();
            }
            for (int i = 0; i < position.Length; i++)
            {
                this.position[i] = incmsg.ReadInt32();
            }
        }

        public void Encode(NetOutgoingMessage outmsg)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                outmsg.Write(this.buttons[i]);
            }
            for (int i = 0; i < position.Length; i++)
            {
                outmsg.Write(this.position[i]);
            }
        }

    }
}
