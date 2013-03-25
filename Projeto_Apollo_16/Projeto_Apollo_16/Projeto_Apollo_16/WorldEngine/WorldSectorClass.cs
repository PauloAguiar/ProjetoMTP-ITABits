using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    class WorldSectorClass
    {
        protected Point sectorCoordinates;
        protected Texture2D mapTexture;
        protected String textureID;
        protected Boolean loaded;
        protected SystemClass systemRef;
        protected ContentManager content;

        public WorldSectorClass(Game game, String texture)
        {
            textureID = texture;
            systemRef = (SystemClass)game;
            loaded = false;
        }

        public Boolean IsSectorLoaded()
        {
            return loaded;
        }

        public void LoadContent()
        {
            content = new ContentManager(systemRef.Content.ServiceProvider, systemRef.Content.RootDirectory);
            mapTexture = content.Load<Texture2D>("Maps\\" + textureID);
            loaded = true;
        }

        public void Draw(SpriteBatch spriteBatch, PlayerClass player)
        {
            spriteBatch.Draw(mapTexture, player.Position, Color.White);
        }
    }
}
