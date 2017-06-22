namespace MTG_Helper.BLL.ViewModels
{
    public class DeckStatsVm
    {
        public string DeckName { get; set; }

        public int CreatureCount { get; set; }

        public int LandCount { get; set; }

        public int ArtifactCount { get; set; }

        public int EnchantmentCount { get; set; }

        public int PlaneswalkerCount { get; set; }

        public int InstantCount { get; set; }

        public int SorceryCount { get; set; }
    }
}
