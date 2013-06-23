using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Input = Microsoft.Xna.Framework.Input;
using SlimDX.DirectInput;

namespace Projeto_Apollo_16
{
    public sealed partial class PlayerClass : ActorClass
    {
        public int Life { get; private set; }
        const int MAX_LIFE = 300;
        const int MIN_LIFE = 0;

        public float Fuel { get; private set; }
        const int MAX_FUEL = 100;
        const int MIN_FUEL = 0;

        void initializeStats()
        {
            Life = 100;
            Fuel = 100;
        }

        void UpdateStats(double dt)
        {
            Life = (int)MathHelper.Clamp(Life, MIN_LIFE, MAX_LIFE);

            Fuel -= Math.Abs(throttle) * (float)dt;
            Fuel = MathHelper.Clamp(Fuel, MIN_FUEL, MAX_FUEL);
        }

        void DrawStats(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, "Life = " + Life.ToString(), globalPosition, Color.White);
            spriteBatch.DrawString(spriteFont, "Fuel = " + Fuel.ToString(), globalPosition + new Vector2(0, 50), Color.White);
        }
    }
}
