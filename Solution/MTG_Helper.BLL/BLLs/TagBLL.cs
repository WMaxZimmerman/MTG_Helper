using MTG_Helper.DAL.Repositories;

namespace MTG_Helper.BLL.BLLs
{
    public static class TagBLL
    {
        public static void AddTag(string tag)
        {
            TagRespository.InsertTag(tag);
        }

        public static void AddTagToCard(string card, string tag)
        {
            TagRespository.InsertCardTag(tag, card);
        }
    }
}
