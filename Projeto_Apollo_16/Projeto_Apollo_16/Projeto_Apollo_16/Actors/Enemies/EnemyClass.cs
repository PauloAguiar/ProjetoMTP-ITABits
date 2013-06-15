using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public abstract class EnemyClass : ActorClass
    {
        public bool isAlive;

        public EnemyClass(Vector2 pos, ContentManager content)
        {
            globalPosition = pos;
            isAlive = true;
            this.LoadFont(content);
            this.LoadTexture(content);
            this.LoadSound(content);
        }

    }
}
