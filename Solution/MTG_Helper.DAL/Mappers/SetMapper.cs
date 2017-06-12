using System.Collections.Generic;
using System.Linq;
using MTG_Helper.DAL.DB;
using MTG_Helper.DAL.DomainModels.ApiModels;

namespace MTG_Helper.DAL.Mappers
{
    public static class SetMapper
    {
        public static Set Map(SetApiDm apiDm)
        {
            var em = new Set
            {
                Url = apiDm.Url,
                SetId = apiDm.Id,
                Border = apiDm.Border,
                SetType = apiDm.Type,
                SetName = apiDm.Name
            };

            return em;
        }

        public static List<Set> Map(List<SetApiDm> dmList)
        {
            return dmList.Select(Map).ToList();
        }
    }
}
