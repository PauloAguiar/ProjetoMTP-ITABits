using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using SlimDX.DirectInput;

namespace Apollo_16_Piloto
{
    public class GamePlayScreen : BaseGameState
    {
        public PilotClass pilot;

        JoystickInputClass joystick;
        /* Constructor */
        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            pilot = new PilotClass();
            joystick = new JoystickInputClass();
            joystick.CreateDevice();
        }

        /* XNA Methods */
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            pilot.LoadFont(content);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            systemRef.networkManager.ReadPackets(this);

            systemRef.networkManager.SendPackets(joystick.Update());
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {

            systemRef.spriteBatch.Begin();

            systemRef.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
