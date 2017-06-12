using System.Collections.Generic;
using System.Linq;
using MTG_Helper.DAL.DB;

namespace MTG_Helper.DAL.Mappers
{
    public static class SubTypeMapper
    {
        public static SubType Map(string dm)
        {
            var em = new SubType
            {
                SubTypeId = 0,
                SubTypeName = dm
            };

            return em;
        }

        public static List<SubType> Map(List<string> dmList)
        {
            return dmList.Select(Map).ToList();
        }
    }
}
