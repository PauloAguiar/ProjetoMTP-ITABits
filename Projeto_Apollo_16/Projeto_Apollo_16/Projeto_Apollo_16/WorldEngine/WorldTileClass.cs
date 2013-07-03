using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    public class WorldTileClass
    {
        /* Fields */
        protected Int32 codeID;
        protected Texture2D texture;
        protected Boolean loaded;

        /* Getters and Setters */
        public Texture2D GetTileTexture()
        {
            return texture;
        }

        /* Constructor */
        public WorldTileClass(int code)
        {
            codeID = code;
            loaded = false;
        }

        /* Methods */
        public void LoadTileOnContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>(@"Maps\Space_Deep");
            loaded = true;
        }
    }
}
