﻿using System.Collections.Generic;


namespace MTG_Helper.BLL.ViewModels
{
   public class CardVm
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

        public CardLegalityVm Legality { get; set; }

        public List<CardSetVm> Sets { get; set; }

        public int Quantity { get; set; } 
    }
}
