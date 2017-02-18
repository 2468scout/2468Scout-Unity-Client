using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System.IO;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        public bool bHasTeamPitScoutsToSend, bHasTeamMatchesToSend, bHasImagesToSend, bHasScoreScoutsToSend, bIsInDebugMode;
        public GameObject mainPanel, matchScoutPanel, pointEventButtonPanel, pitScoutPanel, analyticsPanel, loginPanel, teamPanel, openPanel, scoreScoutPanel;
        public string sUserName, sEventCode, sPrevEventCode, sPrevUserName, sPrevPanel, sCurrentPanel, sEventDownloadStatus, sPrevDownloadStatus;
        public List<TeamMatch> teamMatchListToScout;
        public List<string> listTeamMatchFilePaths, listTeamPitScoutFilePaths, listScoreScoutFilePaths, listImageFilePaths;
        public string sMainURL = "localhost:8080";
        public string sGetEventURL, sGetTeamURL;
        bool hasStarted = false;
        public FRCEvent currentEvent;
        public List<ScheduleItem> scheduleItemList;
        public List<TeamMatch> teamMatchesToScout = new List<TeamMatch>();
        public List<TeamPitScout> teamPitScoutsToScout = new List<TeamPitScout>();
        public List<ScoreScout> scoreScoutsToScout = new List<ScoreScout>();
        public int iNumInSchedule, iNumInTeamPitScouts;
        Text eventStatusText;
        // Use this for initialization
        void Start()
        {
            sMainURL = "localhost:8080";
            sGetEventURL = sMainURL + "/Events/";
            sGetTeamURL = sMainURL + "/Teams/";
            sEventDownloadStatus = "No Event specified, please login";
            if(sCurrentPanel == "mainPanel")
            {
                eventStatusText = GetComponentsInChildren<Text>()[5];
            }
            Screen.fullScreen = false;
            currentEvent = new FRCEvent();
            eventStatusText = GameObject.Find("EventStatusText").GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            if(sCurrentPanel == "mainPanel" && sPrevDownloadStatus != sEventDownloadStatus)
            {
                eventStatusText.text = sEventDownloadStatus;
            }
            if(sPrevPanel != null)
            {
                Input.backButtonLeavesApp = false;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    BackPanel();
                }
            }
            else
            {
                Input.backButtonLeavesApp = true;
            }
            if(!hasStarted)
            {
                StartCoroutine(ChangePanel("mainPanel"));
                sCurrentPanel = "mainPanel";
                hasStarted = true;
            }
            if(sPrevEventCode != sEventCode)
            {
                StartCoroutine(DownloadEvent());
                sPrevEventCode = sEventCode;
            }
            if(sPrevEventCode != sEventCode || sPrevUserName != sUserName)
            {
                sPrevUserName = sUserName;
                List<int> teamMatchPositions = new List<int>();
                if(currentEvent != null)
                {
                    if(currentEvent.teamMatchList != null)
                    {
                        for (int i = 0; i < currentEvent.teamMatchList.Count; i++)
                        {
                            string s = currentEvent.teamMatchList[i].sPersonScouting;
                            if (s == sUserName)
                            {
                                teamMatchPositions.Add(i);
                            }
                        }
                        for (int i = 0; i < teamMatchPositions.Count; i++)
                        {
                            teamMatchListToScout.Add(currentEvent.teamMatchList[teamMatchPositions[i]]);
                        }
                    }
                }
            }
        }
        public IEnumerator DownloadEvent ()
        {
            Debug.Log("Downloading Event from " + sGetEventURL + sEventCode + ".json");
            sEventDownloadStatus = "Downloading event " + sEventCode;
            WWW download = new WWW(sGetEventURL + sEventCode + ".json");
            yield return download;
            Debug.Log(download.text);
            currentEvent = JsonUtility.FromJson<FRCEvent>(download.text);
            if(currentEvent.sEventCode == null || currentEvent.sEventCode == "")
            {
                sEventDownloadStatus = "Failed to download " + sEventCode;
            }
            else
            {
                sEventDownloadStatus = "Successfully loaded " + sEventCode;
            }
            foreach (ScheduleItem s in currentEvent.scheduleItemList)
            {
                scheduleItemList.Add(s);
                if(s.sPersonResponsible == sUserName)
                {
                    switch (s.sItemType)
                    {
                        case "matchScout":
                            teamMatchesToScout.Add(new TeamMatch(s.iTeamNumber, s.iMatchNumber, s.bColor, sEventCode));
                            break;
                        case "scoreScout":
                            scoreScoutsToScout.Add(new ScoreScout(s.bColor, s.iMatchNumber, s.sEventCode));
                            break;
                    }
                }
            }
            if(currentEvent.teamPitScoutList != null)
            {
                foreach (TeamPitScout t in currentEvent.teamPitScoutList)
                {
                    if (t.iSpeed == 0 && t.sPersonResponsible == sUserName)
                    {
                        teamPitScoutsToScout.Add(t);
                    }
                }
            }
            yield break;
        }

        public void CreatePanelWrapper(string panel)
        {
            StartCoroutine(ChangePanel(panel));
        }

        public void BackPanel()
        {
            Debug.Log("BackPanel:" + sCurrentPanel);
            if (sCurrentPanel == "matchScoutPanel")
            {
                StartCoroutine(ChangePanel("mainPanel"));
            }
            else if (sCurrentPanel == "scoreScoutPanel" )
            {
                StartCoroutine(ChangePanel("mainPanel"));
            }
            else if (sCurrentPanel == "pitScoutPanel")
            {
                StartCoroutine(ChangePanel("mainPanel"));
            }
            else if (sCurrentPanel == "analyticsPanel" )
            {
                StartCoroutine(ChangePanel("mainPanel"));
            }
            else if (sCurrentPanel.Contains("teamPanel") )
            {
                StartCoroutine(ChangePanel("analyticsPanel"));
            }
            else if (sCurrentPanel == "loginPanel")
            {
                StartCoroutine(ChangePanel("mainPanel"));
            }
        }

        public IEnumerator ChangePanel(string panel)
        {
            GameObject tempPanel = null;
            RectTransform rectTransform = null;
            if (panel == "matchScoutPanel")
            {
                Debug.Log(sPrevPanel + "," + sCurrentPanel);
                if(scheduleItemList.Count == 0)
                {

                }
                else
                {
                    sPrevPanel = sCurrentPanel;
                    sCurrentPanel = panel;
                    switch (scheduleItemList[iNumInSchedule].sItemType)
                    {
                        case "matchScout":
                            tempPanel = Instantiate(matchScoutPanel);
                            break;
                        case "scoreScout":
                            tempPanel = Instantiate(scoreScoutPanel);
                            break;
                    }
                    rectTransform = tempPanel.GetComponent<RectTransform>();
                    Destroy(openPanel);
                    openPanel = tempPanel;
                    openPanel.transform.SetParent(gameObject.transform);
                    rectTransform.offsetMin = new Vector2(0, 0);
                    rectTransform.offsetMax = new Vector2(0, 0);
                    eventStatusText = null;
                    sPrevDownloadStatus = "";
                }
            }
            else if (panel == "pitScoutPanel")
            {
                sPrevPanel = sCurrentPanel;
                sCurrentPanel = panel;
                Debug.Log(sPrevPanel + "," + sCurrentPanel);
                Destroy(openPanel);
                tempPanel = Instantiate(pitScoutPanel);
                rectTransform = tempPanel.GetComponent<RectTransform>();
                openPanel = tempPanel;
                openPanel.transform.SetParent(gameObject.transform);
                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);
                openPanel.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => { BackPanel(); });
                eventStatusText = null;
                sPrevDownloadStatus = "";
            }
            else if (panel == "analyticsPanel")
            {
                sPrevPanel = sCurrentPanel;
                sCurrentPanel = panel;
                Debug.Log(sPrevPanel + "," + sCurrentPanel);
                Destroy(openPanel);
                tempPanel = Instantiate(analyticsPanel);
                rectTransform = tempPanel.GetComponent<RectTransform>();
                openPanel = tempPanel;
                openPanel.transform.SetParent(gameObject.transform);
                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);
                openPanel.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => { BackPanel(); });
                eventStatusText = null;
                sPrevDownloadStatus = "";
            }
            else if (panel == "mainPanel")
            {
                sPrevPanel = sCurrentPanel;
                sCurrentPanel = panel;
                Debug.Log(sPrevPanel + "," + sCurrentPanel);
                Destroy(openPanel);
                tempPanel = Instantiate(mainPanel);
                rectTransform = tempPanel.GetComponent<RectTransform>();
                openPanel = tempPanel;
                openPanel.transform.SetParent(gameObject.transform);
                openPanel.GetComponentInChildren<Toggle>().onValueChanged.AddListener((value) => { SwitchDebug(value); });
                openPanel.GetComponentInChildren<Toggle>().isOn = bIsInDebugMode;
                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);
                openPanel.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => { StartCoroutine(ChangePanel("pitScoutPanel")); });
                openPanel.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => { StartCoroutine(ChangePanel("matchScoutPanel")); });
                openPanel.GetComponentsInChildren<Button>()[2].onClick.AddListener(() => { StartCoroutine(ChangePanel("analyticsPanel")); });
                openPanel.GetComponentsInChildren<Button>()[3].onClick.AddListener(() => { StartCoroutine(ChangePanel("loginPanel")); });
                eventStatusText = GameObject.Find("EventStatusText").GetComponent<Text>();
            }
            else if (panel == "loginPanel")
            {
                sPrevPanel = sCurrentPanel;
                sCurrentPanel = panel;
                Debug.Log(sPrevPanel + "," + sCurrentPanel);
                tempPanel = Instantiate(loginPanel);
                rectTransform = tempPanel.GetComponent<RectTransform>();
                Destroy(openPanel);
                openPanel = tempPanel;
                openPanel.transform.SetParent(gameObject.transform);
                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);
                //openPanel.GetComponentInChildren<Button>().onClick.AddListener(() => { StartCoroutine()})
                eventStatusText = null;
                sPrevDownloadStatus = "";
            }
            else if (panel.Contains("teamPanel:"))
            {
                sPrevPanel = sCurrentPanel;
                sCurrentPanel = panel;
                tempPanel = Instantiate(teamPanel);
                rectTransform = tempPanel.GetComponent<RectTransform>();
                Destroy(openPanel);
                openPanel = tempPanel;
                openPanel.transform.SetParent(gameObject.transform);
                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);
                openPanel.GetComponent<TeamDataPanelManager>().simpleTeam = JsonUtility.FromJson<SimpleTeam>(panel.Substring(10));
                eventStatusText = null;
                sPrevDownloadStatus = "";
            }
            /*
            else if (panel == )
            {

            }
            */
            yield break;
        }

        public IEnumerator SendData()
        {
            if (bHasTeamPitScoutsToSend)
            {
                foreach (string s in listTeamMatchFilePaths)
                {
                    WWW upload = new WWW(sMainURL + "/postPit", File.ReadAllBytes(s));
                    yield return upload;
                    if (!string.IsNullOrEmpty(upload.error))
                    {
                        print("Error uploading: " + upload.error);
                    }
                    else if (upload.text == "Error")
                    {
                        print("Unknown Upload Error");
                    }
                    else if (upload.text == "Success")
                    {
                        print("Success!");
                    }
                }
            }
            if (bHasImagesToSend)
            {
                foreach (string s in listImageFilePaths)
                {

                }
            }
            if (bHasTeamMatchesToSend)
            {
                foreach (string s in listTeamMatchFilePaths)
                {
                    WWW upload = new WWW(sMainURL + "/postTeamMatch", File.ReadAllBytes(s));
                    yield return upload;
                    if (!string.IsNullOrEmpty(upload.error))
                    {
                        print("Error uploading: " + upload.error);
                    }
                    else if (upload.text == "Error")
                    {
                        print("Unknown Upload Error");
                    }
                    else if (upload.text == "Success")
                    {
                        print("Success!");
                    }
                }
            }
            if (bHasScoreScoutsToSend)
            {
                foreach (string s in listTeamMatchFilePaths)
                {
                    WWW upload = new WWW(sMainURL + "/postMatchScores", File.ReadAllBytes(s));
                    yield return upload;
                    if (!string.IsNullOrEmpty(upload.error))
                    {
                        print("Error uploading: " + upload.error);
                    }
                    else if (upload.text == "Error")
                    {
                        print("Unknown Upload Error");
                    }
                    else if (upload.text == "Success")
                    {
                        print("Success!");
                    }
                }
            }
            
        }

        public void SwitchDebug(bool b)
        {
            bIsInDebugMode = b;
            if (bIsInDebugMode)
            {
                sMainURL = "localhost:8080";
            }
            else
            {
                sMainURL = "http://scouting.westaaustin.org";
            }
            sGetEventURL = sMainURL + "/Events/";
            sGetTeamURL = sMainURL + "/Teams/";
        }
    }
}