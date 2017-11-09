using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MTG_Helper.BLL.Mappers;
using MTG_Helper.BLL.ViewModels;
using MTG_Helper.DAL.DALs;
using MTG_Helper.DAL.DomainModels;
using MTG_Helper.DAL.Repositories;

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

        public static List<string> GetCardColors(CardVm card)
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

        public static CardVm GetCardByName(string cardName)
        {
            return CardMapper.Map(CardRepository.GetCardByName(cardName));
        }

        public static IEnumerable<CardVm> GetCommandersByPartialName(string cardName)
        {
            return CardMapper.Map(CardRepository.GetCommandersByPartialName(cardName));
        }

        public static IEnumerable<CardVm> GetAllCards()
        {
            return CardMapper.Map(CardRepository.GetAllCards());
        }

        public static IEnumerable<CardVm> FindTribalCommandersForType(string tribalType)
        {
            return CardMapper.Map(CardRepository.FindTribalCommandersForType(tribalType));
        }
    }
}
