using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BigDeckPlays.Shared.Models;
using System.Linq;
using Moq;

namespace BigDeckPlays.Tests.ApplicationCore.Services.ScryFallService
{
    [TestClass]
    public class TestGetCards: TestScryfallService
    {        
        [TestInitialize]
        public override void Init()
        {
            base.Init();
        }

        [TestMethod]
        public void GetCard_CallsApi_ForEachName()
        {
            var expectedUrl = "something";
            _mockRepo.Setup(r => r.GetFuzzyCardUrl(It.IsAny<string>())).Returns(expectedUrl);
            var expectedCards = new List<string>
            {
                "sol ring",
                "mystic ramora",
                "carpet of flowers"
            };

            _service.GetCards(expectedCards);

            _mockApi.Verify(a => a.Get<Card>(expectedUrl), Times.Exactly(expectedCards.Count));
        }

        [TestMethod]
        public void GetCard_Returns_Card_ForEachName()
        {
            var expectedCards = new List<string>
            {
                "sol ring",
                "mystic ramora",
                "carpet of flowers"
            };

            var actual = _service.GetCards(expectedCards).ToList();

            Assert.AreEqual(expectedCards.Count, actual.Count);
        }
    }
}
