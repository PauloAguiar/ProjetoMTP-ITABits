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
    public class Explosion : ActorClass
    {
        public int Lifetime { get; protected set; }
        bool isActive;

        public Explosion(Vector2 position)
        {
            Lifetime = 1000;
            isActive = true;
            globalPosition = position;
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\explosion");
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void Update(GameTime gameTime)
        {
            if (Lifetime > 0)
            {
                isActive = true;
            }
            Lifetime--;
            if (Lifetime < 0)
            {
                isActive = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.Draw(texture, globalPosition, Color.White);
            }
        }


    }
}
