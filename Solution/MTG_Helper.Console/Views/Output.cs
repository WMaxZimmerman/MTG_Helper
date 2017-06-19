using System;
using MTG_Helper.DAL.DomainModels;

namespace mtg.Views
{
    public static class Output
    {
        public static void DrawCard(CardDm card)
        {
            if (card == null) return;

            Console.WriteLine($"{card.Name}: {card.Cost}");
            Console.WriteLine($"Types: {string.Join(", ", card.Types)} | SubTypes: {string.Join(", ", card.SubTypes)}");
            Console.WriteLine($"Rules Text: {card.RulesText}");
            Console.WriteLine();
        }
    }
}
