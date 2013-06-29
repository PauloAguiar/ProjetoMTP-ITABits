using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using ProjectMercury;

namespace Projeto_Apollo_16
{
    public class EnemyManager : List<EnemyClass>
    {
        static ContentManager content;
        static SystemClass systemRef;

        public double spawnTime { get; private set; }
        public const double TTS = 1000;

        public EnemyManager(Game game) : base()
        {
            systemRef = (SystemClass)game;
            content = new ContentManager(systemRef.Content.ServiceProvider, systemRef.Content.RootDirectory);
            spawnTime = TTS;
        }

        public void createEnemy(EnemyClass enemy)
        {
            this.Add(enemy);
            spawnTime = 0;
        }

        public void destroyEnemy(int index, Vector2 position, ExplosionManager explosionManager)
        {
            this.ElementAt(index).Destroy(position, content, explosionManager);
            this.RemoveAt(index);
        }

        public void Update(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;
            spawnTime += dt;
            
            for (int i = 0; i < this.Count; i++)
            {
                EnemyClass enemy = this.ElementAt(i);

                enemy.Update(gameTime);
                
                if (!enemy.isAlive)
                {
                    this.RemoveAt(i);
                    i++;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (EnemyClass enemy in this)
            {
                enemy.Draw(spriteBatch);
            }
            
        }


        public RadarDataClass GetRadarData(PlayerClass player)
        {
            List<EnemyClass> temp = new List<EnemyClass>();
            foreach (EnemyClass enemy in this)
            {
                Vector2 dist = enemy.GlobalPosition - player.GlobalPosition;
                if (dist.Length() < 10000)
                {
                    temp.Add(enemy);
                }
            }
            return new RadarDataClass(temp, player.GlobalPosition);
        }

    }
}
