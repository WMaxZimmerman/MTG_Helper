using mtg.Views;
using MTG_Helper.BLL.BLLs;
using MVCLI.Attributes;

namespace mtg.Controllers
{
    [CliController("cards", "A set of commands to interact with or retrieve cards.")]
    public static class CardController
    {
        [CliCommand("find", "A command to search for cards.")]
        public static void FindCards(string tribal = null, string name = null, bool? commander = null, string colors = null, string type = null, string tag = null, int numberToReturn = -1)
        {
            var cards = CardBLL.SearchCards(tribal, name, commander, colors, type, tag, numberToReturn);

            foreach (var card in cards)
            {
                Output.DrawCard(card);
            }
        }

        [CliCommand("addTag", "A command to add a tag")]
        public static void FindCards(string tag)
        {
            TagBLL.AddTag(tag);
        }

        [CliCommand("tagCard", "A command to add a tag to a card")]
        public static void FindCards(string cardName, string tag)
        {
            TagBLL.AddTagToCard(cardName, tag);
        }
    }
}
