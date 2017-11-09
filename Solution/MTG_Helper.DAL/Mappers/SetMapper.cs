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

        public static IEnumerable<Set> Map(IEnumerable<SetApiDm> dmList)
        {
            return dmList.Select(Map).ToList();
        }

        public static SetApiDm Map(Set em)
        {
            var dm = new SetApiDm
            {
                Url = em.Url,
                Id = em.SetId,
                Border = em.Border,
                Type = em.SetType,
                Name = em.SetName
            };

            return dm;
        }

        public static IEnumerable<SetApiDm> Map(IEnumerable<Set> emList)
        {
            return emList.Select(Map).ToList();
        }
    }
}
