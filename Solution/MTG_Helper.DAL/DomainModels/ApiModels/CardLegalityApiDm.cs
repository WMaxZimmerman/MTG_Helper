namespace MTG_Helper.DAL.DomainModels.ApiModels
{
    public class CardLegalityApiDm
    {
        public string Commander { get; set; }

        public string Modern { get; set; }

        public string Standard { get; set; }

        public string Vintage { get; set; }

        public string Legacy { get; set; }
    }
}
