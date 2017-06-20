using System;
using System.Collections.Generic;
using mtg.Models;
using MTG_Helper.BLL.BLLs;

namespace mtg.Controllers
{
    public static class DeckController
    {
        public static void PerformDeckCommand(List<CommandLineArguments> args)
        {
            var message = "";
            var success = true;

            switch (args[1].Command)
            {
                case "-create":
                    success = DeckBLL.CreateDeck(args[1].Value, args[2].Value);
                    message = success
                        ? "Deck Created Successfully."
                        : "An Error Occured While Attempting To Create Deck.";
                    Console.WriteLine(message);
                    break;
                case "-build":
                    success = DeckBLL.BuildCommanderDeck(args[1].Value, args[2].Value);
                    message = success
                        ? "Deck Built Successfully."
                        : "An Error Occured While Attempting To Build Deck.";
                    Console.WriteLine(message);
                    break;
                case "-output":
                    success = DeckBLL.OutputDeckToFile(args[0].Value, args[1].Value);
                    message = success
                        ? $"Deck Was Successfully Output To File '{args[1].Value}'."
                        : "An Error Occured While Attempting To Build Deck.";
                    Console.WriteLine(message);
                    break;
                default:
                    Console.WriteLine("Invalid Option press enter to try again");
                    break;
            }
        }
    }
}
