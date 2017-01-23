﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Assets.Scripts
{
    [System.Serializable]
    public class TeamMatch
    {
        public int iTeamNumber, iMatchNumber, iNumberInAlliance, iAllianceNumber;
        public string sNotes, sFileName, sEventCode, sPersonScouting;
        public bool bColor; // False is blue, true is red
        
        // GAME SPECIFIC ELEMENTS
        public List<MatchEvent> matchEventList;
        public TeamMatch(int iTeamNumber, int iMatchNumber, bool bColor)
        {
            this.iTeamNumber = iTeamNumber;
            this.iMatchNumber = iMatchNumber;
            this.bColor = bColor;
            matchEventList = new List<MatchEvent>();
        }
    }
}
