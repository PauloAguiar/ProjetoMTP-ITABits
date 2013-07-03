using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public abstract class ExplosionClass : ActorClass
    {
        public int lifeTime;
        public bool isActive;
        protected SoundEffect sound;
        protected float distance;
        protected float volume;

        public SoundEffect Sound 
        {
            get { return sound; }
        }

        public abstract void LoadSound(ContentManager content);

        public ExplosionClass(Vector2 playerPosition, Vector2 enemyPosition,ContentManager content)
        {
            distance = (playerPosition - enemyPosition).Length();
            isActive = true;
            globalPosition = enemyPosition;
            this.LoadFont(content);
            this.LoadTexture(content);
            this.LoadSound(content);

        }
        
        public override void Update(GameTime gameTime)
        {
            if (lifeTime > 0)
            {
                lifeTime--;
            }


            if (lifeTime <= 0)
            {
                isActive = false;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, globalPosition, Color.White);   
        }

    }
}
