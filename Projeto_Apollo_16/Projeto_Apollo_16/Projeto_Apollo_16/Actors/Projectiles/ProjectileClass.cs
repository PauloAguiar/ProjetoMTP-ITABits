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
        private Vector2 targetPosition;
        private bool isActive;
        protected Vector2 moveSpeed;
        protected Vector2 moveAcceleration;
        private int lifeTime;

        public bool IsActive
        {
            get { return isActive; } 
        }

        public ProjectileClass()
        {
            isActive = false;
        }

        public ProjectileClass(Vector2 initialPosition, Vector2 speed, Vector2 acceleration)
        {
            globalPosition = initialPosition;
            moveSpeed = speed;
            moveAcceleration = acceleration;
        }

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
    }
}
