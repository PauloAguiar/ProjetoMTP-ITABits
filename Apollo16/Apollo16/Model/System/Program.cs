using System;
namespace Projeto_Apollo_16
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            using (GameClass game = new GameClass())
            {
                game.Run();
            }
        }
    }
#endif
}

