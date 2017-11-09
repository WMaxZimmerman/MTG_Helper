namespace mtg.Models
{
    [System.AttributeUsage(System.AttributeTargets.Method | System.AttributeTargets.Struct)]
    public class CommandAttribute: System.Attribute
    {
        public string Name { get; set; }
        public string Parameter { get; set; }

        public CommandAttribute(string name, string parameter)
        {
            Name = name;
            Parameter = parameter;
        }
    }
}
