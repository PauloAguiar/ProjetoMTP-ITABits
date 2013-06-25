
namespace Projeto_Apollo_16
{
    public sealed partial class PlayerClass : ActorClass
    {
        //constants
        /*
        private const float DELTA_THETA = (float)Math.PI / 400;
        private const float EPSILON_SPEED = 0.1f;
        private const float EPSILON_THROTTLE = 0.0001f;
        private const float DELTA_THROTTLE_UP = 0.0001f;
        private const float DELTA_THROTTLE_DOWN = 0.000005f;
         */

        /*
        private void angleInput()
        {
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Left))
            {
                Angle -= DELTA_THETA;
                if (Angle < -MathHelper.TwoPi)
                {
                    Angle += MathHelper.TwoPi;
                }
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Right))
            {
                Angle += DELTA_THETA;
                if (Angle > MathHelper.TwoPi)
                {
                    Angle -= MathHelper.TwoPi;
                }

            }
        }
         */

        /*
        private void velocityInput()
        {
            if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Up))
            {
                throttle += DELTA_THROTTLE_UP;
            }
            else if (Input.Keyboard.GetState().IsKeyDown(Input.Keys.Down))
            {
                throttle -= DELTA_THROTTLE_DOWN;
            }
            else
            {
                throttle *= 1.0f / 2;
                if (Math.Abs(throttle) <= EPSILON_THROTTLE)
                {
                    throttle = 0;
                }
            }
        }
         */

    }
}
