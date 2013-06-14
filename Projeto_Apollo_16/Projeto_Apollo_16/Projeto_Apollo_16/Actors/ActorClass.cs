using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public abstract class ActorClass
    {
        protected Vector2 globalPosition;   //sector definido a partir da posição
        protected Texture2D texture;
        protected SpriteFont spriteFont;
        protected SoundEffect sound;

        public SpriteFont SpriteFont
        {
            get { return spriteFont; }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public SoundEffect Sound 
        {
            get { return sound; }
        }

        public Vector2 GlobalPosition
        {
            get { return globalPosition; }
            //set { globalPosition = value; }  //tem que tirar isso
        }

        public abstract void LoadTexture(ContentManager content);
        public abstract void LoadFont(ContentManager content);
        public abstract void LoadSound(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
        
    }
}
