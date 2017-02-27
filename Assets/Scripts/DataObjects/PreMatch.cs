using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    [System.Serializable]
    public class PreMatch
    {
        public int iMatchNumber;
        public string sEventCode;
        public List<SimpleTeam> redSimpleTeams, blueSimpleTeams;
    }
}
