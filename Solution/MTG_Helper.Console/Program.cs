using System.Collections.Generic;
using mtg.Controllers;
using mtg.Models;
using MTG_Helper.BLL.BLLs;

namespace MTG_Helper.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("Please enter an argument.  Use 'options' to see available arguments.");
                return;
            }
            var arguments = SetArguments(args);

            switch (arguments[0].Command)
            {
                case "-update":
                   // UpdateCommand.Update(args);
                    break;
                case "-card":
                    CardController.Find(arguments);
                    break;
                case "-options":
                    ListOptions();
                    break;
                case "-deck":
                    DeckController.PerformDeckCommand(arguments);
                    break;
                default:
                    System.Console.WriteLine("Invalid Option press enter to try again");
                    break;
            }
        }

        private static List<CommandLineArguments> SetArguments(string[] args)
        {
            var arguments = new List<CommandLineArguments>();
            CommandLineArguments tempArg = null;

            for (var i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("-"))
                {
                    if (tempArg != null) arguments.Add(tempArg);
                    tempArg = new CommandLineArguments
                    {
                        Command = args[i],
                        Order = arguments.Count + 1
                    };
                }
                else
                {
                    tempArg.Value = args[i];
                }
            }
            arguments.Add(tempArg);

            return arguments;
        }

        private static void ListOptions()
        {
            foreach (var option in Options())
            {
                System.Console.WriteLine($"-{option}");
            }
        }
        
        private static IEnumerable<string> Options()
        {
            return new List<string>
            {
                "update",
                "update sets",
                "update cards",
                "build",
                "find",
                "options"
            };
        }
    }
}
