

namespace MTG_Helper.BLL.ViewModels
{
    public class CardSetVm
    {
        public int Id { get; set; }

        //Set Properties
        public string SetId { get; set; }

        public string Set { get; set; }

        public string Layout { get; set; }

        public string SetUrl { get; set; }

        public string HtmlUrl { get; set; }

        //Card Properties
        public string CardId { get; set; }

        public string FlavorText { get; set; }

        public string CardNumber { get; set; }

        public long MultiverseId { get; set; }

        public string Artist { get; set; }

        public string Rarity { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public string StoreUrl { get; set; }

        public CardPriceVm Price { get; set; }
    }
}
