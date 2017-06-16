using System;
using System.Collections.Generic;
using MTG_Helper.DAL.DomainModels;
using MTG_Helper.DAL.Repositories;
using NUnit.Framework;

namespace MTG_Helper.Tests
{
    [TestFixture]
    public class CardTests
    {
        [Test]
        public void AbleToRetrieveCardByName()
        {
            var cardName = "Seshiro the Anointed";
            var card = CardRepository.GetCardByName(cardName);

            Assert.IsTrue(card.Name == cardName);
        }

        [Test]
        public void AbleToRetrieveLegalCards()
        {
            var format = "staNdard";
            List<CardDm> cards = CardRepository.GetLegalCardsForGivenFormat(format);

            Assert.IsTrue(cards.Count == 1249);
        }
    }
}
