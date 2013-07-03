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
        public int[] _ammo = new int[ShooterClass.NUMBER_TYPE_BULLETS];

        public ShooterDataClass(NetIncomingMessage msg)
        {
            Decode(msg);
        }

        public ShooterDataClass(int[] ammo)
        {
            this._ammo = ammo;
        }

        
        private void Decode(NetIncomingMessage incmsg)
        {
            for (int i = 0; i < ShooterClass.NUMBER_TYPE_BULLETS; i++)
            {
                _ammo[i] = incmsg.ReadInt32();
            }

        }

    }
}
