using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NBAReport
{
	public class LiveGame : AllGames
	{
		public bool containsGames = false;
		public LiveGame() {}

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
				Console.WriteLine(body);
				}
			}
	}
}
