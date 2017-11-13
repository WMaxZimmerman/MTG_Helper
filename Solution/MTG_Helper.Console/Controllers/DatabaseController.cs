using System.Collections.Generic;
using System.IO;
using MTG_Helper.BLL.BLLs;
using MVCLI.Attributes;

namespace mtg.Controllers
{
    [CliController("database", "A set of commands to interact with the database.")]
    public static class DatabaseController
    {
        [CliCommand("update", "A command to update the database to include any missing sets/cards.")]
        public static void Update()
        {
            ApiBLL.UpdateDatabase();
        }

        [CliCommand("initTags", "A command to initialize the database to include tags for cards.")]
        public static void InitializeTagsFromFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var tagsSoFar = new List<string>();

            foreach (var line in lines)
            {
                var lineSplit = line.Split('|');
                var category = lineSplit[0];
                var subcategory = lineSplit[1];
                var cardName = lineSplit[2];

                if (!tagsSoFar.Contains(category))
                {
                    TagBLL.AddTag(category);
                    tagsSoFar.Add(category);
                }
                TagBLL.AddTagToCard(cardName, category);

                if (subcategory != "")
                {
                    if (!tagsSoFar.Contains(subcategory))
                    {
                        TagBLL.AddTag(subcategory);
                        tagsSoFar.Add(subcategory);
                    }
                    TagBLL.AddTagToCard(cardName, subcategory);
                }
            }

        }
    }
}
