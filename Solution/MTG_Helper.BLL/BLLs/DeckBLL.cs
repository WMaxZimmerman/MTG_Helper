using System.IO;
using System.Linq;
using MTG_Helper.DAL.DomainModels;
using MTG_Helper.DAL.Repositories;

namespace MTG_Helper.BLL.BLLs
{
    public static class DeckBLL
    {
        public static DeckDm BuildCommanderDeck(string deckName, string commanderName, string tribeType)
        {
            var deck = new DeckDm
            {
                DeckName = deckName,
                Commander = CardRepository.GetCardByName(commanderName)
            };
            if (deck.Commander == null) return null;

            var legalCards = CardRepository.GetAllCardsLegalForGivenCommander(deck.Commander);
            deck.Cards = legalCards.Where(c => c.SubTypes.Contains(tribeType) || c.RulesText.Contains(tribeType)).ToList();

            return deck;
        }

        public static void SaveDeck(DeckDm deck)
        {
            var path = $"c:/tools/MtgHelper/decks/{deck.DeckName}.txt";

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
        }
    }
}
