using Microsoft.VisualStudio.TestTools.UnitTesting;
using BigDeckPlays.DAL.Repositories;
using Moq;

namespace BigDeckPlays.Tests.DAL.Repositories.ScryFallRepository
{
    [TestClass]
    public class TestScryfallRepository
    {
        protected Mock<IApiRepository> _mockApi;
        
        protected ScryfallRepository _repo;

        [TestInitialize]
        public virtual void Init()
        {
            _repo = new ScryfallRepository();
        }
    }
}
