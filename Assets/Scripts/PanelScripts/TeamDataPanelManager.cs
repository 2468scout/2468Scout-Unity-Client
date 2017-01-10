using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TeamDataPanelManager : MonoBehaviour
    {
        Team team;
        Text teamNameNumberText;
        // Use this for initialization
        void Start()
        {
            Text[] textArray = GetComponentsInChildren<Text>();
            teamNameNumberText = textArray[0];
            teamNameNumberText.text = "lol nerd";
            /*
             * team name
             * team number
             * picture
             * statistics?
             * 
             */
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}