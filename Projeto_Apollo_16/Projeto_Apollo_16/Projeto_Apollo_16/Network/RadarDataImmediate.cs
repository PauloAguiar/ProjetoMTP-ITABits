using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Lidgren.Network;

namespace Projeto_Apollo_16
{
    public class RadarDataImmediate
    {
        public float playerAngle;

        public RadarDataImmediate(float playerAngle)
        {
            this.playerAngle = playerAngle;
        }

        public void EncodeRadarImmediateData(NetOutgoingMessage outmsg)
        {
            outmsg.Write(playerAngle);
        }

    }
}
