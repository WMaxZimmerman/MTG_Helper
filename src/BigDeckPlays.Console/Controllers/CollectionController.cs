using BigDeckPlays.ApplicationCore.Services;
using NTrospection.CLI.Attributes;

namespace BigDeckPlays.Console.Controllers
{
    [CliController("collection", "")]
    public static class CollectionController
    {
        [CliCommand("print", "")]
        public static void OutputAllCard()
        {
            var collection = new CollectionService();
            collection.GetCards();
        }
        
        [CliCommand("transfer", "")]
        public static void TransferCardsToTempFile()
        {
            var collection = new CollectionService();
            collection.TransferCards();
        }
    }
}
