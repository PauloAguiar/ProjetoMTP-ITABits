using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public abstract class ItemClass : ActorClass
    {
        protected string name;
        public double timeLiving;
        public double ttl { get; protected set; }
        protected bool taken;
        protected PlayerClass player;
        public bool IsUsing { get; protected set; }
        protected SoundEffect sound;
        
        public SoundEffect Sound
        {
            get { return sound; }
        }

        public ItemClass(PlayerClass player, Vector2 position, ContentManager content)
        {
            globalPosition = position;
            timeLiving = 0;
            taken = false;
            IsUsing = true;
            this.player = player;
            this.LoadFont(content);
            this.LoadTexture(content);
            this.LoadSound(content);
        }

        public abstract void LoadSound(ContentManager content);

        public override void Update(GameTime gameTime)
        {
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
