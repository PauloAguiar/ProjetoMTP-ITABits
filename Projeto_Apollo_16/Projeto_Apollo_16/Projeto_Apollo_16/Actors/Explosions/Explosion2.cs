using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public class Explosion2 : ExplosionClass
    {
         int i, j;
         const double delay = 0.005;
         double time;
         int x;
         public Explosion2 (Vector2 position, ContentManager content) : base(position, content)
        {
            Lifetime = 25;
            i = j = 0;
            time = 0;
            x = 1;
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\explosionsheet");
        }


        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");

        }

        public override void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle r = new Rectangle (i,j,64,64);
            spriteBatch.Draw(texture, globalPosition, r, Color.White);
            if (time > delay * x)
            {
                i = (i + 64) % 320;
                j = (x / 4) * 64;
                x++;
            }

        }
    }
}
