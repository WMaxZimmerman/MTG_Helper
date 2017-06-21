using System.Collections.Generic;
using System.IO;
using System.Linq;
using MTG_Helper.DAL.DB;
using MTG_Helper.DAL.DomainModels;
using MTG_Helper.DAL.Mappers;

namespace MTG_Helper.DAL.Repositories
{
    public static class DeckRepository
    {
        public static void InsertDeck(DeckDm deck)
        {
            var newDeck = DeckMapper.Map(deck);

            using (var db = new MtgEntities())
            {
                db.Decks.Add(newDeck);
                db.SaveChanges();
            }
        }

        public static DeckDm GetDeck(string deckName)
        {
            using (var db = new MtgEntities())
            {
                var deck = DeckMapper.Map(db.Decks.SingleOrDefault(d => d.DeckName == deckName));
                return deck;
            }
        }

        public static void UpdateDeck(DeckDm deck)
        {
            InsertCards(deck.DeckName, deck.Cards);
        }

        public static void UpdateDeckName(DeckDm deck)
        {
            using (var db = new MtgEntities())
            {
                var sql = $"Update Deck Set DeckName = '{deck.DeckName}' Where DeckId = {deck.Id}";
                db.Database.ExecuteSqlCommand(sql);
            }
        }

        public static bool SaveDeckToFile(DeckDm deck)
        {
            var path = GetDirectory(deck.DeckName);

            if (File.Exists(path)) File.Delete(path);
            var temp = File.Create(path);
            temp.Flush();
            temp.Close();

            using (var file = new StreamWriter(@path))
            {
                file.WriteLine($"1x {deck.Commander.Name} *CMDR*");

                foreach (var card in deck.Cards)
                {
                    file.WriteLine($"1x {card.Name}");
                }

                file.Close();
            }

            return true;
        }

        public static void AddCardToDeck(string deckName, string cardName)
        {
            var card = CardRepository.GetCardByName(cardName);

            InsertCard(deckName, card);
        }

        private static void InsertCard(string deckName, CardDm card)
        {
            var deck = GetDeck(deckName);

            var deckCard = new DeckCard
            {
                DeckCardsId = 0,
                DeckId = deck.Id,
                CardId = card.Id
            };

            using(var db = new MtgEntities())
            {
                db.DeckCards.Add(deckCard);
                db.SaveChanges();
            }
        }

        private static void InsertCards(string deckName, IEnumerable<CardDm> cards)
        {
            foreach (var card in cards)
            {
                InsertCard(deckName, card);
            }
        }

        private static string GetDirectory(string deckName)
        {
            var deckDirectory = "c:/tools/MtgHelper/decks";
            if (!Directory.Exists(deckDirectory)) Directory.CreateDirectory(deckDirectory);

            return $"{deckDirectory}/{deckName}.txt";
        }

        public static void RemoveCardFromDeck(string deckName, string cardName)
        {
            var card = CardRepository.GetCardByName(cardName);
            var deck = GetDeck(deckName);

            DeleteCard(deck, card);
        }

        private static void DeleteCard(DeckDm deck, CardDm card)
        {
            using (var db = new MtgEntities())
            {
                var deckCard = db.DeckCards.Single(dc => dc.DeckId == deck.Id && dc.CardId == card.Id);
                db.DeckCards.Remove(deckCard);
                db.SaveChanges();
            }
        }
    }
}
