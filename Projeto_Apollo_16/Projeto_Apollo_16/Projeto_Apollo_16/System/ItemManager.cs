using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public class ItemManager : List<ItemClass>
    {
        static ContentManager content;
        static SystemClass systemRef;

        public double bulletSpawnTime { get; private set; }

        public ItemManager(Game game)
            : base()
        {
            systemRef = (SystemClass)game;
            content = new ContentManager(systemRef.Content.ServiceProvider, systemRef.Content.RootDirectory);
        }

        public void CreateItem(ItemClass item)
        {
            this.Add(item);
            bulletSpawnTime = 0;
        }


        public void destroyItem(ItemClass item)
        {
            this.Remove(item);
        }

        public void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;
            bulletSpawnTime += dt;

            for (int i = 0; i < this.Count; i++)
            {
                ItemClass item = this.ElementAt(i);
                
                item.Update(gameTime);

                item.timeLiving += gameTime.ElapsedGameTime.TotalMilliseconds;

                
                if (item.timeLiving >= item.ttl)
                {
                    this.RemoveAt(i);
                    i--;
                
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ItemClass item in this)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}
