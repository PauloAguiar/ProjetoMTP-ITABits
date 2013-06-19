using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Projeto_Apollo_16
{
    class WorldSectorClass
    {
        protected Point sectorCoordinates;
        protected SystemClass systemRef;
        protected ContentManager content;
        protected WorldTileClass[,] sectorMap;
        
        public WorldSectorClass(Game game, Point sectorCoordinates)
        {
            systemRef = (SystemClass)game;
            sectorMap = new WorldTileClass[WorldEngine.SectorSize, WorldEngine.SectorSize];
            this.sectorCoordinates = sectorCoordinates;
            content = new ContentManager(systemRef.Content.ServiceProvider, systemRef.Content.RootDirectory);
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    sectorMap[i, j] = new WorldTileClass(1);
                }
            }
            
        }

        public void LoadContent()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    sectorMap[i, j].LoadTileOnContent(content); /* This already manages loading 2 times the same texture */
                }
            }
        }
        public Vector2 CalculateDrawingPosition(int i, int j)
        {
            Vector2 localPosition = new Vector2((float)(sectorCoordinates.X * WorldEngine.SectorSize * WorldEngine.TileSize), (float)(sectorCoordinates.Y * WorldEngine.SectorSize * WorldEngine.TileSize));
            //Vector2 localPosition = player.GlobalPosition - (new Vector2((float)(sectorCoordinates.X * WorldEngine.SectorSize * WorldEngine.TileSize), (float)(sectorCoordinates.Y * WorldEngine.SectorSize * WorldEngine.TileSize)));
            return localPosition + (new Vector2((float)i * WorldEngine.TileSize, (float)j * WorldEngine.TileSize));
        }

        public void Draw(SpriteBatch spriteBatch, PlayerClass player)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    //spriteBatch.Draw(sectorMap[i, j].GetTileTexture(), new Vector2(400,side), Color.White);
                    Texture2D texture = sectorMap[i,j].GetTileTexture();
                    spriteBatch.Draw(texture, CalculateDrawingPosition(i, j), texture.Bounds, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, Globals.BACKGROUND_LAYER);
                    //spriteBatch.Draw(texture, Vector2.Zero, texture.Bounds, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, Globals.BACKGROUND_LAYER);
                    //spriteBatch.Draw(sectorMap[i, j].GetTileTexture(), CalculateDrawingPosition(player, i, j), Color.White);
                }
            }
        }
    }
}

