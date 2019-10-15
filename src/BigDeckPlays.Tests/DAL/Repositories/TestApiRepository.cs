using Microsoft.VisualStudio.TestTools.UnitTesting;
using BigDeckPlays.DAL.Repositories;

namespace BigDeckPlays.Tests.DAL.Repositories
{
    [TestClass]
    public class TestApiRepository
    {
        private ApiRepository _repo;

        [TestInitialize]
        public void Init()
        {
            _repo = new ApiRepository();
        }
    }
}
