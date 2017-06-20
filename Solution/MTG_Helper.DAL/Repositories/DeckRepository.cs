using System.Collections.Generic;
using System.IO;
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
                var deck = DeckMapper.Map(db.Decks.Find(deckName));
                return deck;
            }
        }

        public static void UpdateDeck(DeckDm deck)
        {
            InsertCards(deck.DeckName, deck.Cards);
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

        private static void InsertCard(string deckName, CardDm card)
        {
            var deckCard = new DeckCard
            {
                DeckCardsId = 0,
                DeckName = deckName,
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
    }
}
