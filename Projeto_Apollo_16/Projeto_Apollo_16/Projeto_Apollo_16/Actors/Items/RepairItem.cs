using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Projeto_Apollo_16
{
    public class RepairItem : ItemClass
    {
        public RepairItem(Vector2 position, ContentManager content) : base (position, content)
        {
            ttl = 20000;
            type = ItemType.REPAIR;
            amount = 1;
            this.LoadFont(content);
            this.LoadTexture(content);
            this.LoadSound(content);
        }

        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"Sprites\items\health");
        }

        public void LoadSound(ContentManager content)
        {
            sound = content.Load<SoundEffect>(@"Sounds\shield");
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
