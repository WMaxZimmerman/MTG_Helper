using System.Collections.Generic;
using System.Linq;
using MTG_Helper.DAL.DB;

namespace MTG_Helper.DAL.Mappers
{
    public static class TypeMapper
    {
        public static Type Map(string dm)
        {
            var em = new Type
            {
                TypeId = 0,
                TypeName = dm
            };

            return em;
        }

        public static List<Type> Map(List<string> dmList)
        {
            return dmList.Select(Map).ToList();
        }
    }
}
