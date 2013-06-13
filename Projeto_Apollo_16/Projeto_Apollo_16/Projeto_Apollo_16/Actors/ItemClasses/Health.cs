using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public class Health : ItemClass
    {

        private int heath;


        public Health(int health, PlayerClass player, Vector2 position, ContentManager content) : base (player, position, content)
        {
            this.heath = health;
            //this.name = name;
            name = "life";
            ttl = 10000;

        }

        /*
        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        */

    }
}
