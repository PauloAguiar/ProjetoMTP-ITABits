using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projeto_Apollo_16
{
    public class WorldEngine
    {
        /* Origin: Position (0, 0) of the Sector (0, 0) is equivalent to GlobalPosition (0, 0)
        /* Fields */
        const int TILE_SIZE = 256;
        const int SECTOR_SIZE = 10;
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
            actualSector = new WorldSectorClass(game);
        }

        public void Initialize()
        {
            actualSector.Initialize();
        }
        public void Draw(SpriteBatch spriteBatch, PlayerClass player)
        {
            actualSector.Draw(spriteBatch, player);
        }
        /* Methods */
        public Point GetSector(Vector2 globalPosition)
        {
            return new Point((int)globalPosition.X / (TileSize * SectorSize), (int)globalPosition.Y / (TileSize * SectorSize));
        }
        
    }
}
