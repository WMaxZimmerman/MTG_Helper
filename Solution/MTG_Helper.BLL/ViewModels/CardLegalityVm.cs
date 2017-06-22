namespace MTG_Helper.BLL.ViewModels
{
   public class CardLegalityVm
    {
        public bool Commander { get; set; }

        public bool Modern { get; set; }

        public bool Standard { get; set; }

        public bool Vintage { get; set; }

        public bool Legacy { get; set; }
    }
}
