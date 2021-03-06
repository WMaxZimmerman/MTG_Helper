﻿using System;
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
        public static CardDm GetCardByName(string cardName)
        {
            using (var db = new MtgEntities())
            {
                try
                {
                    return CardMapper.Map(db.Cards.Single(c => c.CardName == cardName));
                }
                catch (Exception)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Unable to find the card {cardName}.");
                    return null;
                }
            }
        }

        public static IEnumerable<CardDm> GetAllCards()
        {
            using (var db = new MtgEntities())
            {
                foreach (var dbCard in db.Cards)
                {
                    yield return CardMapper.Map(dbCard);
                }
            }
        }

        public static IEnumerable<CardDm> GetAllCommanderLegalCardInGivenColors(List<string> desiredColors)
        {
            Console.WriteLine("Attempting to pull from DBs");
            using (var db = new MtgEntities())
            {
                var invalidColors = GetInvalidColors(desiredColors);
                var legalCards = db.Cards
                    .Where(c => c.Commander == "legal")
                    .Where(c => invalidColors.All(i => !c.Cost.Contains(i) && !c.RulesText.Contains(i)));

                foreach (var card in legalCards)
                {
                    yield return CardMapper.Map(card);
                }
            }
            Console.WriteLine("finished db");
        }

        public static IEnumerable<CardDm> GetLegalCardsForGivenFormat(string format)
        {
            using (var db = new MtgEntities())
            {
                format = format.ToLower();
                var cards = Enumerable.Empty<Card>();

                switch (format)
                {
                    case "commander":
                        cards = db.Cards.Where(c => c.Commander == "legal");
                        break;
                    case "standard":
                        cards = db.Cards.Where(c => c.Standard == "legal");
                        break;
                    case "legacy":
                        cards = db.Cards.Where(c => c.Legacy == "legal");
                        break;
                    case "vintage":
                        cards = db.Cards.Where(c => c.Vintage == "legal");
                        break;
                    case "modern":
                        cards = db.Cards.Where(c => c.Modern == "legal");
                        break;
                }

                foreach (var card in cards)
                {
                    yield return CardMapper.Map(card);
                }
            }
        }

        public static IEnumerable<CardDm> GetAllCardsByGivenSubtype(string subtype)
        {
            using (var db = new MtgEntities())
            {
                var legalCards = db.Cards
                    .Where(c => c.SubTypes.Contains(subtype));

                foreach (var card in legalCards)
                {
                    yield return CardMapper.Map(card);
                }
            }
        }

        public static IEnumerable<CardDm> GetCommandersByPartialName(string cardName)
        {
            using (var db = new MtgEntities())
            {
                cardName = cardName.ToLower();
                var commanders = db.Cards
                    .Where(c => c.Types.Contains("Legendary") && c.CardName.ToLower().Contains(cardName));

                foreach (var card in commanders)
                {
                    yield return CardMapper.Map(card);
                }
            }
        }

        public static IEnumerable<CardDm> FindTribalCommandersForType(string tribalType)
        {
            using (var db = new MtgEntities())
            {
                tribalType = tribalType.ToLower();
                var commanders = db.Cards
                    .Where(c => c.Types.Contains("Legendary") && 
                    (c.SubTypes.ToLower().Contains(tribalType) || c.RulesText.ToLower().Contains(tribalType)));

                foreach (var card in commanders)
                {
                    yield return CardMapper.Map(card);
                }
            }
        }

        public static IEnumerable<CardDm> QueryCards(string tribal, string name, bool? commader, List<string> colors, string type, string tag, int numberToReturn = -1)
        {
            using (var db = new MtgEntities())
            {
                var invalidColors = GetInvalidColors(colors);
                var white = "";
                var blue = "";
                var black = "";
                var red = "";
                var green = "";

                foreach (var color in invalidColors)
                {
                    switch (color)
                    {
                        case "{W}":
                            white = color;
                            break;
                        case "{U}":
                            blue = color;
                            break;
                        case "{B}":
                            black = color;
                            break;
                        case "{R}":
                            red = color;
                            break;
                        case "{G}":
                            green = color;
                            break;
                    }
                }
                var takeNumber = numberToReturn > 0 ? numberToReturn : int.MaxValue;

                var cards =  db.Cards.Where(c => 
                    (tag == null || c.CardTags.Any(ct => ct.Tag.TagName == tag)) &&
                    (tribal == null || c.SubTypes.ToLower().Contains(tribal) || c.RulesText.ToLower().Contains(tribal)) &&
                    (commader == null || c.Types.Contains("Legendary")) &&
                    (name == null || c.CardName.ToLower() == name.ToLower()) &&
                    (white == "" || (!c.Cost.Contains(white) && !c.RulesText.Contains(white))) &&
                    (blue == "" || (!c.Cost.Contains(blue) && !c.RulesText.Contains(blue))) &&
                    (black == "" || (!c.Cost.Contains(black) && !c.RulesText.Contains(black))) &&
                    (red == "" || (!c.Cost.Contains(red) && !c.RulesText.Contains(red))) &&
                    (green == "" || (!c.Cost.Contains(green) && !c.RulesText.Contains(green))) &&
                    //(colors == null || invalidColors.All(i => !c.Cost.Contains(i) && !c.RulesText.Contains(i))) && //Super Wish this wouldn't blow up.
                    (type == null || c.Types.Contains(type))
                ).Take(takeNumber);
                
                foreach (var card in cards)
                {
                    yield return CardMapper.Map(card);
                }
            }
        }
        
        public static List<string> GetPossibleColors()
        {
            return new List<string>
            {
                "{W}","{U}","{B}","{R}","{G}"
            };
        }

        public static List<string> GetInvalidColors(List<string> colors)
        {
            return colors == null ? new List<string>() : GetPossibleColors().Where(c => colors.All(a => a != c)).ToList();
        }

        public static void InsertCard(CardApiDm cardApi, string setId)
        {
            using (var db = new MtgEntities())
            {
                try
                {
                    cardApi.Editions = cardApi.Editions.Where(e => e.Set_Id == setId).ToList();
                    var em = CardMapper.Map(cardApi);

                    if (db.Cards.Any(c => c.CardId == em.CardId))
                    {
                        foreach (var cardSet in em.CardSets.Where(s => !db.CardSets.Any(dbSet => dbSet.CardId == s.CardId && dbSet.SetId == s.SetId)))
                        {
                            db.CardSets.Add(cardSet);
                        }
                    }
                    else
                    {
                        db.Cards.Add(em);
                    }

                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Unable to save the card '{cardApi.Name}' Excpetion:");
                    Console.WriteLine(e);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public static void InsertCards(List<CardApiDm> cards, string setId)
        {
            var count = 1;
            foreach (var card in cards)
            {
                Console.WriteLine($"Writing Card: {count}/{cards.Count()}");
                InsertCard(card, setId);
                count++;
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
            return invalidColos.Any(cardColors.Contains);
        }

    }
}
