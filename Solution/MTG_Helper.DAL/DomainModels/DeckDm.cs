using System.Collections.Generic;

namespace MTG_Helper.DAL.DomainModels
{
    public class DeckDm
    {
        public DeckDm()
        {
            Cards = new List<CardDm>();
        }

        public string DeckName { get; set; }

        public CardDm Commander { get; set; }

        public List<CardDm> Cards { get; set; }
    }
}
