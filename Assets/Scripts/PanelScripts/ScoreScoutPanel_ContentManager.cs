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
        bool bColor;
        UIManager manager;
        Button backButton, menuButton, matchStartButton, increase1Button, increase5Button, increase40Button, increase50Button, increase60Button;
        Text timeRemainingText;
        List<Time> increase1TimeList, increase5TimeList, increase40TimeList, increase50TimeList, increase60TimeList;
        // Use this for initialization
        void Start()
        {
            Button[] buttonArray = GetComponentsInChildren<Button>();
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
            this.manager = GetComponentInParent<UIManager>();
            if (bColor)
            {
                this.increase1TimeList = currentlyScoutingMatch.blueIncrease1TimeList;
                this.increase5TimeList = currentlyScoutingMatch.blueIncrease5TimeList;
                this.increase40TimeList = currentlyScoutingMatch.blueIncrease40TimeList;
                this.increase50TimeList = currentlyScoutingMatch.blueIncrease50TimeList;
                this.increase60TimeList = currentlyScoutingMatch.blueIncrease60TimeList;
            }
            else
            {
                this.increase1TimeList = currentlyScoutingMatch.redIncrease1TimeList;
                this.increase5TimeList = currentlyScoutingMatch.redIncrease5TimeList;
                this.increase40TimeList = currentlyScoutingMatch.redIncrease40TimeList;
                this.increase50TimeList = currentlyScoutingMatch.redIncrease50TimeList;
                this.increase60TimeList = currentlyScoutingMatch.redIncrease60TimeList;
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
        }
        public void StartMatch()
        {
            matchStartTime = GetCurrentTime();
            matchStartButton.gameObject.SetActive(false);
            Debug.Log(JsonUtility.ToJson(matchStartTime));
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
                    increase1TimeList.Add(GetCurrentTime());
                    break;
                case 5:
                    increase5TimeList.Add(GetCurrentTime());
                    break;
                case 40:
                    increase40TimeList.Add(GetCurrentTime());
                    break;
                case 50:
                    increase50TimeList.Add(GetCurrentTime());
                    break;
                case 60:
                    increase60TimeList.Add(GetCurrentTime());
                    break;
            }
        }
    }
}