using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    class MapSectorClass
    {
        protected Point sector;
        protected Texture2D mapTexture;
        protected Vector2 sectorPosition;
        protected Boolean loaded;
        protected ContentManager content;

        public MapSectorClass()
        {
            loaded = false;
            sector = new Point(1, 1);
        }

        public Boolean IsSectorLoaded()
        {
            return loaded;
        }

        public void LoadContent(ContentManager originalContentManager)
        {
            content = new ContentManager(originalContentManager.ServiceProvider, originalContentManager.RootDirectory);
            mapTexture = content.Load<Texture2D>("Maps\\" + sector.X + "-" + sector.Y);
            loaded = true;
        }

        public void Draw(SpriteBatch spriteBatch, PlayerClass player)
        {
            spriteBatch.Draw(mapTexture, player.Position, Color.White);
        }
    }
}
