using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

namespace Assets.Scripts{
    public class MatchScoutPanel_ContentManager : MonoBehaviour
    {
        string teamMatchURL = "http://frc2468.org/matchUpload";
        GameObject fieldImage;
        RectTransform fieldImageRectTransform;
        float aspectRatio;
        Time matchStartTime;
        public TeamMatch currentlyScoutingTeamMatch;
        Button backButton, matchStartButton, menuButton, fieldImageButton, stopEventButton;
        UIManager manager;
        Text timeRemainingText, stopEventButtonText;
        Toggle autonomousToggle;
        // Use this for initialization
        void Start()
        {
            currentlyScoutingTeamMatch = new TeamMatch(2468, 1, false);
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
            manager = GetComponentInParent<UIManager>();
            matchStartButton.onClick.AddListener(() => { this.StartMatch(); });
            fieldImageButton.onClick.AddListener(() => { this.CreatePointEvent(UnityEngine.Input.mousePosition); });

            Text[] textArray = GetComponentsInChildren<Text>();
            timeRemainingText = textArray[2];
            stopEventButtonText = textArray[4];

            stopEventButton.gameObject.SetActive(false);
            autonomousToggle = GetComponentInChildren<Toggle>();
        }

        // Update is called once per frame
        void Update()
        {
            if(matchStartTime != null)
            {
                Time t = GetCurrentTime();
                t.TimeSince(matchStartTime);
                timeRemainingText.text = t.TimeSince(matchStartTime).ToString();
                if(t.TimeSince(matchStartTime).ToString() == "0:16")
                {
                    autonomousToggle.isOn = false;
                }
            }
        }
        public void SaveTeamMatch()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + currentlyScoutingTeamMatch.sFileName + ".json");
            bf.Serialize(file, null);
            file.Close();
        }
        public IEnumerator SendTeamMatch ()
        {
            SaveTeamMatch();
            WWWForm form = new WWWForm();
            form.AddField("obj", JsonUtility.ToJson(currentlyScoutingTeamMatch));
            WWW download = new WWW(teamMatchURL, form);
            yield return download;
            if (!string.IsNullOrEmpty(download.error))
            {
                print("Error uploading: " + download.error);
            }
            else if(download.text == "Error")
            {
                print("Unknown Upload Error");
            }
            else if(download.text == "Success")
            {
                print("Success!");
            }            
        }

        public void CreatePointEvent(Vector2 mousePosition)
        {
            Time timeInMatch = GetCurrentTime();
            timeInMatch = timeInMatch.TimeSince(matchStartTime);
            GameObject createdPointMatchPanel = Instantiate(manager.pointEventButtonPanel);
            MatchEvent newEvent = new MatchEvent(timeInMatch, autonomousToggle.isOn, new Point(mousePosition.x / fieldImageRectTransform.rect.width, mousePosition.y / fieldImageRectTransform.rect.height));
            createdPointMatchPanel.GetComponent<RectTransform>().localPosition = mousePosition;
            createdPointMatchPanel.transform.SetParent(this.transform);
            Debug.Log(JsonUtility.ToJson(newEvent));
            createdPointMatchPanel.GetComponent<PointEventButtonPanel>().currentEvent = newEvent;
            currentlyScoutingTeamMatch.matchEventList.Add(newEvent);
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

        public void EnterCount()
        {

        }

        public void MatchEventStart(MatchEvent matchEvent, string s)
        {
            stopEventButton.gameObject.SetActive(true);
            switch (s)
            {
                case "HIGH_GOAL_STOP":
                    stopEventButtonText.text = "High goal stopped";
                    break;
                case "LOW_GOAL_STOP":
                    stopEventButtonText.text = "Low goal stopped";
                    break;
            }
            stopEventButtonText.text = s;
            stopEventButton.onClick.AddListener( () => { this.GenerateStopMatchEvent(s); });
        }

        public void GenerateStopMatchEvent(string s)
        {
            Time timeInMatch = GetCurrentTime();
            timeInMatch = timeInMatch.TimeSince(matchStartTime);
            MatchEvent matchEvent = new MatchEvent(timeInMatch, autonomousToggle.isOn, null);
            matchEvent.sEventName = s;
            stopEventButton.gameObject.SetActive(false);
        }
    }
}