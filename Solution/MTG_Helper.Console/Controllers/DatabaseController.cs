using mtg.Models;
using MTG_Helper.BLL.BLLs;

namespace mtg.Controllers
{
    [CliController("database", "A set of commands to interact with the database.")]
    public static class DatabaseController
    {
        [CliCommand("update", "A command to update the database to include any missing sets/cards.")]
        public static void Update()
        {
            ApiBLL.UpdateDatabase();
        }
    }
}
