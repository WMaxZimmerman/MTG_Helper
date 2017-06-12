using System.Collections.Generic;
using System.Linq;
using MTG_Helper.DAL.DB;

namespace MTG_Helper.DAL.Mappers
{
    public static class ColorMapper
    {
        public static Color Map(string dm)
        {
            var em = new Color
            {
                ColorId = 0,
                ColorName = dm
            };

            return em;
        }

        public static List<Color> Map(List<string> dmList)
        {
            return dmList.Select(Map).ToList();
        }
    }
}
