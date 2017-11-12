using System;
using mtg.Models;
using MTG_Helper.BLL.BLLs;

namespace mtg.Controllers
{
    [CliController("decks", "A set of commands to interact with and create magic decks.")]
    public static class DeckController
    {
        [CliCommand("create", "Creates a new deck with the given name and commander.")]
        public static void Create(string deckName, string commanderName)
        {
            var success = DeckBLL.CreateDeck(deckName, commanderName);
            var message = success
                ? "Deck Created Successfully."
                : "An Error Occured While Attempting To Create Deck.";
            Console.WriteLine(message);
        }

        [CliCommand("build", "Populates an existing deck with cards for the given tribe.")]
        public static void Build(string deckName, string tribe)
        {
            var success = DeckBLL.BuildCommanderDeck(deckName, tribe);
            var message = success
                ? "Deck Built Successfully."
                : "An Error Occured While Attempting To Build Deck.";
            Console.WriteLine(message);
        }

        [CliCommand("output", "Outputs the cards in the given deck to the specified file.")]
        public static void Output(string deckName, string fileName)
        {
            var success = DeckBLL.OutputDeckToFile(deckName, fileName);
            var message = success
                ? $"Deck Was Successfully Output To File '{fileName}'."
                : "An Error Occured While Attempting To Build Deck.";
            Console.WriteLine(message);
        }

        [CliCommand("stats", "Outputs the stats of the given deck to the console.")]
        public static void Stats(string deckName)
        {
            var stats = DeckBLL.GetDeckStats(deckName);
            Console.WriteLine();
            Console.WriteLine($"Deck: {stats.DeckName}");
            Console.WriteLine($"Creature Count: {stats.CreatureCount}");
            Console.WriteLine($"Land Count: {stats.LandCount}");
            Console.WriteLine($"Instant Count: {stats.InstantCount}");
            Console.WriteLine($"Sorcery Count: {stats.SorceryCount}");
            Console.WriteLine($"Planswalker Count: {stats.PlaneswalkerCount}");
            Console.WriteLine($"Artifact Count: {stats.ArtifactCount}");
            Console.WriteLine($"Enchantment Count: {stats.EnchantmentCount}");
        }

        [CliCommand("add", "Adds the given card to the specified deck.")]
        public static void Add(string deckName, string cardName)
        {
            DeckBLL.AddCardToDeck(deckName, cardName);
            Console.WriteLine($"Successfully Added the card '{cardName}' the deck '{deckName}'.");
        }

        [CliCommand("remove", "Removes the given card from the specified deck.")]
        public static void Remove(string deckName, string cardName)
        {
            DeckBLL.RemoveCardFromDeck(deckName, cardName);
            Console.WriteLine($"Successfully Removed the card '{cardName}' the deck '{deckName}'.");
        }

        [CliCommand("rename", "Renames the given deck to the specified name.")]
        public static void Rename(string deckName, string newName)
        {
            DeckBLL.RenameDeck(deckName, newName);
            Console.WriteLine($"Successfully Renamed the deck '{deckName}' to '{newName}'.");
        }

        [CliCommand("delete", "Deletes the specified deck from the database.")]
        public static void Delete(string deckName)
        {
            DeckBLL.DeleteDeck(deckName);
            Console.WriteLine($"Successfully Deleted the deck '{deckName}' .");
        }

        [CliCommand("findLands", "Deletes the specified deck from the database.")]
        public static void FindLands(string deckName)
        {
            var cards = DeckBLL.GetDeckLands(deckName);

            Console.WriteLine("Outputting Cardss");

            foreach (var card in cards)
            {
                Views.Output.DrawCard(card);
            }
        }
    }
}