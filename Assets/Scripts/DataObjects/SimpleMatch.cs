﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    [System.Serializable]
    public class SimpleMatch
    {
        public int iMatchNumber, iRedScore, iBlueScore;
        public string sCompetitionLevel, sEventPlayedAtCode;
        public List<SimpleTeam> simpleTeamsRed, simpleTeamsBlue;
        public bool bHasBeenPlayed;
        public override string ToString()
        {
            return sCompetitionLevel + iMatchNumber;
        }
    }
}
