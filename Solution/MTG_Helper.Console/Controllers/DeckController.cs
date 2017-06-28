using System;
using System.Collections.Generic;
using mtg.Models;
using mtg.Views;
using MTG_Helper.BLL.BLLs;

namespace mtg.Controllers
{
    public static class DeckController
    {
        public static void PerformDeckCommand(List<CommandLineArguments> args)
        {
            string message;
            bool success;

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
                case "-stats":
                    var stats = DeckBLL.GetDeckStats(args[0].Value);
                    Console.WriteLine();
                    Console.WriteLine($"Deck: {stats.DeckName}");
                    Console.WriteLine($"Creature Count: {stats.CreatureCount}");
                    Console.WriteLine($"Land Count: {stats.LandCount}");
                    Console.WriteLine($"Instant Count: {stats.InstantCount}");
                    Console.WriteLine($"Sorcery Count: {stats.SorceryCount}");
                    Console.WriteLine($"Planswalker Count: {stats.PlaneswalkerCount}");
                    Console.WriteLine($"Artifact Count: {stats.ArtifactCount}");
                    Console.WriteLine($"Enchantment Count: {stats.EnchantmentCount}");
                    break;
                case "-add":
                    DeckBLL.AddCardToDeck(args[0].Value, args[1].Value);
                    Console.WriteLine($"Successfully Added the card '{args[1].Value}' the deck '{args[0].Value}'.");
                    break;
                case "-remove":
                    DeckBLL.RemoveCardFromDeck(args[0].Value, args[1].Value);
                    Console.WriteLine($"Successfully Removed the card '{args[1].Value}' the deck '{args[0].Value}'.");
                    break;
                case "-rename":
                    DeckBLL.RenameDeck(args[0].Value, args[1].Value);
                    Console.WriteLine($"Successfully Renamed the deck '{args[0].Value}' to '{args[1].Value}'.");
                    break;
                case "-delete":
                    DeckBLL.DeleteDeck(args[0].Value);
                    Console.WriteLine($"Successfully Deleted the deck '{args[0].Value}' .");
                    break;
                case "-help":
                    Output.ListOptions(Options());
                    break;
                default:
                    Console.WriteLine("Invalid command. For a list of possible commands use '-help'.");
                    break;
            }
        }

        private static IEnumerable<string> Options()
        {
            return new List<string>
            {
                "-create",
                "-build",
                "-output",
                "-stats",
                "-add",
                "-remove",
                "-rename",
                "-delete",
                "-help"
            };
        }
    }
}
