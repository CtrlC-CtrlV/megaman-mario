using System;

namespace MegaManClone
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            using (MegamanGame game = new MegamanGame())
            {
                game.Run();
            }
        }
    }
#endif
}

