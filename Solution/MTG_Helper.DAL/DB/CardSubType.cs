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
    
    public partial class CardSubType
    {
        public int CardSubTypeId { get; set; }
        public string CardId { get; set; }
        public int SubTypeId { get; set; }
    
        public virtual Card Card { get; set; }
        public virtual SubType SubType { get; set; }
    }
}
