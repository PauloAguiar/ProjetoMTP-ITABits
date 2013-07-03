using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Projeto_Apollo_16
{
    public abstract class EnemyClass : ActorClass
    {
        public bool isAlive;
        public Int32 type;
        public int life;
        public SoundEffect hitSound;

        public enum Enemies
        {
            Ghost = 0,
            Polygon,
            Sun,
            Chaser,
        }
        public const int NUMBER_TYPE_ENEMIES = 4;

        public EnemyClass(Vector2 pos, ContentManager content)
        {
            globalPosition = pos;
            isAlive = true;
            this.LoadFont(content);
            this.LoadTexture(content);
            this.LoadSound(content);
        }

        public abstract void LoadSound(ContentManager content);

        public abstract void Destroy(Vector2 playerPosition, Vector2 position, ContentManager content, ExplosionManager explosionManager);

    }
}
