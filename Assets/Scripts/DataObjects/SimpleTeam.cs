using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    [System.Serializable]
    public class SimpleTeam
    {
        public string sTeamName;
        public int iTeamNumber;
        public SimpleTeam(string sTeamName, int iTeamNumber)
        {
            this.sTeamName = sTeamName;
            this.iTeamNumber = iTeamNumber;
        }
    }
}
