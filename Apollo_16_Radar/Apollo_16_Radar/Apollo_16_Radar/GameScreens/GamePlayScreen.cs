using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace Apollo_16_Radar
{
    
    public class GamePlayScreen : BaseGameState
    {
        public RadarDataClass data;
        public RadarImmediateData immediateData;
        Texture2D redDot;
        Texture2D backgroundTexture;
        Texture2D playerIcon;

        /* Constructor */
        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            data = new RadarDataClass();
            immediateData = new RadarImmediateData();
        }

        /* XNA Methods */
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
//            backgroundTexture = content.Load<Texture2D>(@"nave_amarela_1024x768");
            backgroundTexture = content.Load<Texture2D>(@"radarBackground");
            redDot = content.Load<Texture2D>(@"red_dot_2");
            playerIcon = content.Load<Texture2D>(@"playerIcon");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            systemRef.networkManager.ReadInGamePackets();
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin();
            base.Draw(gameTime);

            systemRef.spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);

            foreach (EnemyClass enemy in data.enemies)
            {
                DrawEnemy(systemRef.spriteBatch, enemy, data.playerGlobalPosition);
            }
            DrawPlayer(systemRef.spriteBatch, new Vector2(systemRef.screenRectangle.Width / 2, systemRef.screenRectangle.Height / 2), immediateData.playerAngle);

            systemRef.spriteBatch.End();

            
        }

        private void DrawPlayer(SpriteBatch spriteBatch, Vector2 position, float angle)
        {
            spriteBatch.Draw(playerIcon, position, null, Color.White, angle, new Vector2(playerIcon.Width/2, playerIcon.Height/2), 1.0f, SpriteEffects.None, 0);
            //spriteBatch.Draw(playerIcon, position, Color.White);
        }

        private void DrawEnemy(SpriteBatch spriteBatch, EnemyClass enemy, Vector2 playerPos)
        {
            spriteBatch.Draw(redDot, CalculateRelativePosition(enemy, playerPos), Color.White);
        }

        private Vector2 CalculateRelativePosition(EnemyClass enemy, Vector2 playerPos)
        {
            int factor = 5;
            float X = systemRef.screenRectangle.Width/2;
            float Y = systemRef.screenRectangle.Height/2;
            X += (enemy.globalPosition.X - playerPos.X) / factor;
            Y += (enemy.globalPosition.Y - playerPos.Y) / factor;
            return new Vector2(X, Y);
        }
    }
}
