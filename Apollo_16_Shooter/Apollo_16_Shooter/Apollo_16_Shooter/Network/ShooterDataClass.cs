using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Lidgren.Network;

namespace Apollo_16_Shooter
{
    public class ShooterDataClass
    {
        /* Os membros que contém underline contém dados obtidos diretamente da classe player */
        public Int32 _ammo;

        public ShooterDataClass(NetIncomingMessage msg)
        {
            Decode(msg);
        }

        public ShooterDataClass(int ammo)
        {
            this._ammo = ammo;
        }

        
        private void Decode(NetIncomingMessage incmsg)
        {
            this._ammo = incmsg.ReadInt32();
        }
    }
}
