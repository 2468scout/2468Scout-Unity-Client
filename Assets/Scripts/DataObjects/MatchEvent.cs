﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    [System.Serializable]
    public class MatchEvent
    {
        int timeStamp; 
        public int iPointValue, iCount;
        bool bInAutonomous;
        public Point loc;
        public string sEventName;
        public MatchEvent(Time timeStamp, bool bInAutonomous, Point loc)
        {
            this.timeStamp = timeStamp.sumMilliseconds();
            this.bInAutonomous = bInAutonomous;
            this.loc = loc;
        }
        public MatchEvent(Time timeStamp, bool bInAutonomous)
        {
            this.timeStamp = timeStamp.sumMilliseconds();
            this.bInAutonomous = bInAutonomous;
        }
        public MatchEvent(Time timeStamp, bool bInAutonomous, int iCount, string sEventName)
        {
            this.timeStamp = timeStamp.sumMilliseconds();
            this.bInAutonomous = bInAutonomous;
            this.iCount = iCount;
            this.sEventName = sEventName;
        }
        public MatchEvent()
        {

        }
    }
}