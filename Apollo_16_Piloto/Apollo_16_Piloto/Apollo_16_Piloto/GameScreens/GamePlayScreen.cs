using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Apollo_16_Piloto
{
    public class GamePlayScreen : BaseGameState
    {
        Texture2D powerBox;
        Texture2D powerText;
        Texture2D redBlock;
        Texture2D yellowBlock;
        Texture2D greenBlock;

        public PilotClass pilot;

        /* Constructor */
        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            pilot = new PilotClass();
        }

        /* XNA Methods */
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            powerBox = content.Load<Texture2D>(@"UI\PowerBox");
            pilot.LoadFont(content);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {

            systemRef.spriteBatch.Begin();

            systemRef.spriteBatch.Draw(powerBox, new Vector2(100, 100), Color.White);
            systemRef.spriteBatch.DrawString(pilot.SpriteFont, "teste", Vector2.Zero, Color.White);
            systemRef.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
