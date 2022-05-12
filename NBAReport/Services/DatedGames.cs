using Newtonsoft.Json.Linq;
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
    public class DatedGames : Games
    {
        
        public string Date;

        public DatedGames(string date)
        {
            Date = date;
        }
        public DatedGames() {}

        /*
         * Grabs all NBA games given a certain date
         * Makes an api call to https://api-nba-v1.p.rapidapi.com/games?date=[DATE]
         * Parses JSON data, assigns to GameData class
         * Stores GameData in List<GameData>
         */
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
                JToken token = JToken.Parse(body);
                int size = Convert.ToInt32(token.SelectToken("results"));
                for (int i = 0; i < size; i++)
                {
                    var game = token.SelectToken("response[" + i + "]");
                    string homeName = game["teams"]["home"]["name"].ToString();
                    string awayName = game["teams"]["visitors"]["name"].ToString();
                    string homeLogo = game["teams"]["home"]["logo"].ToString();
                    string awayLogo = game["teams"]["visitors"]["logo"].ToString();
                    int homeScore = Convert.ToInt32(game["scores"]["home"]["points"]);
                    int awayScore = Convert.ToInt32(game["scores"]["visitors"]["points"]);
                    string arenaName = game["arena"]["name"].ToString();
                    GameData gamedata = new GameData(homeName, awayName, homeLogo, awayLogo, homeScore, awayScore, arenaName);
                    gameList.Add(gamedata);
                }
            }
        }
    }
}

