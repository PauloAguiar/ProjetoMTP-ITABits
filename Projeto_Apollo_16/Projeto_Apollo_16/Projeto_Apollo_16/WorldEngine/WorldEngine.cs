using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Projeto_Apollo_16
{
    enum SectorPosition
    {
        TOP_LEFT,
        TOP,
        TOP_RIGHT,
        MIDDLE_LEFT,
        MIDDLE_RIGHT,
        BOTTOM_LEFT,
        BOTTOM,
        BOTTOM_RIGHT
    }

    public class WorldEngine
    {

        /* Origin: Position (0, 0) of the Sector (0, 0) is equivalent to GlobalPosition (0, 0)
        /* Fields */
        const int TILE_SIZE = 1280;
        const int SECTOR_SIZE = 20;
        private List<WorldSectorClass> sectorList;
        private WorldSectorClass actualSector;

        /* Getters and Setters */
        public static int SectorSize
        {
            get { return SECTOR_SIZE; }
        }

        public static int TileSize
        {
            get { return TILE_SIZE; }
        }

        /* Constructor */
        public WorldEngine(Game game)
        {
            actualSector = new WorldSectorClass(game, Point.Zero);
            sectorList = new List<WorldSectorClass>(24);
            for (int x = -2; x <= 2; x++)
            {
                for (int y = -2; y <= 2; y++)
                {
                    if(x != 0 || y != 0)
                        sectorList.Add(new WorldSectorClass(game, new Point(x, y)));
                }
            }
           
        }

        public void LoadContent()
        {
            actualSector.LoadContent();
            foreach (WorldSectorClass sector in sectorList)
            {
                sector.LoadContent();
            }
        }

        public void Draw(SpriteBatch spriteBatch, PlayerClass player)
        {
            actualSector.Draw(spriteBatch, player);
            foreach (WorldSectorClass sector in sectorList)
            {
                sector.Draw(spriteBatch, player);
            }
        }

        /* Methods */
        public Point GetSector(Vector2 globalPosition)
        {
            return new Point((int)globalPosition.X / (TileSize * SectorSize), (int)globalPosition.Y / (TileSize * SectorSize));
        }
        
    }
}
