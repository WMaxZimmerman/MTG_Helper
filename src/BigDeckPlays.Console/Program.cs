using NTrospection.CLI.Core;

namespace BigDeckPlays.Console
{
   class Program
   {
       static void Main(string[] args)
       {
           new Processor().ProcessArguments(args);
       }
   }
}
