using System.Linq;
using MTG_Helper.DAL.Repositories;
using NUnit.Framework;

namespace MTG_Helper.Tests.DAL
{
    [TestFixture]
    public class CardRepositoryTests
    {
        [Test]
        public void AbleToRetrieveCardByName()
        {
            const string cardName = "Seshiro the Anointed";
            var card = CardRepository.GetCardByName(cardName);

            Assert.IsTrue(card.Name == cardName);
        }

        [Test]
        public void AbleToRetrieveCommanderListByPartialName()
        {
            const string cardName = "seshiro";
            var cards = CardRepository.GetCommandersByPartialName(cardName);

            Assert.IsTrue(cards.Count() == 3);
        }

        [Test]
        public void AbleToRetrieveLegalCards()
        {
            const string format = "staNdard";
            var cards = CardRepository.GetLegalCardsForGivenFormat(format);
            var cardCount = cards.Count();

            Assert.IsTrue(cardCount == 1902);
        }

        [Test]
        public void GetLegalCardsForGivenFormatReturnsEmptyListWhenGivenInvalidFormat()
        {
            const string format = "badFormat";
            var cards = CardRepository.GetLegalCardsForGivenFormat(format);

            Assert.IsTrue(!cards.Any());
        }

        [Test]
        public void AbleToRetrieveCardsBySubType()
        {
            const string subtype = "snake";
            var cards = CardRepository.GetAllCardsByGivenSubtype(subtype);
            var cardCount = cards.Count();

            Assert.IsTrue(cardCount == 76);
        }
    }
}
