using System.Collections.Generic;
using mtg.Models;
using mtg.Views;
using MTG_Helper.BLL.BLLs;

namespace mtg.Controllers
{
    [CliController("cards", "A set of commands to interact with or retrieve cards.")]
    public static class CardController
    {
        [CliCommand("find", "A command to search for cards.")]
        public static void FindCards(string tribal = null, string name = null, bool? commander = null, string colors = null, string type = null)
        {
            var cards = CardBLL.SearchCards(tribal, name, commander, colors, type);

            foreach (var card in cards)
            {
                Output.DrawCard(card);
            }
        }
    }
}
