using System;
using System.Collections.Generic;
using System.Text;

namespace app.Menu
{
    class MainMenu
    {
        public void ShowMainMenu()
        {
            Console.Clear();
            if (Global.IsLoggedIn()) // for logged in user
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" Hello, {0, -8} ", Global.user.FirstName);
                Console.ResetColor();
            }
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("    MAIN MENU    \n");
            Console.ResetColor();
            Console.WriteLine("1 → User Menu");
            Console.WriteLine("2 → Projects Menu");
            Console.WriteLine("3 → Exit");
            Console.Write("# → ");
        }

        public void ShowInvalidAction()
        {
            Console.WriteLine("Please select a valid operation!");
            Console.ReadLine();
        }
    }
}
