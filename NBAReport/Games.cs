using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAReport
{
    public class Games
    {
        public List<GameData> gamesList = new List<GameData>();
        public Games(){}

        public virtual async Task getData() {}

        public bool containsGames()
        {
            if (gamesList.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
}
