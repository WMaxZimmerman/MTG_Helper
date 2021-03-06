//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MTG_Helper.DAL.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class CardSet
    {
        public int CardSetId { get; set; }
        public string CardId { get; set; }
        public string SetId { get; set; }
        public string CardNumber { get; set; }
        public long MultiverseId { get; set; }
        public string Artist { get; set; }
        public string Rarity { get; set; }
        public string FlavorText { get; set; }
        public string ImageUrl { get; set; }
        public string StoreUrl { get; set; }
        public Nullable<decimal> HighPrice { get; set; }
        public Nullable<decimal> MedianPrice { get; set; }
        public Nullable<decimal> LowPrice { get; set; }
    
        public virtual Card Card { get; set; }
        public virtual Set Set { get; set; }
    }
}
