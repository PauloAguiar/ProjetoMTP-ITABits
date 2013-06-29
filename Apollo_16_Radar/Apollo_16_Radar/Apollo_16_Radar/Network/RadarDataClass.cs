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
            General.Log("Number of enemies: " + size);
            playerGlobalPosition.X = incmsg.ReadFloat();
            playerGlobalPosition.Y = incmsg.ReadFloat();
            enemies = new List<EnemyClass>();
            for (int i = 0; i < size; i++)
            {
                Int32 type = incmsg.ReadInt32();
                float X = incmsg.ReadFloat();
                float Y = incmsg.ReadFloat();
                enemies.Add(new EnemyClass(type, new Vector2(X, Y)));
                General.Log("Enemy of type: " + type + " spotted at: " + X + ", " + Y);
            }
        }
    }
}
