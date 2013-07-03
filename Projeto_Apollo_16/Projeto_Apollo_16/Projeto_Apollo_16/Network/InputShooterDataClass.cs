using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Projeto_Apollo_16
{
    enum ButtonStatesShooter
    {
        BTN_1 = 0,
        BTN_2,
        BTN_3,
        BTN_4,
    }

    enum StickPositionShooter
    {
        X_AXIS = 0,
        Y_AXIS,
    }

    public class InputShooterDataClass
    {
        public bool[] buttons = new bool[4];
        public Int32[] position = new Int32[2];

        public InputShooterDataClass()
        {
        }


        public void Decode(NetIncomingMessage incmsg)
        {
            for (int i = 0; i < 4; i++)
            {
                this.buttons[i] = incmsg.ReadBoolean();
            }
            for (int i = 0; i < 2; i++)
            {
                this.position[i] = incmsg.ReadInt32();
            }

        }

        public void Encode(NetOutgoingMessage outmsg)
        {
            for (int i = 0; i < 4; i++)
            {
                outmsg.Write(this.buttons[i]);
            }
            for (int i = 0; i < 2; i++)
            {
                outmsg.Write(this.position[i]);
            }
        }

    }
}
