using build_alpha_0._2.Core;
using System;
using System.Diagnostics;

namespace build_alpha_0._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            try
            {
                GameCore game = new GameCore();
                game.Run();
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {exc.Message}");
            }

            if (Localization.LocaleType == "eng")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[INFO] >> Press any key to close application!");
            }
            if (Localization.LocaleType == "rus")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[INFO] >> Нажмите любую клавишу для закрытия приложения!");
            }

            Console.ReadKey();
        }
    }
}
