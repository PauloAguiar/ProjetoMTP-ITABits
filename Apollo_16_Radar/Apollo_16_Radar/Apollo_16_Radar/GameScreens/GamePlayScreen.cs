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
        /* Constructor */
        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        /* XNA Methods */
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            systemRef.networkManager.ReadInGamePackets();
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }
    }
}
