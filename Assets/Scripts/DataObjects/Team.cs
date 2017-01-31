using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Team
    {
        string sTeamName;
        int iTeamNumber, iNumPictures;
        List<FRCAward> awardsList;

        //ADD GAME SPECIFIC STATS
        float fAvgGearsPerMatch, fAvgHighFuelPerMatch, fAvgLowFuelPerMatch, fAvgRankingPoints, fHighGoalAccuracy, fClimbAttemptPercent, fTouchpadPercent, fPenaltyLikelihood, fBreakdownLikelihood, fStuckLikelihood;
        string sBestRole;
        List<string> sNotesList;
        int iGamesScouted, iSpeed, iWeight, iStartingPosition, iTeamAge;
    }
}
