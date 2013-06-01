using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public class ExplosionManager : List<ExplosionClass>
    {
        static ContentManager content;
        static SystemClass systemRef;
        double spawnTime;
        double ttl;

        public ExplosionManager(Game game)
            : base()
        {
            systemRef = (SystemClass)game;
            content = new ContentManager(systemRef.Content.ServiceProvider, systemRef.Content.RootDirectory);
            spawnTime = 10000;  //tempo maior que o ttl
            ttl = 2000;
        }

        public void createExplosion(Vector2 pos)
        {
            if (spawnTime > ttl)   //6 segundos
            {
                ExplosionClass e = new ExplosionSimple(pos);
                e.LoadFont(content);
                e.LoadTexture(content);

                this.Add(e);

                spawnTime = 0;
            }
        }

        public void Update(GameTime gameTime)
        {
            spawnTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            foreach (ExplosionClass e in this)
            {
                if (e.Lifetime > 0)
                {
                    e.Lifetime--;
                }

                
                if (e.Lifetime <= 0)
                {
                    e.isActive = false;
                }
                
            }

            //não dá pra remover os elementos dentro do foreach
            for (int i = 0; i < this.Count; i++)
            {
                if (!this.ElementAt(i).isActive)
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
            
            //só pra testar
            /*
            Explosion a = new Explosion(Vector2.Zero);
            a.LoadFont(content);
            spriteBatch.DrawString(a.SpriteFont, this.Count.ToString(), Vector2.Zero, Color.White);
             */ 
        }



    }
}
