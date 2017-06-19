using System;
using System.Collections.Generic;
using System.Linq;
using mtg.Controllers;
using mtg.Views;
using MTG_Helper.BLL.BLLs;
using MTG_Helper.DAL.DALs;
using MTG_Helper.DAL.DomainModels;
using MTG_Helper.DAL.Repositories;

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

            var action = args[0].ToLower();

            switch (action)
            {
                case "update":
                    UpdateCommand.Update(args);
                    break;
                case "build":
                    BuildCommanderDeck();
                    break;
                case "find":
                    FindCommand.Find(args);
                    break;
                case "options":
                    ListOptions();
                    break;
                default:
                    System.Console.WriteLine("Invalid Option press enter to try again");
                    break;
            }
        }

        private static void ListOptions()
        {
            foreach (var option in Options())
            {
                System.Console.WriteLine($"-{option}");
            }
        }
        
        private static void BuildCommanderDeck()
        {
            System.Console.Clear();
            var deckName = ConsoleUtility.GetConsoleInput("What is the name of the new deck?");
            var commanderName = ConsoleUtility.GetConsoleInput("Who will be the commander?");
            var tribeType = ConsoleUtility.GetConsoleInput("What tribe are we building?");

            var deck = DeckBLL.BuildCommanderDeck(deckName, commanderName, tribeType);
            DeckBLL.SaveDeck(deck);
        }

        private static List<string> Options()
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
