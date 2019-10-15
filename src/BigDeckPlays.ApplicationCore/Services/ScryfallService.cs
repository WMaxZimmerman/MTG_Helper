using System.Collections.Generic;
using System.Linq;
using BigDeckPlays.Shared.Models;
using BigDeckPlays.DAL.Repositories;

namespace BigDeckPlays.ApplicationCore.Services
{
    public interface IScryfallService
    {
        IEnumerable<Card> GetCards(IEnumerable<string> cardNames);
    }
    
    public class ScryfallService : IScryfallService
    {
        private IApiRepository _api;
        private IScryfallRepository _repo;
        
        public ScryfallService(IApiRepository api = null,
                               IScryfallRepository repo = null)
        {
            _api = api ?? new ApiRepository();
            _repo = repo ?? new ScryfallRepository();
            
        }
        
        public IEnumerable<Card> GetCards(IEnumerable<string> cardNames)
        {
            var cards = new List<Card>();
            
            foreach(var name in cardNames)
            {
                var url = _repo.GetFuzzyCardUrl(name);
                var card = _api.Get<Card>(url);
                cards.Add(card);
            }

            return cards;
        }
    }
}
