using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Projeto_Apollo_16
{
    static class Smoke : ActorClass
    {
        static Texture2D SmokeTexture;
        static List<Vector2> SmokeList = new List<Vector2>();

        public override void LoadTexture(ContentManager content)
        {
            SmokeTexture = content.Load<Texture2D>(@"Sprites\Shoots\smoke");
        }

        private void CreateSmoke()
        {

            for (int i = 0; i < 5; i++)
            {
                Vector2 smokePosition = globalPosition;
                smokePosition.X -= GameLogic.rand.Next(2, 10);
                smokePosition.Y -= GameLogic.rand.Next(2, 10);
                SmokeList.Add(smokePosition);
            }

        }



        private static void DrawSmoke(SpriteBatch spriteBatch)
        {
            foreach (Vector2 v in SmokeList)
            {
                spriteBatch.Draw(SmokeTexture, v, Color.White);
            }
        }

    }
}
