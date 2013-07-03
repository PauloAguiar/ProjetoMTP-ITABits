using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Input = SlimDX.DirectInput;

namespace Apollo_16_Piloto
{
    public class GamePlayScreen : BaseGameState
    {
        public PilotClass pilot;
        JoystickInputClass joystick;
        
        //só pra debug
        Vector2 position;
        Texture2D icon;
        
        /* Constructor */
        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            pilot = new PilotClass(content);
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
            icon = content.Load<Texture2D>(@"Menus/Icons/icon_menu");
            pilot.LoadFont(content);
            pilot.LoadTextures(content);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            systemRef.networkManager.ReadPackets(this);

            position.X = joystick.joystickState.X;
            position.Y = joystick.joystickState.Y;
            pilot.Update(gameTime);
            systemRef.networkManager.SendPackets(joystick.Update());
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {

            systemRef.spriteBatch.Begin();
            //systemRef.spriteBatch.Draw(icon, position, Color.White);
            pilot.Draw(systemRef.spriteBatch);
            systemRef.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

