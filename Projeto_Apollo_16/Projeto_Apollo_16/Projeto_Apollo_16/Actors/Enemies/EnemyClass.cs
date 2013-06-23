using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public abstract class EnemyClass : ActorClass
    {
        public bool isAlive;
        public enum Enemies
        {
            Ghost = 0,
            Polygon,
            Sun,
            Chaser,
        }
        public const int numberEnemies = 4;

        public EnemyClass(Vector2 pos, ContentManager content)
        {
            globalPosition = pos;
            isAlive = true;
            this.LoadFont(content);
            this.LoadTexture(content);
            //this.LoadSound(content);

        }

        public abstract void Destroy(Vector2 position, ContentManager content, ExplosionManager explosionManager);

    }
}
