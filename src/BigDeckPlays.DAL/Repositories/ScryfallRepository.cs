using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using BigDeckPlays.Shared.Models;
using Newtonsoft.Json;

namespace BigDeckPlays.DAL.Repositories
{
    public interface IScryfallRepository
    {
        IEnumerable<Card> GetCards();
    }
    
    public class ScryfallRepository
    {
        public IEnumerable<Card> GetCards(IEnumerable<string> cardNames)
        {
            var baseurl = "https://api.scryfall.com/cards/named?fuzzy=";

            foreach(var name in cardNames)
            {
                var parsedName = name.Replace(' ', '+');
                var card = GetStuff<Card>(baseurl + parsedName);
                Console.WriteLine(card.Name + " (" + card.Cmc + ")");
            }

            return null;
        }

        public IEnumerable<Card> GetCards()
        {
            // Ends at 1477
            var baseurl = "https://api.scryfall.com/cards?page=1";

            while(baseurl != null)
            {
                var data = GetStuff<ReturnObj>(baseurl);
                baseurl = data?.Next_Page;

                foreach(var card in data.Data.Distinct())
                {
                    Console.WriteLine(card.Name);
                }
            }

            return null;
        }

        private T GetStuff<T>(string url)
        {
            var client = new HttpClient();
            var res = client.GetAsync(url).Result;
            var content = res.Content;

            var rawdata = content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<T>(rawdata);

            return data;
        }
    }

    public class ReturnObj
    {
        public List<Card> Data { get; set; }
        public string Next_Page { get; set; }
    }
}
