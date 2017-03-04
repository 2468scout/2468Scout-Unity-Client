using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using System.Text;

namespace Assets.Scripts
{
    public class ScoreScoutPanel_ContentManager : MonoBehaviour
    {
        Time matchStartTime;
        Match currentlyScoutingMatch;
        bool bColor, bMatchStarted;
        UIManager manager;
        Button backButton, matchStartButton, increase1Button, increase5Button, increase40Button, increase50Button, increase60Button, nextMatchButton, prevMatchButton;
        Text timeRemainingText, backButtonText, matchNumberText, scoutingColorText;
        ScoreScout currentScoreScout;
        string sMatchStatus;
        // Use this for initialization
        void Start()
        {
            manager = GetComponentInParent<UIManager>();
            currentScoreScout = new ScoreScout(manager.scheduleItemList[manager.iNumInSchedule]);
            bMatchStarted = false;
            Button[] buttonArray = GetComponentsInChildren<Button>();
            backButtonText = GetComponentsInChildren<Text>()[0];
            timeRemainingText = GetComponentsInChildren<Text>()[2];
            backButton = buttonArray[0];
            prevMatchButton = buttonArray[1];
            matchStartButton = buttonArray[2];
            nextMatchButton = buttonArray[3];
            increase1Button = buttonArray[4];
            increase5Button = buttonArray[5];
            increase40Button = buttonArray[6];
            increase50Button = buttonArray[7];
            increase60Button = buttonArray[8];
            increase1Button.onClick.AddListener(() => { this.NewTimeEvent(1); });
            increase5Button.onClick.AddListener(() => { this.NewTimeEvent(5); });
            increase40Button.onClick.AddListener(() => { this.NewTimeEvent(40); });
            increase50Button.onClick.AddListener(() => { this.NewTimeEvent(50); });
            increase60Button.onClick.AddListener(() => { this.NewTimeEvent(60); });

            matchStartButton.onClick.AddListener(() => { this.StartMatch(); });
            backButton.onClick.AddListener(() => { this.BackButton(); });

            nextMatchButton.onClick.AddListener(() => { this.NextMatch(); });
            prevMatchButton.onClick.AddListener(() => { this.PrevMatch(); });
            /*
            if (currentScoreScout.bColor)
            {
                currentScoreScout = currentlyScoutingMatch.blueScoreScout;
            }
            else
            {
                currentScoreScout = currentlyScoutingMatch.redScoreScout;
            }
            */
            sMatchStatus = "Match Unstarted";

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
            backButtonText.text = "End Match";
            sMatchStatus = "Match Active";
        }
        public void BackButton()
        {
            /*
            if (matchStartTime != null && bMatchStarted)
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
            */
            switch (sMatchStatus)
            {
                case "Match Unstarted":
                    manager.BackPanel();
                    break;
                case "Match Active":
                    sMatchStatus = "Match Ended";
                    backButton.GetComponentInChildren<Text>().text = "Save Match";
                    break;
                case "Match Ended":
                    Save();
                    manager.iNumInSchedule++;
                    if (manager.iNumInSchedule == manager.scheduleItemList.Count)
                    {
                        manager.BackPanel();
                    }
                    else
                    {
                        StartCoroutine(manager.ChangePanel("matchScoutPanel"));
                    }
                    break;
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
        void Save()
        {
            FileStream file = null;
            if (currentScoreScout.bColor)
            {

                Debug.Log("Filename: " + Application.persistentDataPath + "score_match" + currentScoreScout.iMatchNumber + "_" + currentScoreScout.sEventCode + "_side" + "blue.json");
                file = File.Create(Application.persistentDataPath + "score_match" + currentScoreScout.iMatchNumber + "_" + currentScoreScout.sEventCode + "_side" + "blue.json");
                manager.listTeamMatchFilePaths.Add(Application.persistentDataPath + "score_match" + currentScoreScout.iMatchNumber + "_" + currentScoreScout.sEventCode + "_side" + "blue.json");
            }
            else
            {
                Debug.Log("Filename: " + Application.persistentDataPath + "score_match" + currentScoreScout.iMatchNumber + "_" + currentScoreScout.sEventCode + "_side" + "red.json");
                file = File.Create(Application.persistentDataPath + "score_match" + currentScoreScout.iMatchNumber + "_" + currentScoreScout.sEventCode + "_side" + "red.json");
                manager.listTeamMatchFilePaths.Add(Application.persistentDataPath + "score_match" + currentScoreScout.iMatchNumber + "_" + currentScoreScout.sEventCode + "_side" + "red.json");
            }

            file.Write(Encoding.ASCII.GetBytes(JsonUtility.ToJson(currentScoreScout)), 0, Encoding.ASCII.GetByteCount(JsonUtility.ToJson(currentScoreScout)));
            file.Dispose();
            manager.bHasTeamMatchesToSend = true;
        }

        void NextMatch()
        {
            if (manager.iNumInSchedule < manager.scheduleItemList.Capacity -1 )
            {
                manager.iNumInSchedule++;
                StartCoroutine(manager.ChangePanel("matchScoutPanel"));
            }
        }

        void PrevMatch()
        {
            if(manager.iNumInSchedule > 0)
            {
                manager.iNumInSchedule--;
                StartCoroutine(manager.ChangePanel("matchScoutPanel"));
            }
        }
    }
}