using System.Collections.Generic;
using System.Linq;
using MTG_Helper.DAL.DALs;
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
                    Update(args);
                    break;
                case "list":
                    OutputAllCards();
                    break;
                case "build":
                    BuildCommanderDeck();
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

        private static void Update(string[] args)
        {
            if (args.Length > 1)
            {
                var updateArg = args[1].ToLower();

                switch (updateArg)
                {
                    case "sets":
                        UpdateSetsFromApi();
                        break;
                    case "cards":
                        UdpateCardsFromApi();
                        break;
                    default:
                        System.Console.WriteLine($"I don't Know how to update {updateArg}. Please try again.");
                        break;
                }
            }
            else
            {
                UpdateDatabaseFromApi();
            }
        }

        private static void UpdateDatabaseFromApi()
        {
            UpdateSetsFromApi();
            UdpateCardsFromApi();
        }

        private static void UpdateSetsFromApi()
        {
            System.Console.WriteLine("Starting Sets:");
            SetRepository.InsertSets(MtgApi.GetAllSets());
            System.Console.WriteLine("All Sets Completed Successfully.");
        }

        private static void UdpateCardsFromApi()
        {
            System.Console.WriteLine("Starting Cards:");
            var cards = MtgApi.GetCardsByPageRange(0, 400);

            CardRepository.InsertCards(cards);
        }

        private static void OutputAllCards()
        {
            var cards = CardRepository.GetAllCards();

            foreach (var card in cards)
            {
                System.Console.WriteLine($"[{card.Cost}] {card.Name}");
            }
        }

        private static void BuildCommanderDeck()
        {
            System.Console.Clear();
            System.Console.WriteLine("Who is your commander?");
            var commanderName = System.Console.ReadLine();
            var commaderCard = CardRepository.GetCardByName(commanderName);

            if (commaderCard != null)
            {
                var cards = CardRepository.GetAllCardsLegalForGivenCommander(commaderCard)
                    .Where(c => c.SubTypes.Contains("snake") || c.RulesText.Contains("snake"));

                foreach (var card in cards)
                {
                    System.Console.WriteLine($"[{card.Cost}] {card.Name}");
                }

                System.Console.WriteLine();
                System.Console.WriteLine($"Found {cards.Count()} cards.");
            }
        }

        private static List<string> Options()
        {
            return new List<string>
            {
                "update",
                "update sets",
                "update cards",
                "build",
                "options"
            };
        }
    }
}
