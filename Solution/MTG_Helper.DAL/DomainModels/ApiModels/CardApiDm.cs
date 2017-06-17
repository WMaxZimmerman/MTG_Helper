using System.Collections.Generic;

namespace MTG_Helper.DAL.DomainModels.ApiModels
{
    public class CardApiDm
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Store_Url { get; set; }

        public string Cmc { get; set; }

        public string Cost { get; set; }

        public string Text { get; set; }

        public List<string> Colors { get; set; }

        public List<string> Supertypes { get; set; }

        public List<string> Types { get; set; }

        public List<string> Subtypes { get; set; }

        public string Power { get; set; }

        public string Toughness { get; set; }

        public CardLegalityApiDm Formats { get; set; }

        public List<CardSetApiDm> Editions { get; set; }
    }
}
