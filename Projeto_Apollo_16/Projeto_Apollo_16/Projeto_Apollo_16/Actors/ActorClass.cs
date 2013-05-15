using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16.Actors
{
    public abstract class ActorClass
    {

        protected Vector2 globalPosition;   //sector definido a partir da posição
        public Texture2D texture;


        public Vector2 GlobalPosition
        {
            get { return globalPosition; }
            set { globalPosition = value; }  //tem que tirar isso
        }

        public abstract void LoadTexture(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
        
    }
}
