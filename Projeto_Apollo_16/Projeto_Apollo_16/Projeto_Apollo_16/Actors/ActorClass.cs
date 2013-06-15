using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Projeto_Apollo_16
{
    public abstract class ActorClass
    {
        protected Vector2 globalPosition;   //sector definido a partir da posição
        protected Texture2D texture;
        protected SpriteFont spriteFont;
        protected List<SoundEffect> sounds;


        public SpriteFont SpriteFont
        {
            get { return spriteFont; }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }
        //é mais bizu retornar uma lista de sound effects, porque alguns atores podem emitir mais de 1 som
        public List<SoundEffect> Sound 
        {
            get { return sounds; }
        }
        //para barulhos que duram mais tempo
        public List<Song> Song;

        public Vector2 GlobalPosition
        {
            get { return globalPosition; }
        }

        public abstract void LoadTexture(ContentManager content);
        public abstract void LoadFont(ContentManager content);
        public abstract void LoadSound(ContentManager content);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
        
    }
}
