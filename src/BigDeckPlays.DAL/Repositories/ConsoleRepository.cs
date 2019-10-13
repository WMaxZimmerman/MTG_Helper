using System;

namespace BigDeckPlays.DAL.Repositories
{
    public interface IConsoleRepository
    {
        void WriteLine(string message);
    }

    public class ConsoleRepository : IConsoleRepository
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
