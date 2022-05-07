using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


/*
 * Class contains information about NBA games given a user input of date
 */
namespace NBAReport
{
    public class GameDate : AllGames
    {
        public string Date;

        public GameDate(string date) 
        {
            Date = date;
        }
        public GameDate() {}

        public override async Task getData()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api-nba-v1.p.rapidapi.com/games?date=" + Date),
                Headers =
                    {
                        { "X-RapidAPI-Host", "api-nba-v1.p.rapidapi.com" },
                        { "X-RapidAPI-Key", "" },
                    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
        }
    }
}
