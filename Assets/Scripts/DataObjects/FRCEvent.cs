using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class FRCEvent
    {
        string sEventName, sEventCode;
        List<string> teamNameList;
        //List<Alliance> allianceList;
        public List<TeamMatch> teamMatchList;
        List<SimpleMatch> matchList;
        public List<string> listNamesByTeamMatch;
    }
}
