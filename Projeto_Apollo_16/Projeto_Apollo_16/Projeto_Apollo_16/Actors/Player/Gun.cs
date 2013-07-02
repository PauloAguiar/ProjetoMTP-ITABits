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
        Texture2D gun;
        Vector2 gunPosition;

        void LoadGunTexture(ContentManager content)
        {
            gun = content.Load<Texture2D>(@"Sprites\Nave\canon");
        }

        void UpdateGunPosition()
        {
            gunPosition = globalPosition;
        }

        void DrawGun(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gun, gunPosition, null, Color.White, (float)Angle, new Vector2(gun.Width / 2, gun.Height / 2), 1.0f, SpriteEffects.None, Globals.GUN_LAYER);
        }
    }
}
