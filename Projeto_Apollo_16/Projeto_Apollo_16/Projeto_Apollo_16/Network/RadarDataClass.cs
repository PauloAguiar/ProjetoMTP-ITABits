using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Lidgren.Network;

namespace Projeto_Apollo_16
{
    public class RadarDataClass
    {
        public Int32 size;
        public Vector2 playerGlobalPosition;
        public List<EnemyClass> enemies;

        public RadarDataClass(List<EnemyClass> enemies, Vector2 playerGlobalPosition)
        {
            size = enemies.Count;
            this.enemies = enemies;
            this.playerGlobalPosition = playerGlobalPosition;
        }

        public void EncodeRadarData(NetOutgoingMessage outmsg)
        {
            outmsg.Write(size);
            outmsg.Write(playerGlobalPosition.X);
            outmsg.Write(playerGlobalPosition.Y);
            foreach (EnemyClass enemy in enemies)
            {
                outmsg.Write(enemy.type);
                outmsg.Write(enemy.GlobalPosition.X);
                outmsg.Write(enemy.GlobalPosition.Y);
            }
        }

    }
}
