using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class TeamPitScout 
    {
        public int iSpeed; //0-5
        public int iFuelCapacity;
        public SimpleTeam currentlyScoutedTeam; 
        string sEventCode;
        public bool bCanHighGoal, bCanGears, bCanLowGoal, bCanClimb, bCanHopper, bCanIntake;
    }
}
