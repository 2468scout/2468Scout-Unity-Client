using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScoreScoutPanel_ContentManager : MonoBehaviour
    {
        Time matchStartTime;
        Match currentlyScoutingMatch;
        bool bColor, bMatchStarted;
        UIManager manager;
        Button backButton, menuButton, matchStartButton, increase1Button, increase5Button, increase40Button, increase50Button, increase60Button;
        Text timeRemainingText, backButtonText;
        ScoreScout currentScoreScout;
        // Use this for initialization
        void Start()
        {
            bMatchStarted = false;
            Button[] buttonArray = GetComponentsInChildren<Button>();
            backButtonText = GetComponentsInChildren<Text>()[0];
            timeRemainingText = GetComponentsInChildren<Text>()[2];
            backButton = buttonArray[0];
            menuButton = buttonArray[1];
            matchStartButton = buttonArray[2];
            increase1Button = buttonArray[3];
            increase5Button = buttonArray[4];
            increase40Button = buttonArray[5];
            increase50Button = buttonArray[6];
            increase60Button = buttonArray[7];
            increase1Button.onClick.AddListener(() => { this.NewTimeEvent(1); });
            increase5Button.onClick.AddListener(() => { this.NewTimeEvent(5); });
            increase40Button.onClick.AddListener(() => { this.NewTimeEvent(40); });
            increase50Button.onClick.AddListener(() => { this.NewTimeEvent(50); });
            increase60Button.onClick.AddListener(() => { this.NewTimeEvent(60); });

            matchStartButton.onClick.AddListener(() => { this.StartMatch(); });
            backButton.onClick.AddListener(() => { this.BackButton(); });
            this.manager = GetComponentInParent<UIManager>();
            if (currentScoreScout.bColor)
            {
                currentScoreScout = currentlyScoutingMatch.blueScoreScout;
            }
            else
            {
                currentScoreScout = currentlyScoutingMatch.redScoreScout;
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (matchStartTime != null)
            {
                Time t = GetCurrentTime();
                t.TimeSince(matchStartTime);
                timeRemainingText.text = t.TimeSince(matchStartTime).ToString();
            }
            if (matchStartTime != null && bMatchStarted)
            {
                backButtonText.text = "Match End";
            }
            else if (bMatchStarted)
            {
                backButtonText.text = "Save Match";
            }
        }
        public void StartMatch()
        {
            bMatchStarted = true;
            matchStartTime = GetCurrentTime();
            matchStartButton.gameObject.SetActive(false);
            Debug.Log(JsonUtility.ToJson(matchStartTime));
        }
        public void BackButton()
        {
            if(matchStartTime != null && bMatchStarted)
            {
                backButtonText.text = "Match End";
            }
            else if (bMatchStarted)
            {
                backButtonText.text = "Save Match";
            }
            else
            {
                manager.BackPanel();
            }
        }

        Time GetCurrentTime()
        {
            return new Time(System.DateTime.Now);
        }
        void NewTimeEvent(int i)
        {
            switch (i)
            {
                case 1:
                    currentScoreScout.increase1TimeList.Add(GetCurrentTime().sumMilliseconds());
                    break;
                case 5:
                    currentScoreScout.increase5TimeList.Add(GetCurrentTime().sumMilliseconds());
                    break;
                case 40:
                    currentScoreScout.increase40TimeList.Add(GetCurrentTime().sumMilliseconds());
                    break;
                case 50:
                    currentScoreScout.increase50TimeList.Add(GetCurrentTime().sumMilliseconds());
                    break;
                case 60:
                    currentScoreScout.increase60TimeList.Add(GetCurrentTime().sumMilliseconds());
                    break;
            }
        }
    }
}