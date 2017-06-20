using System.IO;
using System.Linq;
using MTG_Helper.DAL.DomainModels;
using MTG_Helper.DAL.Repositories;

namespace MTG_Helper.BLL.BLLs
{
    public static class DeckBLL
    {
        public static bool CreateDeck(string deckName, string commanderName)
        {
            var commander = CardRepository.GetCardByName(commanderName);
            if (commander == null) return false;

            var deck = new DeckDm
            {
                DeckName = deckName,
                Commander = commander
            };

            DeckRepository.InsertDeck(deck);

            return true;
        }

        public static bool BuildCommanderDeck(string deckName, string tribeType)
        {
            var deck = DeckRepository.GetDeck(deckName);
            if (deck.Commander == null) return false;

            var legalCards = CardRepository.GetAllCardsLegalForGivenCommander(deck.Commander);
            deck.Cards = legalCards.Where(c => c.SubTypes.Contains(tribeType) || c.RulesText.Contains(tribeType) && c.Id != deck.Commander.Id).ToList();

            DeckRepository.UpdateDeck(deck);

            return true;
        }
        
        public static bool OutputDeckToFile(string deckName, string path)
        {
            var deck = DeckRepository.GetDeck(deckName);

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
    }
}
