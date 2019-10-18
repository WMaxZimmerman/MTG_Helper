using System;

namespace BigDeckPlays.Shared.Models
{
    public class Card:  IEquatable<Card>
    {
        public string Name {get;set;}
        public int Quantity {get;set;}
        public double Cmc {get;set;}

        public bool Equals(Card other)
        {
            return this.Name == other.Name;
        }
    }
}
