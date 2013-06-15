using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public abstract class ExplosionClass : ActorClass
    {
        public int Lifetime;
        public bool isActive;
        
        public ExplosionClass(Vector2 position,ContentManager content)
        {
            isActive = true;
            globalPosition = position;
            this.LoadFont(content);
            this.LoadTexture(content);
            //this.LoadSound(content);
        }
        
        public override void Update(GameTime gameTime)
        {
            if (Lifetime > 0)
            {
                Lifetime--;
            }


            if (Lifetime <= 0)
            {
                isActive = false;
            }

             //só vai tocar 1 vez, e não a cada loop, mas como faz pra tocar 1 vez a cada explosão na explosão multipla???
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, globalPosition, Color.White);   
        }

    }
}
