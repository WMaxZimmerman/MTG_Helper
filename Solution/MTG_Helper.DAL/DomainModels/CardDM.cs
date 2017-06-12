using System.Collections.Generic;
using MTG_Helper.DAL.DomainModels.ApiModels;

namespace MTG_Helper.DAL.DomainModels
{
    public class CardDm
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string StoreUrl { get; set; }

        public string Cmc { get; set; }

        public string Cost { get; set; }

        public string RulesText { get; set; }

        public List<string> Colors { get; set; }

        public List<string> Types { get; set; }

        public List<string> SubTypes { get; set; }

        public string Power { get; set; }

        public string Toughness { get; set; }

        public CardLegalityDm Legality { get; set; }

        public List<CardSetDm> Sets { get; set; }
    }
}
