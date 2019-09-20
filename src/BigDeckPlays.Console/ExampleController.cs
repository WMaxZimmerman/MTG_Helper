using System.Collections.Generic;
using BigDeckPlays.ApplicationCore.Services;
using NTrospection.CLI.Attributes;

namespace BigDeckPlays.Console.Controllers
{
    [CliController("example", "An example of a CLI Controller")]
    public class ExampleController
    {
        [CliCommand("hello", "A Hello World for a CLI Project")]
        public void HelloWorld()
        {
            var message = ExampleService.HelloWorld();
            System.Console.WriteLine(message);
        }
        
        [CliCommand("test", "A Hello World for a CLI Project")]
        public void HelloWorld(List<string> names)
        {
            ExampleService.GetCardsByName(names);
        }
    }
}
