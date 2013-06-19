using System;
namespace Projeto_Apollo_16
{
#if WINDOWS || XBOX
    static class Program
    {
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

