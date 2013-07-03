using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Apollo_16_Shooter
{
    
    public class GamePlayScreen : BaseGameState
    {
        public ShooterClass shooter;
        JoystickInputClass joystick;
        protected Texture2D background;

        //só pra debug
        Vector2 position;
        Texture2D icon;

        /* Constructor */
        public GamePlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            shooter = new ShooterClass(content);
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
            icon = content.Load<Texture2D>(@"icon_menu");
            shooter.LoadFont(content);
            shooter.LoadTextures(content);
            background = content.Load<Texture2D>(@"Backgrounds\shooterBackground");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            systemRef.networkManager.ReadPackets(this);

            position.X = joystick.joystickState.X;
            position.Y = joystick.joystickState.Y;
            //if (joystick.joystickState.GetButtons(0) == true)
            //{

            //}

            shooter.Update(gameTime);

            systemRef.networkManager.SendPackets(joystick.Update());
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin();
            systemRef.spriteBatch.Draw(background, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
            systemRef.spriteBatch.Draw(icon, position, Color.White);
            shooter.Draw(systemRef.spriteBatch);

            systemRef.spriteBatch.End();

            base.Draw(gameTime);
            
        }
    }
}

