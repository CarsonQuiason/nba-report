using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAReport
{

    //GameData contains information pertaining to a game's information.
    public class GameData
    {
        public string HomeName { get; }
        public string AwayName { get; }
        public string HomeLogo { get; }
        public string AwayLogo { get; }
        public int HomeScore { get; }
        public int AwayScore { get; }
        public string ArenaName { get; }
        public int Quarter { get; }

        public string Title { get;  }

        //Dated Game
        public GameData(string homeName, string awayName, string homeLogo, string awayLogo, int homeScore, int awayScore, string arenaName)
        {
            HomeName = homeName;
            AwayName = awayName;
            HomeLogo = homeLogo;
            AwayLogo = awayLogo;
            HomeScore = homeScore;
            AwayScore = awayScore;
            ArenaName = arenaName;
            Title = homeName + " vs. " + awayName; 
        }

        //Constructor overload for Live Game
        public GameData(string homeName, string awayName, string homeLogo, string awayLogo, int homeScore, int awayScore, string arenaName, int quarter)
        {
            HomeName = homeName;
            AwayName = awayName;
            HomeLogo = homeLogo;
            AwayLogo = awayLogo;
            HomeScore = homeScore;
            AwayScore = awayScore;
            ArenaName = arenaName;
            Quarter = quarter;
            Title = homeName + " vs. " + awayName;
        }



    }
}
