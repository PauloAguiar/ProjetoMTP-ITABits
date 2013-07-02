using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Projeto_Apollo_16
{
    public abstract class ProjectileClass : ActorClass
    {
        private bool isActive;
        public double timeLiving;
        public double ttl {get; protected set;}
        protected SoundEffect sound;
        //public int ammo;

        //getters & setters
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public SoundEffect Sound
        {
            get { return sound; }
        }
        
     
        public ProjectileClass(Vector2 initialPosition, ContentManager content)
        {
            globalPosition = initialPosition;
            timeLiving = 0;
            isActive = true;
            this.LoadFont(content);
            this.LoadTexture(content);
            this.LoadSound(content);
        }

        public void Activate()
        {
            isActive = true;
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\Shoots\bullet");
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        public abstract void LoadSound(ContentManager content);        
    }
}
