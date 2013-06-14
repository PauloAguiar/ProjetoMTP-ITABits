using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public abstract class ProjectileClass : ActorClass
    {
        private bool isActive;
        //protected Vector2 velocity;
        //protected Vector2 acceleration;
        public double timeLiving;
        public double ttl {get; protected set;}

        //coloquei no playerclass:  public double spawnTime { get; protected set; }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public ProjectileClass(Vector2 initialPosition, ContentManager content)
        {
            globalPosition = initialPosition;
            //this.velocity = velocity;
            //this.acceleration = acceleration;
            timeLiving = 0;
            isActive = true;
            this.LoadFont(content);
            this.LoadTexture(content);
        }

        //public abstract void Update(GameTime gameTime);

        public void Activate()
        {
            isActive = true;
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"bullet");
            
        }

        public override void LoadFont(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>(@"Fonts\ActorInfo");
        }

        //public override void LoadSound(ContentManager content)
        //{
        //   soundEffect = content.Load<SoundEffect>(@"");
        //}
    }
}
