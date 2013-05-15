using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Projeto_Apollo_16.Actors;

namespace Projeto_Apollo_16
{
    class WorldSectorClass
    {
        protected Point sectorCoordinates;
        protected SystemClass systemRef;
        protected ContentManager content;
        protected WorldTileClass[,] sectorMap;

        public WorldSectorClass(Game game)
        {
            systemRef = (SystemClass)game;
            sectorMap = new WorldTileClass[10, 10];
            
        }

        public void Initialize()
        {
            sectorCoordinates = new Point(0, 0);
            content = new ContentManager(systemRef.Content.ServiceProvider, systemRef.Content.RootDirectory);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    sectorMap[i, j] = new WorldTileClass(1);
                    sectorMap[i, j].LoadTileOnContent(content); /* This already manages loading 2 times the same texture */
                }
            }
        }

        public Vector2 CalculateDrawingPosition(PlayerClass player, int i, int j)
        {
            Vector2 localPosition = player.GlobalPosition - (new Vector2((float)(sectorCoordinates.X * WorldEngine.SectorSize * WorldEngine.TileSize), (float)(sectorCoordinates.Y * WorldEngine.SectorSize * WorldEngine.TileSize)));
            return ((-1) * localPosition) + (new Vector2((float)i * WorldEngine.TileSize, (float)j * WorldEngine.TileSize));
        }

        public void Draw(SpriteBatch spriteBatch, PlayerClass player)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    //spriteBatch.Draw(sectorMap[i, j].GetTileTexture(), new Vector2(400,300), Color.White);
                    Texture2D texture = sectorMap[i,j].GetTileTexture();
                    spriteBatch.Draw(texture, CalculateDrawingPosition(player, i, j), texture.Bounds, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, Globals.BACKGROUND_LAYER);
                    //spriteBatch.Draw(sectorMap[i, j].GetTileTexture(), CalculateDrawingPosition(player, i, j), Color.White);
                }
            }
        }
    }
}
