using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Assets.Scripts
{
    [System.Serializable]
    public class TeamMatch
    {
        public int iTeamNumber, iMatchNumber, iStationNumber, iAllianceNumber;
        public string sNotes, sFileName, sEventCode, sPersonScouting;
        public bool bIsRematch, bColor; // Blue is true
        
        // GAME SPECIFIC ELEMENTS
        public List<MatchEvent> matchEventList;
        public TeamMatch(int iTeamNumber, int iMatchNumber, bool bColor, string sEventCode)
        {
            this.iTeamNumber = iTeamNumber;
            this.iMatchNumber = iMatchNumber;
            this.bColor = bColor;
            this.sEventCode = sEventCode;
            bIsRematch = false;
            matchEventList = new List<MatchEvent>();
            sFileName = "/team" + iTeamNumber + "_match" + iMatchNumber + "_event" + sEventCode;
        }
        public TeamMatch(ScheduleItem item)
        {
            iTeamNumber = item.iTeamNumber;
            iMatchNumber = item.iMatchNumber;
            bColor = item.bColor;
            sEventCode = item.sEventCode;
            iStationNumber = item.iStationNumber;
            bIsRematch = item.bIsRematch;
            matchEventList = new List<MatchEvent>();
            sFileName = "/team" + iTeamNumber + "_match" + iMatchNumber + "_event" + sEventCode;
        }
    }
}
