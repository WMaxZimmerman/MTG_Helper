using System;
using System.Collections.Generic;
using System.Linq;
using MTG_Helper.DAL.DB;
using MTG_Helper.DAL.DomainModels;
using MTG_Helper.DAL.DomainModels.ApiModels;
using MTG_Helper.DAL.Mappers;

namespace MTG_Helper.DAL.Repositories
{
    public static class CardRepository
    {
        public static void InsertCard(CardApiDm cardApi)
        {
            using (var db = new MtgEntities())
            {
                try
                {
                    var em = CardMapper.Map(cardApi);
                    db.Cards.Add(em);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Unable to save the cardApi '{cardApi.Name}' Excpetion:");
                    Console.WriteLine(e);
                }
            }
        }
        
        public static void InsertCards(List<CardApiDm> cards)
        {
            var count = 1;
            foreach (var card in cards)
            {
                Console.WriteLine($"Writing Card: {count}/{cards.Count()}");
                InsertCard(card);
                count++;
            }
        }

        public static CardDm GetCardByName(string cardName)
        {
            using (var db = new MtgEntities())
            {
                try
                {
                    return CardMapper.Map(db.Cards.Single(c => c.CardName == cardName));
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Unable to find the card {cardName}.");
                    Console.WriteLine(e);
                    return null;
                }
            }
        }

        public static List<CardDm> GetAllCards()
        {
            using (var db = new MtgEntities())
            {
                try
                {
                    return CardMapper.Map(db.Cards).ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Unable to retreive all cards.");
                    Console.WriteLine(e);
                    return new List<CardDm>();
                }
            }
        }

        public static List<CardDm> GetAllCardsLegalForGivenCommander(CardDm commander)
        {
            using (var db = new MtgEntities())
            {
                try
                {
                    var legalCards = db.Cards
                        .Where(c => c.Commander == "legal")
                        .ToList()
                        .Where(c => IsWithinCommanderColors(c.Colors.Replace(" ", "").Split(',').ToList(), commander.Colors))
                        .ToList();

                    return CardMapper.Map(legalCards).ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Unable to retreive all cards.");
                    Console.WriteLine(e);
                    return new List<CardDm>();
                }
            }
        }

        private static bool IsWithinCommanderColors(IEnumerable<string> cardColors, ICollection<string> commanderColors)
        {
            foreach (var cardColor in cardColors)
            {
                if (!commanderColors.Contains(cardColor)) return false;
            }

            return true;
        }

        public static List<CardDm> GetLegalCardsForGivenFormat(string format)
        {
            return new List<CardDm>();
        }
    }
}
