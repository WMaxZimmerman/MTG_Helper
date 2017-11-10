namespace mtg.Models
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class CliController: System.Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CliController(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
