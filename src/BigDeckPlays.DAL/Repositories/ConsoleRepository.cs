using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using BigDeckPlays.Shared.Models;
using Newtonsoft.Json;

namespace BigDeckPlays.DAL.Repositories
{
    public interface IConsoleRepository
    {
        void WrtieLine(string message);
    }

    public class ConsoleRepository : IConsoleRepository
    {
        public void WrtieLine(string message)
        {
            throw new NotImplementedException();
        }
    }
}
