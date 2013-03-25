using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    public class GamePlayScreen : BaseGameState
    {
        WorldEngine world = new WorldEngine();

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
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


    }
}
