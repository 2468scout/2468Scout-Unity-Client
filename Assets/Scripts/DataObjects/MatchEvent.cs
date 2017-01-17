using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class MatchEvent
    {
        Time timeStamp; 
        public int iPointValue, iCount;
        bool bInAutonomous;
        Point loc;
        public string sEventName;
        public MatchEvent(Time timeStamp, bool bInAutonomous, Point loc)
        {
            this.timeStamp = timeStamp;
            this.bInAutonomous = bInAutonomous;
            this.loc = loc;
        }
        public MatchEvent()
        {

        }
    }
}