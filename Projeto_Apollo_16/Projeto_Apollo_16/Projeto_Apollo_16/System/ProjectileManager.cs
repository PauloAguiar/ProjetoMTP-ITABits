using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public class ProjectileManager : List<ProjectileClass>
    {
        static ContentManager content;
        static SystemClass systemRef;
        public const double tts = 300;  //time to spawn

        public double bulletSpawnTime { get; private set; }

        public ProjectileManager(Game game)
            : base()
        {
            systemRef = (SystemClass)game;
            content = new ContentManager(systemRef.Content.ServiceProvider, systemRef.Content.RootDirectory);
            bulletSpawnTime = tts;
        }

        public void CreateBullet(ProjectileClass p)
        {
            this.Add(p);
            bulletSpawnTime = 0;
        }


        public void destroyBullet(ProjectileClass p)
        {
            this.Remove(p);
        }

        public void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;
            bulletSpawnTime += dt;

            for (int i = 0; i < this.Count; i++)
            {
                ProjectileClass p = this.ElementAt(i);
                
                p.Update(gameTime);

                p.timeLiving += gameTime.ElapsedGameTime.TotalMilliseconds;

                if (p.timeLiving >= p.ttl)
                {
                    p.IsActive = false;
                }

                if (!p.IsActive)
                {
                    this.RemoveAt(i);
                    i--;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ProjectileClass p in this)
            {
                p.Draw(spriteBatch);
            }
        }

    }
}
