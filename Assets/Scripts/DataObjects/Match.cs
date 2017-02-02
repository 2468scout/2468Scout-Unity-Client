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
        public ScoreScout redScoreScout, blueScoreScout;
    }
}
