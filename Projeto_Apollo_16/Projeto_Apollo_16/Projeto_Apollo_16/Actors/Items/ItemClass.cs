using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Projeto_Apollo_16
{
    public enum ItemType
    {
        PRIMARY_WEAPON,
        SECONDARY_WEAPON,
        SHIELD,
        HULL,
        ENGINE,
        REPAIR,
        FUEL
    }

    public abstract class ItemClass : ActorClass
    {
        public double timeLiving;
        public double ttl { get; protected set; }

        public ItemType type;

        public Int32 amount;
        protected SoundEffect sound;
        
        public SoundEffect Sound
        {
            get { return sound; }
        }

        public ItemClass(Vector2 position, ContentManager content)
        {
            globalPosition = position;
            timeLiving = 0;
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, globalPosition, Color.White);
        }
          

    }
}
