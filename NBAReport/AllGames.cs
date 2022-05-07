using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAReport
{
    public class AllGames
    {
        List<GameData> games = new List<GameData>();
        public AllGames(){}

        public virtual async Task getData() {}

        public bool containsGames()
        {
            if (games.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
}
