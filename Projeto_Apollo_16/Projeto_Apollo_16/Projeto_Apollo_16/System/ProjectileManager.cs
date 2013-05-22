﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public class ProjectileManager : LinkedList<ProjectileClass>
    {
        static ContentManager content;
        static SystemClass systemRef;

        public ProjectileManager(Game game)
            : base()
        {
            systemRef = (SystemClass)game;
            content = new ContentManager(systemRef.Content.ServiceProvider, systemRef.Content.RootDirectory);
        }

        public void CreateBullet(Vector2 pos, Vector2 speed, Vector2 acceleration)
        {
            this.AddFirst(new LinearProjectile(Vector2.Zero, speed, acceleration));
            this.First.Value.LoadTexture(content);
            this.First.Value.LoadFont(content);
        }

        public void Update(GameTime gameTime)
        {
            foreach (ProjectileClass p in this)
            {
                if (p.IsActive)
                    p.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ProjectileClass p in this)
            {
                if (p.IsActive)
                    p.Draw(spriteBatch);
            }
        }
    }
}
