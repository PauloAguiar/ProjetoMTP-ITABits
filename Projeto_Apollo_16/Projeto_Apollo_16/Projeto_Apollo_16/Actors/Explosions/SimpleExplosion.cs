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
    class ExplosionSimple : ExplosionClass
    {
        public int Lifetime;
        public bool isActive;
        
        //bug de ficar desenhando várias vezes a mesma explosion
        //public bool drawed;

        public ExplosionSimple (Vector2 position) : base(position)
        {

        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\explosion");
        }

    }
}
