using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class FRCEvent
    {
        public string sEventName, sEventCode;
        public List<SimpleTeam> simpleTeamList;
        //List<Alliance> allianceList;
        public List<TeamMatch> teamMatchList;
        public List<Match> matchList;
        public List<ScheduleItem> scheduleItemList;
        public List<SimpleMatch> simpleMatchList;
    }
}
