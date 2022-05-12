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
	 * Class makes an API call to get information about an NBA player given their last name
	 */
	public class SearchPlayer
	{
		public string Name { get; }
		public List<PlayerData> playerList = new List<PlayerData>();

		public SearchPlayer(string name)
        {
			Name = name;
        }

		/*
         * Grabs all NBA players with matching last names
         * Makes an api call to "https://api-nba-v1.p.rapidapi.com/players?search=[NAME]
         * Parses JSON data, assigns to a newly created PlayerData class
         * Stores PlayerData in List<PlayerData>
         */
		public async Task getData()
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://api-nba-v1.p.rapidapi.com/players?search="+Name),
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
                    try
                    {
						var player = token.SelectToken("response[" + i + "]");
						string fname = player["firstname"].ToString();
						string lname = player["lastname"].ToString();
						string bday = player["birth"]["date"].ToString();
						int feet = Convert.ToInt32(player["height"]["feets"]);
						int inch = Convert.ToInt32(player["height"]["inches"]);
						int ibs = Convert.ToInt32(player["weight"]["pounds"]);
						int num = Convert.ToInt32(player["leagues"]["standard"]["jersey"]);
						string pos = player["leagues"]["standard"]["pos"].ToString();
						PlayerData playerdata = new PlayerData(fname, lname, bday, feet, inch, ibs, num, pos);
						playerList.Add(playerdata);
                    }
                    catch //If values are null, move to next player
                    {
						continue;
                    }
				}
			}
		}
	}
}
