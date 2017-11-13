using System;
using System.Linq;
using MTG_Helper.DAL.DB;

namespace MTG_Helper.DAL.Repositories
{
    public static class TagRespository
    {
        public static void InsertTag(string tagName)
        {
            using (var db = new MtgEntities())
            {
                try
                {
                    var em = new Tag
                    {
                        TagName = tagName
                    };

                    db.Tags.Add(em);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Unable to save the setApi '{tagName}' Excpetion:");
                    Console.WriteLine(e);
                }
            }
        }

        public static void InsertCardTag(string tag, string card)
        {
            using (var db = new MtgEntities())
            {
                try
                {
                    var emTag = db.Tags.Single(t => t.TagName == tag);
                    var emCard = db.Cards.Single(c => c.CardName == card);

                    var emCardTag = new CardTag
                    {
                        CardId = emCard.CardId,
                        TagId = emTag.TagId
                    };

                    db.CardTags.Add(emCardTag);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Unable to save the tag {tag} to the card {card}.");
                    Console.WriteLine(e);
                }
            }
        }
    }
}
