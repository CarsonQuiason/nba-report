using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NBAReport
{

	/*
	 * Class makes an API call to get information about NBA games that are currently live
	 */
	public partial class LiveGames : Games
	{
		public LiveGames() {}


		/*
         * Grabs all NBA live NBA games
         * Makes an api call to https://api-nba-v1.p.rapidapi.com/games?live=all
         * Parses JSON data, assigns to a newly created GameData class
         * Stores GameData in List<GameData>
         */
		public async Task getData()
        {
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://api-nba-v1.p.rapidapi.com/games?live=all"),
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
					int quarter = Convert.ToInt32(game["periods"]["current"]);
					GameData gamedata = new GameData(homeName, awayName, homeLogo, awayLogo, homeScore, awayScore, arenaName, quarter);
					gameList.Add(gamedata);
				}
			}
		}
	}
}
