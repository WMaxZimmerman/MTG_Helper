using System;
using System.Collections.Generic;
using System.Linq;
using MTG_Helper.DAL.DB;
using MTG_Helper.DAL.DomainModels;
using MTG_Helper.DAL.DomainModels.ApiModels;

namespace MTG_Helper.DAL.Mappers
{
    public static class CardMapper
    {
        public static Card Map(CardApiDm apiDm)
        {
            if (apiDm.Supertypes == null) apiDm.Supertypes = new List<string>();
            apiDm.Supertypes.AddRange(apiDm.Types);

            var em = new Card
            {
                CardId = apiDm.Id,
                CardName = apiDm.Name,
                ConvertedManaCost = apiDm.Cmc,
                Cost = apiDm.Cost,
                Commander = apiDm.Formats.Commander,
                Legacy = apiDm.Formats.Legacy,
                Standard = apiDm.Formats.Standard,
                Modern = apiDm.Formats.Modern,
                Vintage = apiDm.Formats.Vintage,
                Power = apiDm.Power,
                Toughness = apiDm.Toughness,
                RulesText = apiDm.Text,
                StoreUrl = apiDm.Store_Url,
                Url = apiDm.Url,
                Types = string.Join(", ", apiDm.Supertypes),
                SubTypes = string.Join(", ", apiDm.Subtypes ?? new List<string>()),
                Colors = string.Join(", ", apiDm.Colors ?? new List<string>()),
                CardSets = MapSets(apiDm).ToList()
            };

            return em;
        }

        public static List<Card> Map(List<CardApiDm> dmList)
        {
            return dmList.Select(Map).ToList();
        }

        public static CardDm Map(Card em)
        {
            var dm = new CardDm
            {
                Id = em.CardId,
                Name = em.CardName,
                Cmc = em.ConvertedManaCost,
                Cost = em.Cost,
                Legality = new CardLegalityDm
                {
                    Commander = em.Commander == "legal",
                    Legacy = em.Legacy == "legal",
                    Modern = em.Modern == "legal",
                    Standard = em.Standard == "legal",
                    Vintage = em.Vintage == "legal"
                },
                Power = em.Power,
                Toughness = em.Toughness,
                RulesText = em.RulesText,
                StoreUrl = em.StoreUrl,
                Url = em.Url,
                Types = em.Types.Replace(" ", "").Split(',').ToList(),
                SubTypes = em.SubTypes.Replace(" ", "").Split(',').ToList(),
                Colors = em.Colors.Replace(" ", "").Split(',').ToList(),
                Sets = MapSets(em.CardSets.ToList()),
                Tags = TagMapper.Map(em.CardTags.Select(ct => ct.Tag)).ToList()
            };

            return dm;
        }

        public static IEnumerable<CardDm> Map(IEnumerable<Card> emList)
        {
            return emList.Select(Map);
        }

        private static List<CardSet> MapSets(CardApiDm apiDm)
        {
            return apiDm.Editions.Select(set => new CardSet
            {
                Artist = set.Artist,
                CardId = apiDm.Id,
                CardNumber = set.Number,
                CardSetId = 0,
                FlavorText = set.Flavor,
                HighPrice = (decimal)(set.PriceApi?.High ?? 0),
                MedianPrice = (decimal)(set.PriceApi?.Medium ?? 0),
                LowPrice = (decimal)(set.PriceApi?.Low ?? 0),
                ImageUrl = set.Image_Url,
                MultiverseId = Convert.ToInt64(set.Multiverse_Id == "" ? "0" : set.Multiverse_Id),
                Rarity = set.Rarity,
                SetId = set.Set_Id,
                StoreUrl = set.Store_Url
            }).ToList();
        }

        private static CardSetDm MapSets(CardSet em)
        {
            var dm = new CardSetDm
            {
                Id = em.CardSetId,
                SetId = em.SetId,
                CardId = em.CardId,
                Artist = em.Artist,
                CardNumber = em.CardNumber,
                FlavorText = em.FlavorText,
                Price = new CardPriceDm
                {
                    High = em.HighPrice ?? 0,
                    Medium = em.MedianPrice ?? 0,
                    Low = em.LowPrice ?? 0,
                },
                ImageUrl = em.ImageUrl,
                MultiverseId = em.MultiverseId,
                Rarity = em.Rarity,
                StoreUrl = em.StoreUrl
            };

            return dm;
        }

        private static List<CardSetDm> MapSets(List<CardSet> emList)
        {
            return emList.Select(MapSets).ToList();
        }
    }
}
