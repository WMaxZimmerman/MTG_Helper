using System;
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
            if (card.Types.Contains("creature")) Console.WriteLine($"Power: {card.Power} | Toughness: {card.Toughness}");
            Console.WriteLine();
        }
    }
}
