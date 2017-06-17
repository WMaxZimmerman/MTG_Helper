using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using MTG_Helper.DAL.DomainModels.ApiModels;

namespace MTG_Helper.DAL.DALs
{
    public static class MtgApi
    {
        private const string Url = "https://api.deckbrew.com/mtg";

        public static List<CardApiDm> GetCardsByPage(int pageNumber)
        {
            var client = new HttpClient {BaseAddress = new Uri($"{Url}/cards")};

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            var response = client.GetAsync($"?page={pageNumber}").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var dataObjects = response.Content.ReadAsAsync<List<CardApiDm>>().Result;
                return dataObjects;
            }
            else
            {
                return new List<CardApiDm>();
            }
        }

        public static List<CardApiDm> GetCardsByPageRange(int startPage, int endPage)
        {
            var cards = new List<CardApiDm>();

            for (var i = startPage; i <= endPage; i++)
            {
                System.Console.WriteLine($"Reading Page: {i}/{endPage}");
                cards.AddRange(GetCardsByPage(i));
            }

            return cards;
        }

        public static List<CardApiDm> GetAllCards()
        {
            var cards = new List<CardApiDm>();

            for (var i = 0; i < 400; i++)
            {
                cards.AddRange(GetCardsByPage(i));
            }

            return cards;
        }

        public static List<SetApiDm> GetAllSets()
        {
            var client = new HttpClient {BaseAddress = new Uri($"{Url}/sets")};

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            var response = client.GetAsync("").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var dataObjects = response.Content.ReadAsAsync<List<SetApiDm>>().Result;
                return dataObjects;
            }
            else
            {
                return new List<SetApiDm>();
            }
        }
    }
}
