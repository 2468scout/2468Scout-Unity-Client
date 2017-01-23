using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TeamDataPanelManager : MonoBehaviour
    {
        UIManager manager;
        public Team team;              //use this when pulling statistics
        Button backButton;

        Text teamNameNumberText, gamesScoutedText, winPercentageText, backButtonText;
        
        
            //General Texts
            Text generalText, highGoalsPerGameText, lowGoalsPerGameText, gearsPerGameText, pointContText, rpPerGameText;

            //Robot Texts
            Text robotText, highGoalAccuracyText, speedText, weightText, bestSuitedRoleText, defenseCapText, antidefenseCapText, cakeSkillText;

            //Autonomous Texts 
            Text autonomousText, autoCapText, startPosText;

            //End game Texts 
            Text endGameText, climbPercText, touchpadPercText;

            //Likelihoods Texts
            Text likelihoodText, penaltyLikeText, breakdownLikeText, stuckLikeText;
       
        

        // Use this for initialization
        void Start()
        {
            manager = GetComponentInParent<UIManager>();
            Text[] textArray = GetComponentsInChildren<Text>();

            //back button functionality
            //backButton = GetComponentInChildren<Button>();
            //backButton.onClick.AddListener(() => { manager.CreatePanelWrapper("analyticsPanel"); });
            int spot = 0;
            teamNameNumberText      =       textArray[spot];       spot++;
            gamesScoutedText        =       textArray[spot];       spot++;
            winPercentageText       =       textArray[spot];       spot++;

            
            //general
            generalText             =       textArray[spot];       spot++;
            highGoalsPerGameText    =       textArray[spot];       spot++;
            lowGoalsPerGameText     =       textArray[spot];       spot++;
            gearsPerGameText        =       textArray[spot];       spot++;
            pointContText           =       textArray[spot];       spot++;
            rpPerGameText           =       textArray[spot];       spot++;
            /*
            //robot
            robotText               =       textArray[spot];       spot++;
            highGoalAccuracyText    =       textArray[spot];       spot++;
            speedText               =       textArray[spot];       spot++;
            weightText              =       textArray[spot];       spot++;
            bestSuitedRoleText      =       textArray[spot];       spot++;
            defenseCapText          =       textArray[spot];       spot++;
            antidefenseCapText      =       textArray[spot];       spot++;
            cakeSkillText           =       textArray[spot];       spot++;

            //Autonomous
            autonomousText          =       textArray[spot];       spot++;
            autoCapText             =       textArray[spot];       spot++;
            startPosText            =       textArray[spot];       spot++;

            //End game
            endGameText             =       textArray[spot];       spot++;
            climbPercText           =       textArray[spot];       spot++;
            touchpadPercText        =       textArray[spot];       spot++;

            //Likelihood
            likelihoodText          =       textArray[spot];       spot++;
            penaltyLikeText         =       textArray[spot];       spot++;
            breakdownLikeText       =       textArray[spot];       spot++;
            stuckLikeText           =       textArray[spot];       spot++;
            */
            

            //personalizing data to specific team

            teamNameNumberText.text = "2468 Team Appreciate";  //team.teamNameNumberText
            
            Image robotImage = GetComponentInChildren<Image>();
            //robotImage = team.robotImage;
            robotImage.preserveAspect = true;
           
            int gamesScouted = 10; //team.gamesScouted;
            gamesScoutedText.text = "Games Scouted: " + gamesScouted;
            
            double winPercentage = 95; //team.winPercentage;
            winPercentageText.text = "Win Percentage: " + winPercentage + "%";

            
            //GENERAL==========================================================================
            generalText.text = "GENERAL (Per Match Averages)";

            double highGoals = 30; //team.highGoalsPerGame
            highGoalsPerGameText.text = "High Goals: " + highGoals;

            double lowGoals = 10; //team.lowGoalsPerGame
            lowGoalsPerGameText.text = "Low Goals: " + lowGoals;

            
            double gears = 12; //team.gearsPerGame
            gearsPerGameText.text = "Gears: " + gears;
            
            double pointCont = 50;
            pointContText.text = "Point Contribution: " + pointCont;

            double rpPerGame = 1.5;
            rpPerGameText.text = "Ranking Points: " + rpPerGame;
            
            
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}