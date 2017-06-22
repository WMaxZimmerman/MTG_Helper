

using System.Collections.Generic;
using System.Linq;
using MTG_Helper.BLL.ViewModels;
using MTG_Helper.DAL.DomainModels;

namespace MTG_Helper.BLL.Mappers
{
    public static class CardMapper
    {
        public static IEnumerable<CardVm> Map(IEnumerable<CardDm> dmlist)
        {
            return dmlist.Select(Map);
        }

        public static CardVm Map(CardDm dm)
        {
            var vm = new CardVm
            {
                Id = dm.Id,
                Name = dm.Name,
                Cmc = dm.Cmc,
                Cost = dm.Cost,
                Legality = new CardLegalityVm
                {
                    Commander = dm.Legality.Commander,
                    Legacy = dm.Legality.Legacy,
                    Modern = dm.Legality.Modern,
                    Standard = dm.Legality.Standard,
                    Vintage = dm.Legality.Vintage
                },
                Power = dm.Power,
                Toughness = dm.Toughness,
                RulesText = dm.RulesText,
                StoreUrl = dm.StoreUrl,
                Url = dm.Url,
                Types = dm.Types,
                SubTypes = dm.SubTypes,
                Colors = dm.Colors,
                Sets = MapSets(dm.Sets).ToList()
            };
            return vm;
        }

        private static CardSetVm MapSets(CardSetDm dm)
        {
                var vm = new CardSetVm
                {
                    Id = dm.Id,
                    SetId = dm.SetId,
                    CardId = dm.CardId,
                    Artist = dm.Artist,
                    CardNumber = dm.CardNumber,
                    FlavorText = dm.FlavorText,
                    Price = new CardPriceVm
                    {
                        High = dm.Price.High,
                        Medium = dm.Price.Medium,
                        Low = dm.Price.Low,
                    },
                    ImageUrl = dm.ImageUrl,
                    MultiverseId = dm.MultiverseId,
                    Rarity = dm.Rarity,
                    StoreUrl = dm.StoreUrl
                };
                return vm;
        }

        private static IEnumerable<CardSetVm> MapSets(IEnumerable<CardSetDm> dmlist)
        {
            return dmlist.Select(MapSets).ToList();
        }
    }
}
