using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Lidgren.Network;

namespace Projeto_Apollo_16
{
    public class ShooterDataClass
    {
        /* Os membros que contém underline contém dados obtidos diretamente da classe player */
        public const int NUMBER_TYPE_BULLETS = 5;
        public int[] _ammo = new int[NUMBER_TYPE_BULLETS];

        public ShooterDataClass(int[] ammo)
        {
            this._ammo = ammo;
        }

        public void EncodeShooterData(NetOutgoingMessage outmsg)
        {
            for (int i = 0; i < NUMBER_TYPE_BULLETS; i++)
			{
                outmsg.Write(_ammo[i]); 
			}
            
        }


    }
}
