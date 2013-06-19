using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Lidgren.Network;

namespace Apollo_16_Piloto
{
    public class PilotDataClass
    {
        /* Os membros que contém underline contém dados obtidos diretamente da classe player */
        public float _throttle;
        public float _speed;
        public float _angle;
        public Vector2 _velocity;

        public PilotDataClass(NetIncomingMessage msg)
        {
            Decode(msg);
        }

        public PilotDataClass(float throttle, float speed, float angle, Vector2 velocity)
        {
            this._throttle = throttle;
            this._speed = speed;
            this._angle = angle;
            this._velocity = velocity;
        }

        
        private void Decode(NetIncomingMessage incmsg)
        {
            this._throttle = incmsg.ReadFloat();
            this._speed = incmsg.ReadFloat();
            this._angle = incmsg.ReadFloat();
            this._velocity.X = incmsg.ReadFloat();
            this._velocity.Y = incmsg.ReadFloat();
        }

    }
}
