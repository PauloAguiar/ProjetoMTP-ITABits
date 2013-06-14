using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public class ExplosionManager : List<ExplosionClass>
    {
        static ContentManager content;
        static SystemClass systemRef;
        
        public ExplosionManager(Game game)
            : base()
        {
            systemRef = (SystemClass)game;
            content = new ContentManager(systemRef.Content.ServiceProvider, systemRef.Content.RootDirectory);
        }

        public void createExplosion(ExplosionClass e)
        {
            this.Add(e);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < this.Count; i++)
            {
                ExplosionClass e = this.ElementAt(i);

                e.Update(gameTime);

                
                
                if (!e.isActive)
                {
                    this.RemoveAt(i);
                    i++;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ExplosionClass e in this)
            {
                e.Draw(spriteBatch);
            }
        }


    }
}
