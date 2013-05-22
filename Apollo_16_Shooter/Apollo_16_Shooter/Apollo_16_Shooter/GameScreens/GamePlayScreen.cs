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
            shooter.LoadFont(content);
            shooter.LoadTextures(content);
            background = content.Load<Texture2D>(@"Backgrounds\shooterBackground");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            shooter.Update(gameTime);
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            systemRef.spriteBatch.Begin();
            shooter.Draw(systemRef.spriteBatch);
            systemRef.spriteBatch.Draw(background, Vector2.Zero, Color.White);
            systemRef.spriteBatch.End();

            base.Draw(gameTime);
            
        }
    }
}

