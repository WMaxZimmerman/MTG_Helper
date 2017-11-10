using System.Collections.Generic;

namespace mtg.Models
{
    [System.AttributeUsage(System.AttributeTargets.Method | System.AttributeTargets.Struct)]
    public class CliCommand: System.Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CliCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
