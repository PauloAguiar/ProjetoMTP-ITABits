using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Projeto_Apollo_16
{
    /* A class for handling keyboard and gamepad input, providing methods for basic key interaction *
     * Author: Paulo Henrique
     * Created 21/03/2013
     */

    public class InputHandler : Microsoft.Xna.Framework.GameComponent
    {
        static KeyboardState keyboardState;
        static KeyboardState lastKeyboardState;

        /* Getters and Setters for fields */
        public static KeyboardState KeyboardState
        {
            get { return keyboardState; }
        }

        public static KeyboardState LastKeyboardStatex
        {
            get { return lastKeyboardState; }
        }

        /* Constructor, imediatelly set the state of the keyboard and gamepads */
        public InputHandler(Game game) 
            : base(game)
        {
            keyboardState = Keyboard.GetState();

        }

        /* Override methods from gameCOmponent, so they will be called each loop */
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            base.Update(gameTime);
        }

        /* General Use Methods */
        public static void Flush()
        {
            lastKeyboardState = keyboardState;
        }
        
        /* Key States Methods */
        public static bool KeyReleased(Keys key)
        {
            return keyboardState.IsKeyUp(key) && lastKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key);
        }

        public static bool KeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }
    }
}
