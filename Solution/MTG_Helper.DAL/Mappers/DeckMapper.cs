using System.Collections.Generic;
using System.Linq;
using MTG_Helper.DAL.DB;
using MTG_Helper.DAL.DomainModels;

namespace MTG_Helper.DAL.Mappers
{
    public static class DeckMapper
    {
        public static DeckDm Map(Deck em)
        {
            var dm = new DeckDm
            {
                DeckName = em.DeckName,
                Commander = CardMapper.Map(em.Card),
                Cards = CardMap(em.DeckCards).ToList()
            };

            return dm;
        }

        public static Deck Map(DeckDm dm)
        {
            var em = new Deck
            {
                DeckName = dm.DeckName,
                Commander = dm.Commander.Id,
                DeckCards = CardMap(dm.DeckName, dm.Cards).ToList()
            };

            return em;
        }

        private static IEnumerable<CardDm> CardMap(IEnumerable<DeckCard> emList)
        {
            return emList.Select(CardMap);
        }

        private static CardDm CardMap(DeckCard em)
        {
            return CardMapper.Map(em.Card);
        }

        private static IEnumerable<DeckCard> CardMap(string deckName, IEnumerable<CardDm>dmList)
        {
            return dmList.Select(d => CardMap(deckName, d));
        }

        private static DeckCard CardMap(string deckName, CardDm card)
        {
            return new DeckCard
            {
                DeckCardsId = 0,
                DeckName = deckName,
                CardId = card.Id,
                Quantity = card.Quantity
            };
        }
    }
}
