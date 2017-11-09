using System;
using System.Collections.Generic;
using mtg.Models;
using mtg.Views;
using MTG_Helper.BLL.BLLs;

namespace mtg.Controllers
{
    public static class DatabaseController
    {
        public static void Update(List<CommandLineArguments> args)
        {
            var updateArg = args[1];

            switch (updateArg.Command)
            {
                case "-update":
                    ApiBLL.UpdateDatabase();
                    break;
                case "-help":
                    Output.ListOptions(Options());
                    break;
                default:
                    Console.WriteLine($"Invalid parameters");
                    break;
            }
        }

        private static IEnumerable<string> Options()
        {
            return new List<string>
            {
                "-update",
                "-help"
            };
        }
    }
}
