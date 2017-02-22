using System;
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
        public List<string> listTeamsBlueAlliance, listTeamsRedAlliance;
        public List<SimpleTeam> simpleTeamsRed, simpleTeamsBlue;
    }
}
