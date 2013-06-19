using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Lidgren.Network;

namespace Projeto_Apollo_16
{
    public class PilotDataClass
    {
        /* Os membros que contém underline contém dados obtidos diretamente da classe player */
        private float _throttle = 0;
        private float _speed;
        private float _angle;
        private Vector2 _velocity;

        public PilotDataClass(float throttle, float speed, float angle, Vector2 velocity)
        {
            this._throttle = throttle;
            this._speed = speed;
            this._angle = angle;
            this._velocity = velocity;
        }

        public void EncodePilotData(NetOutgoingMessage outmsg)
        {
            outmsg.Write(_throttle);
            outmsg.Write(_speed);
            outmsg.Write(_angle);
            outmsg.Write(_velocity.X);
            outmsg.Write(_velocity.Y);
        }
    }
}
