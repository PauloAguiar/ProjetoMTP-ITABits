using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public class Shield : ItemClass
    {
        private const double lifeTime = 5000;
        private double timeLiving;
        
        public Shield(int health, PlayerClass player, Vector2 position, ContentManager content) : base (player, position, content)
        {
            //this.name = name;
            name = "shield";
            ttl = 10000;
            timeLiving = 0;
            IsUsing = true;

        }

        public override void  Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timeLiving += gameTime.ElapsedGameTime.TotalMilliseconds;
            if(timeLiving >= lifeTime)
            {
                IsUsing = false;
                //destroy
            }
            
        }

        public override void  LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\items\shield");
        }
    }
}
