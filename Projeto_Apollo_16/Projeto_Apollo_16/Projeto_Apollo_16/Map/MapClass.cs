using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projeto_Apollo_16
{
    class MapClass
    {

        const int SECTOR_SIZE = 2048;
        const int CACHE_SECTORS = 3;
        private MapSectorClass actualMapSector;
        private List<MapSectorClass> cacheMapSectors;

        public MapClass()
        {
            cacheMapSectors = new List<MapSectorClass>(CACHE_SECTORS);
            actualMapSector = new MapSectorClass();
        }

        public void UpdateCache(Vector2 globalPosition)
        {
            
        }

        public void LoadMapContent(ContentManager originalContentManager)
        {
            actualMapSector.LoadContent(originalContentManager);
        }

        public void Draw(SpriteBatch spriteBatch, PlayerClass player)
        {
            actualMapSector.Draw(spriteBatch, player);
        }
    }
}
