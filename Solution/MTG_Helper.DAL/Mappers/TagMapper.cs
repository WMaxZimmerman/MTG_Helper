using System.Collections.Generic;
using System.Linq;
using MTG_Helper.DAL.DB;
using MTG_Helper.DAL.DomainModels;

namespace MTG_Helper.DAL.Mappers
{
    public static class TagMapper
    {
        public static TagDM Map(Tag em)
        {
            return new TagDM
            {
                Id = em.TagId,
                Name = em.TagName
            };
        }

        public static IEnumerable<TagDM> Map(IEnumerable<Tag> emList)
        {
            return emList.Select(Map);
        }

        public static Tag Map(TagDM dm)
        {
            return new Tag
            {
                TagId = dm.Id,
                TagName = dm.Name
            };
        }

        public static IEnumerable<Tag> Map(IEnumerable<TagDM> dmList)
        {
            return dmList.Select(Map);
        }
    }
}
