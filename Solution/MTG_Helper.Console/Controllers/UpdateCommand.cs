using System.Collections.Generic;

namespace mtg.Controllers
{
    public static class UpdateCommand
    {
        public static void Update(string[] args)
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
                case "options":
                    ListOptions();
                    break;
                default:
                    System.Console.WriteLine($"I don't Know how to update {updateArg}. Use 'options' to see available arguments.");
                    break;
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
            //SetRepository.InsertSets(MtgApi.GetAllSets());
            System.Console.WriteLine("All Sets Completed Successfully.");
        }

        private static void UdpateCardsFromApi()
        {
            System.Console.WriteLine("Starting Cards:");
            //var cards = MtgApi.GetCardsByPageRange(0, 400);

            //CardRepository.InsertCards(cards);
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
                "sets",
                "cards",
                "options"
            };
        }
    }
}
