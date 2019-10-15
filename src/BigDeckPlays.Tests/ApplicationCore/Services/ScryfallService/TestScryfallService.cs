using Microsoft.VisualStudio.TestTools.UnitTesting;
using BigDeckPlays.DAL.Repositories;
using Moq;
using BigDeckPlays.ApplicationCore.Services;

namespace BigDeckPlays.Tests.ApplicationCore.Services.ScryFallService
{
    [TestClass]
    public class TestScryfallService
    {
        protected Mock<IApiRepository> _mockApi;
        protected Mock<IScryfallRepository> _mockRepo;
        protected ScryfallService _service;

        [TestInitialize]
        public virtual void Init()
        {
            _mockApi = new Mock<IApiRepository>();
            _mockRepo = new Mock<IScryfallRepository>();

            _service = new ScryfallService(_mockApi.Object, _mockRepo.Object);
        }
    }
}
