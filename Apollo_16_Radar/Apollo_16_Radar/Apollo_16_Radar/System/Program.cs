using System;
using System.Threading;

namespace Apollo_16_Radar
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SystemClass game = new SystemClass())
            {
                game.Run();
            }

        }
    }
#endif
}

