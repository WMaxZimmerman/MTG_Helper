using System;
using System.Collections.Generic;
using mtg.Models;
using mtg.Views;
using MTG_Helper.BLL.BLLs;

namespace mtg.Controllers
{
    public static class CardController
    {
        public static void Find(List<CommandLineArguments> args)
        {
            if (args.Count < 2)
            {
                var value = args[0].Value;

            }
            else
            {
                
            }

            var updateArg = args[1];

            switch (updateArg.Command)
            {
                case "-update":
                    CardBLL.UdpateCardsFromApi();
                    break;
                case "-help":
                    ListOptions();
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
                "-update"
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
