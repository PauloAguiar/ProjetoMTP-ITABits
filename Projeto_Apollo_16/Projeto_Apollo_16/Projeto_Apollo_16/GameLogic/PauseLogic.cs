using Input = Microsoft.Xna.Framework.Input;

namespace Projeto_Apollo_16
{
    public static partial class GameLogic
    {
        static bool isPaused = false;
        static bool pauseKeyDown = false;

        private static void BeginPause()
        {
            isPaused = true;
            //pausedForGuide = !UserInitiated;
            //TODO: Pause audio playback
            //TODO: Pause controller vibration
        }

        private static void EndPause()
        {
            //TODO: Resume audio
            //TODO: Resume controller vibration
            //pausedForGuide = false;
            isPaused = false;
        }

        private static void checkPauseKey(Input.KeyboardState keyboardState)
        {
            bool pauseKeyDownThisFrame = keyboardState.IsKeyDown(Input.Keys.P);
            // If key was not down before, but is down now, we toggle the
            // pause setting
            if (!pauseKeyDown && pauseKeyDownThisFrame)
            {
                if (!isPaused)
                    BeginPause();
                else
                    EndPause();
            }
            pauseKeyDown = pauseKeyDownThisFrame;
        }


    }
}
