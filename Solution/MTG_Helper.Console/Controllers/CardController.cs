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
//                var card = CardBLL.GetCardByName(value);
//                Output.DrawCard(card);
                var cards = CardBLL.GetAllCards();
                foreach (var card in cards)
                {
                    Output.DrawCard(card);
                }
            }
            else
            {
                var updateArg = args[1];

                switch (updateArg.Command)
                {
                    case "-update":
                        CardBLL.UdpateCardsFromApi();
                        break;
                    case "-help":
                        Output.ListOptions(Options());
                        break;
                    default:
                        Console.WriteLine($"Invali");
                        break;
                }
            }
        }

        private static IEnumerable<string> Options()
        {
            return new List<string>
            {
                "-update",
                "-help"
            };
        }
    }
}
