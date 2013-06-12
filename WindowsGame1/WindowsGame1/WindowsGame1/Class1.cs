using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    public static class Class1
    {
        static int x;
        static SpriteFont font;

        public static void muda(int n)
        {
            x ++;
        }

        public static void loadFont(ContentManager content)
        {
            font = content.Load<SpriteFont>("font");
        }

        public static void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, x.ToString(), new Vector2(30,30), Color.White);

        }
    }

}
