using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TeamDataPanelManager : MonoBehaviour
    {
        UIManager manager;
        Team team;
        Text teamNameNumberText, backButtonText;
        Button backButton;
        // Use this for initialization
        void Start()
        {
            manager = GetComponentInParent<UIManager>();
            Text[] textArray = GetComponentsInChildren<Text>();
            teamNameNumberText = textArray[0];
            teamNameNumberText.text = "lol nerd";
            backButtonText = textArray[1];
            backButton = GetComponentInChildren<Button>();
            backButtonText.text = "Back";
            backButton.onClick.AddListener(() => { manager.ChangePanel("analyticsPanel"); });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}