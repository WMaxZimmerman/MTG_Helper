using System;

namespace mtg.Controllers
{
    public static class ConsoleUtility
    {
        public static string GetConsoleInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }
}
