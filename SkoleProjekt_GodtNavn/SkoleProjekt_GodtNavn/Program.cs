﻿using System;

namespace SkoleProjekt_GodtNavn
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Gameworld())
                game.Run();
        }
    }
#endif
}
