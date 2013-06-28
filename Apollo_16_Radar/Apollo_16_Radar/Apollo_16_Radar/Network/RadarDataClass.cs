using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Lidgren.Network;

namespace Apollo_16_Radar
{
    public class RadarDataClass
    {
        public Int32 size;
        public Vector2 playerGlobalPosition;
        public List<EnemyClass> enemies;

        public RadarDataClass()
        {
            enemies = new List<EnemyClass>();
        }

        public void DecodeRadarData(NetIncomingMessage incmsg)
        {
            size = incmsg.ReadInt32();
            for (int i = 0; i < size; i++)
            {
                enemies.Add(new EnemyClass(incmsg.ReadInt32(), new Vector2(incmsg.ReadFloat(),incmsg.ReadFloat())));
            }
        }
    }
}
