using System;
using System.Windows.Forms;
using System.Threading;

namespace Apollo_16_Piloto
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

