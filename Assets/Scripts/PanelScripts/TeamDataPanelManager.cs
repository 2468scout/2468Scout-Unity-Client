using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TeamDataPanelManager : MonoBehaviour
    {
        UIManager manager;
        public Team team = null;
        public SimpleTeam simpleTeam;
        Button backButton, leftButton, rightButton;

        Text teamNameNumberText, leftButtonText, rightButtonText, gamesScoutedText, winPercentageText, backButtonText;
        
            //General Texts
            Text generalText, highGoalsPerGameText, lowGoalsPerGameText, gearsPerGameText, pointContText, rpPerGameText, generalSixText, generalSevenText, generalEightText, generalNineText, generalTenText;

            //Robot Texts
            Text robotText, highGoalAccuracyText, speedText, weightText, bestSuitedRoleText, defenseCapText, antidefenseCapText, cakeSkillText, compOfDrivingText, robotNineText, robotTenText;

            //Autonomous Texts 
            Text autonomousText, autoCapText, startPosText, autonomousThreeText, autonomousFourText, autonomousFiveText;

            //End game Texts 
            Text endGameText, climbPercText, touchpadPercText, endGameThreeText, endGameFourText, endGameFiveText;

            //Likelihoods Texts
            Text likelihoodText, penaltyLikeText, breakdownLikeText, stuckLikeText, likelihoodsFourText, likelihoodsFiveText;



        // Use this for initialization
        void Start()
        {
            manager = GetComponentInParent<UIManager>();
            Debug.Log("Starting TeamDataPanel: SimpleTeam: " + JsonUtility.ToJson(simpleTeam));
            SetData();
            backButton = GetComponentsInChildren<Button>()[0];
            backButton.onClick.AddListener(() => { manager.CreatePanelWrapper(manager.sPrevPanel); });
        }

        // Update is called once per frame
        void Update()
        {
            if(team.sTeamName != simpleTeam.sTeamName && simpleTeam != null)
            {
                Debug.Log("Starting pull coroutine");
                StartCoroutine(PullTeamFromServer());
            }
        }

        IEnumerator PullTeamFromServer()
        {
            WWW pullFromServer = new WWW(UIManager.sGetTeamURL + simpleTeam.iTeamNumber + "/" + simpleTeam.iTeamNumber + ".json");
            Debug.Log("Pulling Team data from " + UIManager.sGetTeamURL + simpleTeam.iTeamNumber + "/" + simpleTeam.iTeamNumber + ".json");
            yield return pullFromServer;
            team = JsonUtility.FromJson<Team>(pullFromServer.text);
            SetData();
            yield break;
        }
        void SetData()
        {
            Debug.Log("Setting Data: " + JsonUtility.ToJson(team));
            Text[] textArray = GetComponentsInChildren<Text>();
            //back button functionality
            //backButton = GetComponentInChildren<Button>();
            //backButton.onClick.AddListener(() => { manager.CreatePanelWrapper("analyticsPanel"); });
            int spot = 0;
            teamNameNumberText = textArray[spot]; spot++;
            leftButtonText = textArray[spot]; spot++;
            rightButtonText = textArray[spot]; spot++;
            gamesScoutedText = textArray[spot]; spot++;
            winPercentageText = textArray[spot]; spot++;


            //general------------------------------------------------------------------------------------
            generalText = textArray[spot]; spot++;
            highGoalsPerGameText = textArray[spot]; spot++;
            lowGoalsPerGameText = textArray[spot]; spot++;
            gearsPerGameText = textArray[spot]; spot++;
            pointContText = textArray[spot]; spot++;
            rpPerGameText = textArray[spot]; spot++;
            generalSixText = textArray[spot]; spot++;
            generalSevenText = textArray[spot]; spot++;
            generalEightText = textArray[spot]; spot++;
            generalNineText = textArray[spot]; spot++;
            generalTenText = textArray[spot]; spot++;

            //robot
            robotText = textArray[spot]; spot++;
            highGoalAccuracyText = textArray[spot]; spot++;
            speedText = textArray[spot]; spot++;
            weightText = textArray[spot]; spot++;
            bestSuitedRoleText = textArray[spot]; spot++;
            defenseCapText = textArray[spot]; spot++;
            antidefenseCapText = textArray[spot]; spot++;
            cakeSkillText = textArray[spot]; spot++;
            compOfDrivingText = textArray[spot]; spot++;
            robotNineText = textArray[spot]; spot++;
            robotTenText = textArray[spot]; spot++;

            //Autonomous
            autonomousText = textArray[spot]; spot++;
            autoCapText = textArray[spot]; spot++;
            startPosText = textArray[spot]; spot++;
            autonomousThreeText = textArray[spot]; spot++;
            autonomousFourText = textArray[spot]; spot++;
            autonomousFiveText = textArray[spot]; spot++;

            //End game
            endGameText = textArray[spot]; spot++;
            climbPercText = textArray[spot]; spot++;
            touchpadPercText = textArray[spot]; spot++;
            endGameThreeText = textArray[spot]; spot++;
            endGameFourText = textArray[spot]; spot++;
            endGameFiveText = textArray[spot]; spot++;

            //Likelihood
            likelihoodText = textArray[spot]; spot++;
            penaltyLikeText = textArray[spot]; spot++;
            breakdownLikeText = textArray[spot]; spot++;
            stuckLikeText = textArray[spot]; spot++;
            likelihoodsFourText = textArray[spot]; spot++;
            likelihoodsFiveText = textArray[spot]; spot++;



            //personalizing data to specific team

            teamNameNumberText.text = "" + team.iTeamNumber + " " + team.sTeamName;  //team.teamNameNumberText

            Image robotImage = GetComponentInChildren<Image>();
            //robotImage = team.robotImage;
            robotImage.preserveAspect = true;

            int gamesScouted = team.iGamesScouted; //team.gamesScouted;
            gamesScoutedText.text = "Games Scouted: " + gamesScouted;

            float winPercentage = team.fWinPercentage; //team.winPercentage;
            winPercentageText.text = "Win Percentage: " + (winPercentage * 100) + "%";


            //GENERAL==========================================================================
            generalText.text = "Averages (Per Match )";

            float highGoals = team.fAvgHighFuelPerMatch; //team.highGoalsPerGame
            highGoalsPerGameText.text = "High Goals: " + highGoals;

            float lowGoals = team.fAvgLowFuelPerMatch; //team.lowGoalsPerGame
            lowGoalsPerGameText.text = "Low Goals: " + lowGoals;


            float gears = team.fAvgGearsPerMatch; //team.gearsPerGame
            gearsPerGameText.text = "Gears: " + gears;

            double pointCont = 50;
            pointContText.text = "Point Contribution: " + pointCont;

            float rpPerGame = team.fAvgRankingPoints;
            rpPerGameText.text = "Ranking Points: " + rpPerGame;

            generalSixText.text = "";
            generalSevenText.text = "";
            generalEightText.text = "";
            generalNineText.text = "";
            generalTenText.text = "";

            //ROBOT==========================================================================
            robotText.text = "Robot Statistics";

            float highGoalAccuracy = team.fHighGoalAccuracy; //team.highGoalAccuracy
            highGoalAccuracyText.text = "High Goal Accuracy: " + highGoalAccuracy + "%";

            int speed = team.iSpeed; //team.speed
            speedText.text = "Speed: " + speed;

            int weight = team.iWeight; //team.weight
            weightText.text = "Weight: " + weight;

            string bestSuitedRole = team.sBestRole; //team.bestSuitedRole
            bestSuitedRoleText.text = "Best Role: " + bestSuitedRole;

            int defenseCap = 2; //team.defenseCap
            defenseCapText.text = "Defense Capability: " + defenseCap;

            int antidefenseCap = 3; //team.antidefenseCap
            antidefenseCapText.text = "Antidefense Capability: " + antidefenseCap;

            int cakeSkill = 0;
            cakeSkillText.text = "Cake ability: " + cakeSkill;

            int competencyOfDriving = 5;
            compOfDrivingText.text = "Competency of Driving: " + competencyOfDriving;

            robotNineText.text = "";
            robotTenText.text = "";

            //AUTONOMOUS==========================================================================
            autonomousText.text = "Autonomous";

            string autoCap = "score";
            autoCapText.text = "Capabilities: " + autoCap;

            int startPos = team.iStartingPosition;
            startPosText.text = "Starting pos: " + startPos;

            autonomousThreeText.text = "";
            autonomousFourText.text = "";
            autonomousFiveText.text = "";

            //END GAME==========================================================================
            endGameText.text = "End Game";

            float climbPerc = team.fClimbAttemptPercent;
            climbPercText.text = "Climb: " + (climbPerc * 100) + "%";

            float touchpadPerc = team.fTouchpadPercent;
            touchpadPercText.text = "TouchPad: " + (touchpadPerc * 100) + "%";

            endGameThreeText.text = "";
            endGameFourText.text = "";
            endGameFiveText.text = "";

            //LIKELIHOOD==========================================================================
            likelihoodText.text = "Likelihoods";

            float penaltyLike = team.fPenaltyLikelihood;
            penaltyLikeText.text = "Penalty Chance: " + (penaltyLike * 100) + "%";

            float breakdownLike = team.fBreakdownLikelihood;
            breakdownLikeText.text = "Breakdown Chance: " + (breakdownLike * 100) + "%";

            float stuckLike = team.fStuckLikelihood;
            stuckLikeText.text = "Stuck: " + (stuckLike * 100) + "%";

            likelihoodsFourText.text = "";
            likelihoodsFiveText.text = "";
        }
    }
}