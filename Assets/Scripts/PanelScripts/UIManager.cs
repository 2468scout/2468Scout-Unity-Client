using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {

        public GameObject mainPanel, matchScoutPanel, pitScoutPanel, analyticsPanel, loginPanel, teamPanel, openPanel;
        public string sUserName, sEventCode, sPrevEventCode, sPrevUserName;
        public List<TeamMatch> teamMatchListToScout;
        string sGetEventURL = "196.168.56.1:8080/events/";
        FRCEvent currentEvent;
        // Use this for initialization
        void Start()
        {
            currentEvent = new FRCEvent();
        }

        // Update is called once per frame
        void Update()
        {
            if(sPrevEventCode != sEventCode)
            {
                DownloadEvent();
                sPrevEventCode = sEventCode;
            }
            if(sPrevEventCode != sEventCode || sPrevUserName != sUserName)
            {
                List<int> teamMatchPositions = new List<int>();
                for (int i = 0; i < currentEvent.listNamesByTeamMatch.Count; i++)
                {
                    string s = currentEvent.listNamesByTeamMatch[i];
                    if (s == sUserName)
                    {
                        teamMatchPositions.Add(i);
                    }
                }
                for(int i = 0; i < teamMatchPositions.Count; i++)
                {
                    teamMatchListToScout.Add(currentEvent.teamMatchList[teamMatchPositions[i]]);
                }
            }
        }
        public IEnumerator DownloadEvent ()
        {
            WWW download = new WWW(sGetEventURL + sEventCode + ".json");
            yield return download;
            currentEvent = JsonUtility.FromJson<FRCEvent>(download.text);
        }

        public void ChangePanel(string panel)
        {
            GameObject tempPanel = null;
            RectTransform rectTransform = null;
            switch (panel)
            {
                case "matchScoutPanel":
                    tempPanel = Instantiate(matchScoutPanel);
                    rectTransform = tempPanel.GetComponent<RectTransform>();
                    Destroy(openPanel);
                    openPanel = tempPanel;
                    openPanel.transform.parent = gameObject.transform;
                    rectTransform.offsetMin = new Vector2(0, 0);
                    rectTransform.offsetMax = new Vector2(0, 0);
                    openPanel.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => { this.ChangePanel("mainPanel"); });
                    break;
                case "pitScoutPanel":
                    tempPanel = Instantiate(pitScoutPanel);
                    rectTransform = tempPanel.GetComponent<RectTransform>();
                    Destroy(openPanel);
                    openPanel = tempPanel;
                    openPanel.transform.parent = gameObject.transform;
                    rectTransform.offsetMin = new Vector2(0, 0);
                    rectTransform.offsetMax = new Vector2(0, 0);
                    openPanel.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => { this.ChangePanel("mainPanel"); });
                    break;
                case "analyticsPanel":
                    tempPanel = Instantiate(analyticsPanel);
                    rectTransform = tempPanel.GetComponent<RectTransform>();
                    Destroy(openPanel);
                    openPanel = tempPanel;
                    openPanel.transform.parent = gameObject.transform;
                    rectTransform.offsetMin = new Vector2(0, 0);
                    rectTransform.offsetMax = new Vector2(0, 0);
                    openPanel.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => { this.ChangePanel("mainPanel"); });
                    break;
                case "mainPanel":
                    tempPanel = Instantiate(mainPanel);
                    rectTransform = tempPanel.GetComponent<RectTransform>();
                    Destroy(openPanel);
                    openPanel = tempPanel;
                    openPanel.transform.parent = gameObject.transform;
                    rectTransform.offsetMin = new Vector2(0, 0);
                    rectTransform.offsetMax = new Vector2(0, 0);
                    openPanel.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => { this.ChangePanel("pitScoutPanel"); });
                    openPanel.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => { this.ChangePanel("matchScoutPanel"); });
                    openPanel.GetComponentsInChildren<Button>()[2].onClick.AddListener(() => { this.ChangePanel("analyticsPanel"); });
                    openPanel.GetComponentsInChildren<Button>()[3].onClick.AddListener(() => { this.ChangePanel("loginPanel"); });
                    break;
                case "loginPanel":
                    tempPanel = Instantiate(loginPanel);
                    rectTransform = tempPanel.GetComponent<RectTransform>();
                    Destroy(openPanel);
                    openPanel = tempPanel;
                    openPanel.transform.parent = gameObject.transform;
                    rectTransform.offsetMin = new Vector2(0, 0);
                    rectTransform.offsetMax = new Vector2(0, 0);
                    break;
            }
        }
    }
}