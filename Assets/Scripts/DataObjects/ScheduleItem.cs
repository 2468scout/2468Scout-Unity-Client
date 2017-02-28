using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    [System.Serializable]
    public class ScheduleItem
    {
        public string sPersonResponsible,
            sItemType, sEventCode; // matchScout, scoreScout
        public int iMatchNumber, iTeamNumber, iStationNumber;
        public bool bColor, bRematch;
    }
}