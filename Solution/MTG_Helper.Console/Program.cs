using System.Collections.Generic;
using mtg.Controllers;
using mtg.Models;
using mtg.Views;

namespace MTG_Helper.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("Please enter an argument.  Use 'help' to see available arguments.");
                return;
            }
            var arguments = SetArguments(args);

            switch (arguments[0].Command)
            {
                case "-sets":
                   // UpdateCommand.Update(args);
                    break;
                case "-cards":
                    CardController.Find(arguments);
                    break;
                case "-help":
                    Output.ListOptions(Options());
                    break;
                case "-decks":
                    DeckController.PerformDeckCommand(arguments);
                    break;
                default:
                    System.Console.WriteLine("Invalid command. For a list of possible commands use '-help'.");
                    break;
            }
        }

        private static List<CommandLineArguments> SetArguments(string[] args)
        {
            var arguments = new List<CommandLineArguments>();
            CommandLineArguments tempArg = null;

            foreach (var argument in args)
            {
                if (argument.StartsWith("-"))
                {
                    if (tempArg != null) arguments.Add(tempArg);
                    tempArg = new CommandLineArguments
                    {
                        Command = argument,
                        Order = arguments.Count + 1
                    };
                }
                else
                {
                    tempArg.Value = argument;
                }
            }
            arguments.Add(tempArg);

            return arguments;
        }
        
        private static IEnumerable<string> Options()
        {
            return new List<string>
            {
                "-decks",
                "-cads",
                "-sets",
                "-help"
            };
        }
    }
}
