using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public abstract class ExplosionClass : ActorClass
    {
        public int Lifetime;
        public bool isActive;
        protected SoundEffect sound;

        public SoundEffect Sound 
        {
            get { return sound; }
        }

        public abstract void LoadSound(ContentManager content);

        public ExplosionClass(Vector2 position,ContentManager content)
        {
            isActive = true;
            globalPosition = position;
            this.LoadFont(content);
            this.LoadTexture(content);
            this.LoadSound(content);

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

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, globalPosition, Color.White);   
        }

    }
}
