using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Apollo_16_Radar
{
    public class InputDataClass
    {
        public bool[] buttons = new bool[7];
        public Int32[] position = new Int32[3];
        public Int32 rotationZ;
        public byte pov;

        public InputDataClass()
        {
        }
        
        
        private void Decode(NetIncomingMessage incmsg)
        {
            for (int i = 0; i < 7; i++)
            {
                this.buttons[i] = incmsg.ReadBoolean();
            }
            for (int i = 0; i < 3; i++)
            {
                this.position[i] = incmsg.ReadInt32();
            }
            rotationZ = incmsg.ReadInt32();
            pov = incmsg.ReadByte();

        }

        public void Encode(NetOutgoingMessage outmsg)
        {
            for (int i = 0; i < 7; i++)
            {
                outmsg.Write(this.buttons[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                outmsg.Write(this.position[i]);
            }
            outmsg.Write(rotationZ);
            outmsg.Write(pov);

        }
    }
}
