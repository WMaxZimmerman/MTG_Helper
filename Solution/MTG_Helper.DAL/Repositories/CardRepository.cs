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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine($"Unable to save the cardApi '{cardApi.Name}' Excpetion:");
                    Console.WriteLine(e);
                    Console.ForegroundColor = ConsoleColor.White;
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

        public static List<CardDm> GetAllCommanderLegalCardInGivenColors(IEnumerable<string> desiredColors)
        {
            using (var db = new MtgEntities())
            {
                try
                {
                    var legalCards = db.Cards
                        .Where(c => c.Commander == "legal")
                        .ToList()
                        .Where(c => IsWithinCommanderColors(c.Colors.Replace(" ", "").Split(',').ToList(), desiredColors))
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

        private static bool IsWithinCommanderColors(IEnumerable<string> cardColors, IEnumerable<string> commanderColors)
        {
            var allColors = GetPossibleColors();
            var invalidColors = allColors.Where(c => !commanderColors.Contains(c));

            return DoesNotContainInvlaidColor(cardColors, invalidColors);
        }

        private static bool DoesNotContainInvlaidColor(IEnumerable<string> cardColors, IEnumerable<string> invalidColos)
        {
            var isInvalid = false;

            foreach (var color in invalidColos)
            {
                if (cardColors.Contains(color)) return true;
            }

            return isInvalid;
        }

        public static List<CardDm> GetLegalCardsForGivenFormat(string format)
        {
            using (var db = new MtgEntities())
            {
                try
                {
                    format = format.ToLower();
           
                    switch (format)
                    {
                        case "commander":
                            return CardMapper.Map(db.Cards.Where(c => c.Commander == "legal")).ToList();
                        case "standard":
                            return CardMapper.Map(db.Cards.Where(c => c.Standard == "legal")).ToList();
                        case "legacy":
                            return CardMapper.Map(db.Cards.Where(c => c.Legacy == "legal")).ToList();
                        case "vintage":
                            return CardMapper.Map(db.Cards.Where(c => c.Vintage == "legal")).ToList();
                        case "modern":
                            return CardMapper.Map(db.Cards.Where(c => c.Modern == "legal")).ToList();
                        default:
                            return new List<CardDm>();
                    }
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

        public static IEnumerable<CardDm> GetAllCardsByGivenSubtype(string subtype)
        {
            using (var db = new MtgEntities())
            {
                try
                {
                    var legalCards = db.Cards
                        .Where(c => c.SubTypes.Contains(subtype));

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

        public static IEnumerable<CardDm> GetCommandersByPartialName(string cardName)
        {
            using (var db = new MtgEntities())
            {
                try
                {
                    cardName = cardName.ToLower();
                    var commanders = db.Cards
                        .Where(c => c.Types.Contains("Legendary") && c.CardName.ToLower().Contains(cardName));

                    return CardMapper.Map(commanders).ToList();
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

        public static IEnumerable<CardDm> FindTribalCommandersForType(string tribalType)
        {
            using (var db = new MtgEntities())
            {
                try
                {
                    tribalType = tribalType.ToLower();
                    var commanders = db.Cards
                        .Where(c => c.Types.Contains("Legendary") && 
                        (c.SubTypes.ToLower().Contains(tribalType) || c.RulesText.ToLower().Contains(tribalType)));

                    return CardMapper.Map(commanders).ToList();
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

        public static IEnumerable<string> GetPossibleColors()
        {
            return new List<string>
            {
                "black",
                "green",
                "blue",
                "red",
                "white"
            };
        }
    }
}
