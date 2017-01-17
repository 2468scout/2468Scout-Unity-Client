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
        int gamesScouted, winPercentage;
        Image robotImage;

        // Use this for initialization
        void Start()
        {
            manager = GetComponentInParent<UIManager>();
            Text[] textArray = GetComponentsInChildren<Text>();
            Debug.Log(textArray);
            backButton = GetComponentInChildren<Button>();
            /*
            //back button functionality
            backButton.onClick.AddListener(() => { manager.ChangePanel("analyticsPanel"); });
            */

            //team name and number at the top of the page
            teamNameNumberText = textArray[0];
            teamNameNumberText.text = "2468 Team Appreciate";   //team.teamNameNumberText

            //team's robot image
            robotImage = GetComponentInChildren<Image>();
            //robotImage = team.robotImage;

            
            //# of matches scouted regarding the certain team
            gamesScoutedText = textArray[1];
            //gamesScouted = team.gamesScouted;
            gamesScouted = 5;
            gamesScoutedText.text = "Games Scouted: " + gamesScouted;

            //% of matches won
            winPercentageText = textArray[2];
            //winPercentage = team.winPercentage;
            winPercentage = 76;
            winPercentageText.text = "Win Percentage: " + winPercentage + "%";


            /*
            ---general has 6 elements
            High goals per game
            Low goals per game
            Gears per game
            Ranking Points per game
            Point contribution per game
            Point contribution per time
            
            ---robot has 6 elements
            Speed
            Weight
            Defense capability
            Anti-defense capability
            Best suited role
            Cake skill

            ---autonomous has 2 elements
            Autonomous capabilities
            Specific starting position

            ---end game has 2
            Climb %
            Touchpad %


            ---likelihood has 3
            Penalty likelihood
            Breakdown likelihood
            “Stuck” likelihood
            
            Text highGoalsPerGameText, lowGoalsPerGameText, gearsPerGameText, pointContribution, rpPerGame, speed, weight,
                bestSuitedRole, defenseCapability, pointContPerTime, cakeSkill, auto, startingPos, climbPerc, touchpadPerc, penaltyLike, breakdownLike, stuckLike;
            Text[] text = new Text[19];
            text = [teamNameNumberText, gamesScoutedText, winPercentageText, highGoalsPerGameText, lowGoalsPerGameText, gearsPerGameText, pointContribution, rpPerGame, speed, weight,
                bestSuitedRole, defenseCapability, pointContPerTime, cakeSkill, auto, startingPos, climbPerc, touchpadPerc, penaltyLike, breakdownLike, stuckLike]
            */
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}