using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16.Actors
{
    class Shoot : ActorClass
    {

        public bool exists = false;
        public Shoot(Vector2 position)
        {
            globalPosition = position;

        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\shoot");
        }

        public override void LoadFont(ContentManager content)
        {
            throw new NotImplementedException();
        }
        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }




        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, globalPosition, Color.White);
        }

    }
}
