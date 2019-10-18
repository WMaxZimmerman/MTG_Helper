using System;
using BigDeckPlays.Shared.Models;

namespace BigDeckPlays.DAL.Mappers
{
    public static class CardMapper
    {
        public static char Dilimeter = '|';
        
        public static Card StringToCard(string cardString)
        {
            var values = cardString.Split(Dilimeter);
            
            return new Card
            {
                Name = values[1],
                Quantity = Convert.ToInt32(values[0])
            };
        }

        public static string CardToString(Card card)
        {
            return $"{card.Quantity}{Dilimeter}{card.Name}";
        }
    }
}
