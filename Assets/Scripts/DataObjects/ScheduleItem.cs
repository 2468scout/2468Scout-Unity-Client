using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    [System.Serializable]
    public class ScheduleItem
    {
        string sPersonResponsible,
            sEventType, sEventCode; // pitScout, matchScout, scoreScout
        int iMatchNumber, iTeamNumber;
    }
}
