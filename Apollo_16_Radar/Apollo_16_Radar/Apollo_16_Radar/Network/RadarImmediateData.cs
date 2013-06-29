using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Lidgren.Network;

namespace Apollo_16_Radar
{
    public class RadarImmediateData
    {
        public float playerAngle;

        public void DecodeRadarImmediateData(NetIncomingMessage incmsg)
        {
            playerAngle = incmsg.ReadFloat();
        }
    }
}
