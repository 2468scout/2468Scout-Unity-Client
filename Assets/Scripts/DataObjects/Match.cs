using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    [System.Serializable]
    class Match
    {
        public int iMatchNumber, iRedScore, iBlueScore, iRedRankingPoints, iBlueRankingPoints;
        public string sCompetitionLevel, sEventPlayedAtCode;
        public List<TeamMatch> teamMatchList;
        public List<Time> blueIncrease1TimeList, blueIncrease5TimeList, blueIncrease40TimeList, blueIncrease50TimeList, blueIncrease60TimeList;
        public List<Time> redIncrease1TimeList, redIncrease5TimeList, redIncrease40TimeList, redIncrease50TimeList, redIncrease60TimeList;
    }
}
