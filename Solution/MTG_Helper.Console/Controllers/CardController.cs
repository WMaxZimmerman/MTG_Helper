using System;
using System.Collections.Generic;
using mtg.Views;
using MTG_Helper.BLL.BLLs;
using MTG_Helper.BLL.Mappers;

namespace mtg.Controllers
{
    public static class CardController
    {
        public static void Find(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("You need to tell me what to find.");
                return;
            }

            var updateArg = args[1];

            switch (updateArg)
            {
                case "all":
                    OutputAllCards();
                    break;
                case "card":
                    FindCard(args);
                    break;
                case "cards":
                    FindCards(args);
                    break;
                case "options":
                    ListOptions();
                    break;
                case "commander":
                    FindCommander(args);
                    break;
                default:
                    Console.WriteLine($"I don't Know how to find {updateArg}. Use 'options' to see available arguments.");
                    break;
            }
        }

        private static void ListOptions()
        {
            foreach (var option in Options())
            {
                Console.WriteLine($"-{option}");
            }
        }

        private static IEnumerable<string> Options()
        {
            return new List<string>
            {
                "all",
                "card",
                "cards"
            };
        }

        private static void FindCard(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("You need to tell me what to card to find.");
                return;
            }

            var cardName = args[2];
            var card = CardBLL.GetCardByName(cardName);

            Output.DrawCard(card);
        }

        private static void FindCards(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("You need to tell me what to cards to find.");
                return;
            }

            var cardName = args[2];
            var cards = CardBLL.GetCommandersByPartialName(cardName);

            foreach (var card in cards)
            {
                Output.DrawCard(card);
            }
        }

        private static void OutputAllCards()
        {
            var cards = CardBLL.GetAllCards();

            foreach (var card in cards)
            {
                Console.WriteLine($"[{card.Cost}] {card.Name}");
            }
        }

        private static void FindCommander(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("I Need A bit more information than that.");
                return;
            }

            var commanderType = args[2];

            if (commanderType == "tribal")
            {
                if (args.Length < 4)
                {
                    Console.WriteLine("I Need A bit more information than that.");
                    return;
                }

                var tribalType = args[3];

                var cards = CardBLL.FindTribalCommandersForType(tribalType);

                foreach (var card in cards)
                {
                    Output.DrawCard(card);
                }
            }
        }
    }
}
