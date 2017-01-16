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
        Button backButton, matchStartButton, menuButton, fieldImageButton;
        UIManager manager;
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
            manager = GetComponentInParent<UIManager>();
            matchStartButton.onClick.AddListener(() => { this.StartMatch(); });
            fieldImageButton.onClick.AddListener(() => { this.CreatePointMatch(UnityEngine.Input.mousePosition); });
        }

        // Update is called once per frame
        void Update()
        {
            
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

        public void CreatePointMatch(Vector2 mousePosition)
        {
            Time timeInMatch = GetCurrentTime();
            timeInMatch.subtract(matchStartTime);
            GameObject createdPointMatchPanel = Instantiate(manager.pointEventButtonPanel);
            MatchEvent newEvent = new MatchEvent(timeInMatch, timeInMatch.minute == 0 && timeInMatch.second <= 15, new Point(mousePosition.x / fieldImageRectTransform.rect.width, mousePosition.y / fieldImageRectTransform.rect.height));
            createdPointMatchPanel.GetComponent<RectTransform>().localPosition = mousePosition;
            createdPointMatchPanel.transform.SetParent(this.transform);
            Debug.Log(JsonUtility.ToJson(newEvent));
            createdPointMatchPanel.GetComponent<PointEventButtonPanel>().currentEvent = newEvent;
            currentlyScoutingTeamMatch.matchEventList.Add(newEvent);
        }
        
        public void StartMatch()
        {
            matchStartTime = GetCurrentTime();
            Debug.Log(JsonUtility.ToJson(matchStartTime));
        }

        Time GetCurrentTime()
        {
            return new Time(System.DateTime.Now.Millisecond, System.DateTime.Now.Second, System.DateTime.Now.Minute);
        }
    }
}