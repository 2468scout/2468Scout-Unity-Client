using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    [System.Serializable]
    public class Team
    {
        public string sTeamName;
        public int iTeamNumber, iNumPictures;
        public List<FRCAward> awardsList;

        //ADD GAME SPECIFIC STATS
        public float fAvgGearsPerMatch, fAvgHighFuelPerMatch, fAvgLowFuelPerMatch, 
            fAvgRankingPoints, fHighGoalAccuracy, fClimbAttemptPercent, fTouchpadPercent, 
            fPenaltyLikelihood, fBreakdownLikelihood, fStuckLikelihood, fWinPercentage, fWinRate, fQPAverage, fHighFuelPoints, fLowFuelPoints, fAvgFuelPoints, fHighFuelAccuracy, fLowFuelAccuracy, fGearAccuracy, fRPContrib, fQPConrib, fPlayoffContrib, fBaseQPContrib, fBasePContrib, fGearRPContrib, fGearQPContrib, fGearPContrib, fTouchQPContrib, fTouchPContrib, fDefendPercent;
        public string sBestRole, sWTL;
        public List<string> sNotesList;
        public int iGamesScouted, iSpeed, iWeight, iStartingPosition, iTeamAge, iNumMatches, iRank, iWins, iLosses, iTies, iTimesDisqualified, iTotalBaselineMatches, iGearsScored, iTotalGearMatches, iTotalTouchpadMatches;
        public HeatmapsData heatmapsData;
    }
}
