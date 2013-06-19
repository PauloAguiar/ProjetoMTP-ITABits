using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Apollo_16_Piloto
{
    public class InputDataClass
    {
        public bool spaceBar = false;

        public InputDataClass()
        {
        }

        public InputDataClass(bool spaceBar)
        {
            this.spaceBar = spaceBar;
        }

        
        private void Decode(NetIncomingMessage incmsg)
        {
            this.spaceBar = incmsg.ReadBoolean();
        }

        public void Encode(NetOutgoingMessage outmsg)
        {
            outmsg.Write(this.spaceBar);
        }
    }
}
