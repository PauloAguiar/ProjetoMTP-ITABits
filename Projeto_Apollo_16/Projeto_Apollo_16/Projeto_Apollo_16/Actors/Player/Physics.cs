using System;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public sealed partial class PlayerClass : ActorClass
    {
        #region engineControl
        private const float MAX_SPEED = 2.0f;
        private const float MIN_SPEED = -0.75f;
        private const float MAX_MAX_THROTTLE = 0.2f;
        private const float MIN_MIN_THROTTLE = -0.1f;
        public float throttle { get; private set; }
        public float Speed { get; private set; }
        public Vector2 Direction { get; private set; }
        //dependem do throttle (alavanca traseira do joystick)
        private float maxThrottle = 0.4f;
        private float minThrottle = -0.2f;
        #endregion

        #region sideMovement
        private const float MAX_SIDE_ACCELERATION = 0.007f;
        private const float MAX_SIDE_SPEED = 0.03f;
        private float sideAcceleration;
        public Vector2 SideDirection { get; private set; }
        public float SideSpeed { get; private set; }
        #endregion

        #region turnMovement
        private const float ANGLE_MULTIPLIER = (float)MathHelper.PiOver2;
        private const float MAX_ANGLE = (float)MathHelper.PiOver4;
        public float Angle { get; private set; }
        #endregion

        private void UpdatePosition(double dt)
        {
            UpdateTangetMovement(dt);
            UpdateSideMovement(dt);
            UpdateTurboPosition();
            UpdateGunPosition();
        }

        private void UpdateTangetMovement(double dt)
        {
            throttle = MathHelper.Clamp(throttle, minThrottle, maxThrottle);
            Speed += throttle;
            Speed = MathHelper.Clamp(Speed, MIN_SPEED, MAX_SPEED);

            Direction = MathFunctions.AngleToVector(Angle);
            Direction.Normalize();

            globalPosition += Direction * Speed * (float)dt;
        }

        void UpdateSideMovement(double dt)
        {
            SideDirection = new Vector2(-Direction.Y, Direction.X);
            sideAcceleration *= Math.Abs(throttle);
            sideAcceleration = MathHelper.Clamp(sideAcceleration, -MAX_SIDE_ACCELERATION, MAX_SIDE_ACCELERATION);
            SideSpeed += sideAcceleration * (float)dt;
            SideSpeed = MathHelper.Clamp(SideSpeed, -MAX_SIDE_SPEED, MAX_SIDE_SPEED);
            globalPosition += SideDirection * (SideSpeed * (float)dt + 0.5f * sideAcceleration * (float)(dt * dt)) * (float)dt;
        }
 
    }
}
