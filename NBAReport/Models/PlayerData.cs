using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAReport
{

    //PlayerData contains information pertaining to a player's information.
    public class PlayerData
    {
        public String FirstName { get; }
        public String LastName { get; }
        public String Birth { get; } 
        public int HeightFeet { get; }
        public int HeightInch { get; }
        public int Ibs { get; }
        public int JerseyNum { get; }
        public String Position { get; }
        public String FullName { get; }

        public PlayerData(string fname, string lname, string bday, int feet, int inch, int ibs, int num, string pos)
        {
            FirstName = fname;
            LastName = lname;
            Birth = bday;
            HeightFeet = feet;
            HeightInch = inch;
            Ibs = ibs;
            JerseyNum = num;
            Position = pos;
            FullName = FirstName + " " + LastName;
        }
    }
}
