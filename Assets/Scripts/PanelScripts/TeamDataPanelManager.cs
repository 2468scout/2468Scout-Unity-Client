using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TeamDataPanelManager : MonoBehaviour
    {
        UIManager manager;
        Team team;              //use this when pulling statistics
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
            backButtonText = textArray[0]; //textArray.Length - 1
            backButtonText.text = "Back";
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
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}