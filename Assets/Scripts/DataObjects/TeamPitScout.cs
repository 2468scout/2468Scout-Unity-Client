using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class TeamPitScout 
    {
        public int iSpeed; //0-5
        public int iFuelCapacity, iTeamNumber; 
        string sEventCode;
        public bool bCanHighGoal, bCanGears, bCanLowGoal, bCanClimb, bCanHopper, bCanIntake;
        public TeamPitScout(int iTeamNumber, string sEventCode)
        {
            this.iTeamNumber = iTeamNumber;
            this.sEventCode = sEventCode;
        }
    }
}
