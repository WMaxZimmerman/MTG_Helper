using System.Collections.Generic;
using System.Threading.Tasks;
using BigDeckPlays.DAL.Repositories;

namespace BigDeckPlays.ApplicationCore.Services
{
    public class ExampleService
    {
        public static string HelloWorld()
        {
            var scry = new ScryfallRepository();
            scry.GetCards();
            return ExampleRepository.HelloWorld();
        }

        public static void GetCardsByName(IEnumerable<string> names)
        {
            var scry = new ScryfallRepository();
            scry.GetCards(names);
        }
    }
}
