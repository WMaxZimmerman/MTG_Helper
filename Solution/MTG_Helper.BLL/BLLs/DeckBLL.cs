using System.IO;
using System.Linq;
using MTG_Helper.BLL.Mappers;
using MTG_Helper.BLL.ViewModels;
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
                Id = 0,
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

            var legalCards = CardRepository.GetAllCommanderLegalCardInGivenColors(CardBLL.GetCardColors(CardMapper.Map(deck.Commander)));
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

        public static DeckStatsVm GetDeckStats(string deckName)
        {
            var deck = DeckRepository.GetDeck(deckName);

            var deckStats = new DeckStatsVm
            {
                DeckName = deck.DeckName,
                CreatureCount = deck.Cards.Count(c => c.Types.Contains("creature")) + 1,
                LandCount = deck.Cards.Count(c => c.Types.Contains("land")),
                ArtifactCount = deck.Cards.Count(c => c.Types.Contains("artifact")),
                SorceryCount = deck.Cards.Count(c => c.Types.Contains("sorcery")),
                InstantCount = deck.Cards.Count(c => c.Types.Contains("instant")),
                PlaneswalkerCount = deck.Cards.Count(c => c.Types.Contains("planeswalker")),
                EnchantmentCount = deck.Cards.Count(c => c.Types.Contains("enchantment"))
            };

            return deckStats;
        }

        public static void AddCardToDeck(string deckName, string cardName)
        {
            DeckRepository.AddCardToDeck(deckName, cardName);
        }

        public static void RemoveCardFromDeck(string deckName, string cardName)
        {
            DeckRepository.RemoveCardFromDeck(deckName, cardName);
        }

        public static void RenameDeck(string deckName, string newName)
        {
            var deck = DeckRepository.GetDeck(deckName);
            deck.DeckName = newName;
            DeckRepository.UpdateDeckName(deck);
        }

        public static void DeleteDeck(string deckName)
        {
            DeckRepository.Delete(deckName);
        }
    }
}
