using System.Collections.Generic;
using System.Text.RegularExpressions;
using MTG_Helper.DAL.DomainModels;

namespace MTG_Helper.BLL.BLLs
{
    public static class CardBLL
    {
        public static IEnumerable<string> GetPossibleColors()
        {
            return new List<string>
            {
                "black",
                "green",
                "blue",
                "red",
                "white"
            };
        }

        public static List<string> GetCardColors(CardDm card)
        {
            var colorList = ExtractColorsFromString(card.Cost);
            colorList.AddRange(ExtractColorsFromString(card.RulesText));

            return colorList;
        }

        private static List<string> ExtractColorsFromString(string s)
        {
            var colorList = new List<string>();

            var costColors = Regex.Matches(s, "{[WUBRG]}");
            foreach (var c in costColors)
            {
                colorList.Add(c.ToString());
            }

            return colorList;
        }
    }
}
