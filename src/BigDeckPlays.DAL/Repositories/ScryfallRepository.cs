namespace BigDeckPlays.DAL.Repositories
{
    public interface IScryfallRepository
    {
        string GetFuzzyCardUrl(string card);
    }
    
    public class ScryfallRepository : IScryfallRepository
    {
        private IApiRepository _api;
        public const string HostUrl = "https://api.scryfall.com/";
        
        public ScryfallRepository()
        {
        }

        public string GetFuzzyCardUrl(string card)
        {
            var baseurl = HostUrl + "cards/named?fuzzy=";
            var parsedName = card.Replace(' ', '+');

            return baseurl + parsedName;
        }
    }
}
