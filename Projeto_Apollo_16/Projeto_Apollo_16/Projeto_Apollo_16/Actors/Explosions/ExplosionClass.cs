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
        public int Lifetime;
        public bool isActive;
        
        //bug de ficar desenhando várias vezes a mesma explosion
        //public bool drawed;

        public Explosion(Vector2 position)
        {
            isActive = true;
            globalPosition = position;
            Lifetime = 100;
            //drawed = false;
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

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //if(!drawed)
            spriteBatch.Draw(texture, globalPosition, Color.White);

            //drawed = true;
            
        }


    }
}
