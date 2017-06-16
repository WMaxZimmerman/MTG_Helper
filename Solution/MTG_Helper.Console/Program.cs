using System.Collections.Generic;
using System.Linq;
using MTG_Helper.DAL.DALs;
using MTG_Helper.DAL.Repositories;

namespace MTG_Helper.Console
{
    public class Program
    {

        static void Main(string[] args)
        {
            var action = "";
            while (action != "close")
            {
                action = GetInput();

                switch (action)
                {
                    case "update sets":
                        UpdateSetsFromApi();
                        break;
                    case "update cards":
                        UdpateCardsFromApi();
                        break;
                    case "update all":
                        UpdateDatabaseFromApi();
                        break;
                    case "list":
                        OutputAllCards();
                        break;
                    case "close":
                        break;
                    case "build":
                        BuildCommanderDeck();
                        break;
                    default:
                        System.Console.WriteLine("Invalid Option press enter to try again");
                        System.Console.ReadLine();
                        break;
                }
            }
        }

        private static string GetInput()
        {
            System.Console.Clear();
            System.Console.WriteLine("What Action Would you like to perform?");

            foreach (var option in Options())
            {
                System.Console.WriteLine($"-{option}");
            }
            System.Console.WriteLine();
            System.Console.WriteLine("Please note that sets should be updated before cards and All will update both.");
            return System.Console.ReadLine();
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
            System.Console.WriteLine("Press Enter to Continue.");
            System.Console.ReadLine();
        }

        private static void UdpateCardsFromApi()
        {
            System.Console.WriteLine("Starting Cards:");
            var cards = MtgApi.GetCardsByPageRange(0, 400);

            CardRepository.InsertCards(cards);

            System.Console.WriteLine();
            System.Console.WriteLine("Press Enter to Continue.");
            System.Console.ReadLine();
        }

        private static void OutputAllCards()
        {
            var cards = CardRepository.GetAllCards();

            foreach (var card in cards)
            {
                System.Console.WriteLine($"[{card.Cost}] {card.Name}");
            }

            System.Console.ReadLine();
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

            System.Console.ReadLine();
        }

        private static List<string> Options()
        {
            return new List<string>
            {
                "update sets",
                "update cards",
                "update all",
                "build",
                "close"
            };
        }
    }

    public class DataObject
    {
        public string Name { get; set; }
    }
}
