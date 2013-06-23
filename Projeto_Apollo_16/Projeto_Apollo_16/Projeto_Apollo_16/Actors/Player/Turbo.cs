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
        Texture2D turboTexture;
        Vector2 turboPosition;
        Vector2 turboBackPosition;
        float turboAngle;

        void LoadTurboTexture(ContentManager content)
        {
            turboTexture = content.Load<Texture2D>(@"Sprites\Nave\hadouken");
        }

        void DrawTurbo(SpriteBatch spriteBatch)
        {
            if (Speed > 0)
            {
                spriteBatch.Draw(turboTexture, turboPosition, null, Color.White, (float)turboAngle, new Vector2(turboTexture.Width / 2, turboTexture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
            }
            else if (Speed < 0)
            {
                spriteBatch.Draw(turboTexture, turboBackPosition, null, Color.White, (float)turboAngle, new Vector2(turboTexture.Width / 2, turboTexture.Height / 2), 1.0f, SpriteEffects.None, Globals.PLAYER_LAYER);
            }
        }
    }
}
