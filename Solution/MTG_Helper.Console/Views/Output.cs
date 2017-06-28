using System;
using System.Collections.Generic;
using MTG_Helper.BLL.ViewModels;

namespace mtg.Views
{
    public static class Output
    {
        public static void DrawCard(CardVm card)
        {
            if (card == null) return;

            Console.WriteLine($"{card.Name}: {card.Cost}");
            Console.WriteLine($"Types: {string.Join(", ", card.Types)} | SubTypes: {string.Join(", ", card.SubTypes)}");
            Console.WriteLine($"Rules Text: {card.RulesText}");
            Console.WriteLine();
        }

        public static void InvalidCommand()
        {
            Console.WriteLine("Invalid command. For a list of possible commands use '-help'.");
        }
 
        public static void ListOptions(IEnumerable<string> options)
        {
            foreach (var option in options)
            {
                Console.WriteLine(option);
            }
        }
    }
}
