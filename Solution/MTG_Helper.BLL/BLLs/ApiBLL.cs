using System;
using System.Linq;
using MTG_Helper.DAL.DALs;
using MTG_Helper.DAL.Repositories;

namespace MTG_Helper.BLL.BLLs
{
    public static class ApiBLL
    {
        public static void UpdateDatabase()
        {
            var apiSets = MtgApi.GetAllSets();
            var dbSets = SetRepository.GetAllSets();

            var count = 1;
            var total = apiSets.Count(a => dbSets.All(d => d.Id != a.Id));

            foreach (var set in apiSets.Where(a => dbSets.All(d => d.Id != a.Id)))
            {
                SetRepository.InsertSet(set);

                Console.WriteLine();
                Console.WriteLine($"Retreiving all cards for the set {set.Name}({set.Id}): {count}/{total}");

                var cards = MtgApi.GetAllCardsForSet(set.Id);
                CardRepository.InsertCards(cards, set.Id);

                count++;
            }
        }
    }
}
