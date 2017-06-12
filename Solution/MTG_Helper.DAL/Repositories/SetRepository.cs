using System;
using System.Collections.Generic;
using System.Linq;
using MTG_Helper.DAL.DB;
using MTG_Helper.DAL.DomainModels.ApiModels;
using MTG_Helper.DAL.Mappers;

namespace MTG_Helper.DAL.Repositories
{
    public static class SetRepository
    {
        public static void InsertSet(SetApiDm setApi)
        {
            using (var db = new MtgEntities())
            {
                try
                {
                    var em = SetMapper.Map(setApi);
                    db.Sets.Add(em);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Unable to save the setApi '{setApi.Name}' Excpetion:");
                    Console.WriteLine(e);
                }
            }
        }

        public static void InsertSets(List<SetApiDm> sets)
        {
            var count = 1;
            foreach (var set in sets)
            {
                Console.WriteLine($"Writing Set: {count}/{sets.Count()}");
                InsertSet(set);
                count++;
            }
        }
    }
}
