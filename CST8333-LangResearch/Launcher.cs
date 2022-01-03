//<Author = Anoopbir Singh Sidhu>
//<Sno = 040984994>

using Model;
using System;
using View;
using Control;

namespace Control
{
    /// <summary>
    /// Launches the program
    /// </summary>
    class Launcher
    {
        /// <summary>
        /// The starting point of the program
        /// </summary>
        /// <param name="args">Stores all command line arguments which are given by the user when the program is run</param>
        static void Main(string[] args)
        {
            VaccineView view = new();
            view.Menu();
        }
    }
}
