using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

namespace Assets.Scripts{
    public class MatchScoutPanel_ContentManager : MonoBehaviour
    {
        GameObject fieldImage;
        RectTransform fieldImageRectTransform;
        float aspectRatio;
        Time matchStartTime;
        public TeamMatch currentlyScoutingTeamMatch;
        Button backButton, matchStartButton, menuButton, fieldImageButton, stopEventButton, leftCountIncreaseButton, leftCountDecreaseButton, rightCountIncreaseButton, rightCountDecreaseButton;
        UIManager manager;
        Text timeRemainingText, stopEventButtonText, teamNumberText, colorStationNumberText, matchNumberText;
        Toggle autonomousToggle;
        int iLeftCount, iRightCount;
        string sLeftCountCode, sRightCountCode, sTeamMatchURL, sMatchStatus;
        // Use this for initialization
        void Start()
        {
            sMatchStatus = "Match Unstarted";
            manager = GetComponentInParent<UIManager>();
            sTeamMatchURL = manager.sMainURL + "/postTeamMatch";
            switch (manager.scheduleItemList[manager.iNumInSchedule].sItemType)
            {
                case "matchScout":
                    currentlyScoutingTeamMatch = new TeamMatch(manager.scheduleItemList[manager.iNumInSchedule]);
                    break;
                case "scoreScout":
                    break;
            }
            fieldImage = GameObject.Find("FieldImage");
            fieldImageRectTransform = fieldImage.GetComponent<RectTransform>();
            fieldImageRectTransform.offsetMin = new Vector2(0, -(GameObject.Find("FieldPanel").GetComponent<RectTransform>().rect.width /2048) * 784 / 2);
            fieldImageRectTransform.offsetMax = new Vector2(0, (GameObject.Find("FieldPanel").GetComponent<RectTransform>().rect.width / 2048) * 784 / 2);
            Debug.Log("FieldPanel width:" + GameObject.Find("FieldPanel").GetComponent<RectTransform>().rect.width + ", FieldPanel height: " + GameObject.Find("FieldPanel").GetComponent<RectTransform>().rect.height + ", calculated height:"  + ((GameObject.Find("FieldPanel").GetComponent<RectTransform>().rect.width / 2048) * 784));
            Button[] buttonArray = GetComponentsInChildren<Button>();
            backButton = buttonArray[0];
            menuButton = buttonArray[1];
            matchStartButton = buttonArray[2];
            fieldImageButton = buttonArray[3];
            stopEventButton = buttonArray[4];
            leftCountIncreaseButton = buttonArray[5];
            leftCountDecreaseButton = buttonArray[6];
            rightCountIncreaseButton = buttonArray[7];
            rightCountDecreaseButton = buttonArray[8];
            matchStartButton.onClick.AddListener(() => { this.StartMatch(); });
            fieldImageButton.onClick.AddListener(() => { this.CreatePointEvent(UnityEngine.Input.mousePosition); });

            Text[] textArray = GetComponentsInChildren<Text>();
            timeRemainingText = textArray[2];
            stopEventButtonText = textArray[7];

            stopEventButton.gameObject.SetActive(false);
            leftCountIncreaseButton.gameObject.SetActive(false);
            leftCountDecreaseButton.gameObject.SetActive(false);
            rightCountIncreaseButton.gameObject.SetActive(false);
            rightCountDecreaseButton.gameObject.SetActive(false);
            autonomousToggle = GetComponentInChildren<Toggle>();

            teamNumberText = GetComponentsInChildren<Text>()[4];
            colorStationNumberText = GetComponentsInChildren<Text>()[5];
            matchNumberText = GetComponentsInChildren<Text>()[6];

            teamNumberText.text = "Team #" + currentlyScoutingTeamMatch.iTeamNumber;
            matchNumberText.text = "Match #" + currentlyScoutingTeamMatch.iMatchNumber;

            if (currentlyScoutingTeamMatch.bColor)
            {

                colorStationNumberText.text = "Blue " + currentlyScoutingTeamMatch.iStationNumber;
            } else
            {
                colorStationNumberText.text = "Red " + currentlyScoutingTeamMatch.iStationNumber;
            }
            backButton.onClick.AddListener(() => { BackButton(); });
        }

        // Update is called once per frame
        void Update()
        {
            if(matchStartTime != null && sMatchStatus == "Match Active")
            {
                Time t = GetCurrentTime();
                t.TimeSince(matchStartTime);
                timeRemainingText.text = t.TimeSince(matchStartTime).ToString();
                if (t.TimeSince(matchStartTime).ToString() == "0:16")
                {
                    autonomousToggle.isOn = false;
                }
                else if (t.TimeSince(matchStartTime).ToString() == "1:40")
                {
                    BackButton();
                }
            }
        }
        public void SaveTeamMatch()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + currentlyScoutingTeamMatch.sFileName + ".json");
            manager.listScoreScoutFilePaths.Add(Application.persistentDataPath + currentlyScoutingTeamMatch.sFileName + ".json");
            bf.Serialize(file, currentlyScoutingTeamMatch);
            file.Dispose();
            manager.bHasTeamMatchesToSend = true;
        }

        public void CreatePointEvent(Vector2 mousePosition)
        {
            Vector2 adjustedMousePosition = new Vector2(mousePosition.x, mousePosition.y - (((gameObject.GetComponent<RectTransform>().rect.height - GameObject.Find("ToolbarPanel").GetComponent<RectTransform>().rect.height) - fieldImageRectTransform.rect.height) / 2));
            Time timeInMatch = GetCurrentTime();
            timeInMatch = timeInMatch.TimeSince(matchStartTime);
            GameObject createdPointMatchPanel = Instantiate(manager.pointEventButtonPanel);
            MatchEvent newEvent = new MatchEvent(timeInMatch, autonomousToggle.isOn, new Point(adjustedMousePosition.x / fieldImageRectTransform.rect.width, adjustedMousePosition.y / fieldImageRectTransform.rect.height));
            createdPointMatchPanel.GetComponent<RectTransform>().localPosition = mousePosition;
            createdPointMatchPanel.transform.SetParent(this.transform);
            createdPointMatchPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 5, Screen.width / 5);
            Debug.Log(JsonUtility.ToJson(newEvent));
            createdPointMatchPanel.GetComponent<PointEventButtonPanel>().currentEvent = newEvent;
            currentlyScoutingTeamMatch.matchEventList.Add(newEvent);
        }
        
        public void StartMatch()
        {
            matchStartTime = GetCurrentTime();
            matchStartButton.gameObject.SetActive(false);
            Debug.Log(JsonUtility.ToJson(matchStartTime));
            backButton.GetComponentInChildren<Text>().text = "Stop Match";
            sMatchStatus = "Match Active";
        }

        Time GetCurrentTime()
        {
            return new Time(System.DateTime.Now);
        }

        public void MatchEventStart(MatchEvent matchEvent, string s)
        {
            stopEventButton.gameObject.SetActive(true);
            switch (s)
            {
                case "HIGH_GOAL_STOP":
                    stopEventButtonText.text = "High goal stopped";
                    if((matchEvent.loc.x <= .5 || rightCountIncreaseButton.gameObject.activeSelf) && !leftCountIncreaseButton.gameObject.activeSelf)
                    {
                        leftCountIncreaseButton.gameObject.SetActive(true);
                        leftCountIncreaseButton.onClick.AddListener(() => this.AddToLeftCount(1));
                        sLeftCountCode = "HIGH_GOAL_MISS";
                    }
                    else if((matchEvent.loc.x > .5 || leftCountIncreaseButton.gameObject.activeSelf) && !rightCountIncreaseButton.gameObject.activeSelf)
                    {
                        rightCountIncreaseButton.gameObject.SetActive(true);
                        rightCountIncreaseButton.onClick.AddListener(() => this.AddToRightCount(1));
                        sRightCountCode = "HIGH_GOAL_MISS";
                    }
                    break;
                case "LOW_GOAL_STOP":
                    stopEventButtonText.text = "Low goal stopped";
                    
                    break;
            }
            stopEventButton.onClick.AddListener( () => { this.GenerateStopMatchEvent(s); });
        }

        public void GenerateStopMatchEvent(string s)
        {
            Time timeInMatch = GetCurrentTime();
            timeInMatch = timeInMatch.TimeSince(matchStartTime);
            MatchEvent matchEvent = new MatchEvent(timeInMatch, autonomousToggle.isOn);
            matchEvent.sEventName = s;
            stopEventButton.gameObject.SetActive(false);
            currentlyScoutingTeamMatch.matchEventList.Add(matchEvent);
            if (s == "HIGH_GOAL_STOP")
            {
                if(sLeftCountCode == "HIGH_GOAL_MISS")
                {
                    leftCountIncreaseButton.gameObject.SetActive(false);
                    matchEvent = new MatchEvent(timeInMatch, autonomousToggle.isOn, iLeftCount, sLeftCountCode);
                    currentlyScoutingTeamMatch.matchEventList.Add(matchEvent);
                }
                else if(sRightCountCode == "HIGH_GOAL_MISS")
                {
                    rightCountIncreaseButton.gameObject.SetActive(false);
                    matchEvent = new MatchEvent(timeInMatch, autonomousToggle.isOn, iRightCount, sRightCountCode);
                    currentlyScoutingTeamMatch.matchEventList.Add(matchEvent);
                }
            }
        }
        
        public void AddToLeftCount(int val)
        {
            iLeftCount += val;
        }
        
        public void AddToRightCount(int val)
        {
            iRightCount += val;
        }

        public void BackButton()
        {
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
                    SaveTeamMatch();
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
            /*
            if (matchStartTime != null)
            {

            }
            else
            {
                manager.BackPanel();
            }
            */
        }
    }
}