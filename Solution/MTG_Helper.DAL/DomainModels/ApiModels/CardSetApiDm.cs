namespace MTG_Helper.DAL.DomainModels.ApiModels
{
    public class CardSetApiDm
    {
        public string Set { get; set; }

        public string Set_Id { get; set; }

        public string Rarity { get; set; }

        public string Artist { get; set; }

        public string Multiverse_Id { get; set; }

        public string Flavor { get; set; }

        public string Number { get; set; }

        public string Layout { get; set; }
        
        public CardPriceApiDm PriceApi { get; set; }

        public string Url { get; set; }

        public string Image_Url { get; set; }

        public string Set_Url { get; set; }

        public string Store_Url { get; set; }

        public string Html_Url { get; set; }
    }
}
