using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System.IO;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        private CameraClearFlags storedClearFlags;
        private int storedCullingMask;
        public bool bHasTeamPitScoutsToSend, bHasTeamMatchesToSend, bHasImagesToSend, bHasScoreScoutsToSend, bIsInDebugMode, bIsSendingData;
        public GameObject mainPanel, matchScoutPanel, pointEventButtonPanel, pitScoutPanel, analyticsPanel, loginPanel, teamPanel, openPanel, 
            scoreScoutPanel, prevMatchDataPanel, comingMatchDataPanel;
        public string sUserName, sEventCode, sPrevEventCode, sPrevUserName, sPrevPanel, sCurrentPanel, sEventDownloadStatus, sPrevDownloadStatus;
        public List<TeamMatch> teamMatchListToScout;
        public List<string> listTeamMatchFilePaths, listTeamPitScoutFilePaths, listScoreScoutFilePaths, listImageFilePaths;
        public string sMainURL;
        public string sGetEventURL, sGetTeamURL;
        bool hasStarted = false;
        public FRCEvent currentEvent;
        public List<ScheduleItem> scheduleItemList;
        public List<TeamMatch> teamMatchesToScout = new List<TeamMatch>();
        public List<TeamPitScout> teamPitScoutsToScout = new List<TeamPitScout>();
        public List<ScoreScout> scoreScoutsToScout = new List<ScoreScout>();
        public int iNumInSchedule, iNumInTeamPitScouts;
        public Button uploadDataButton;
        Text eventStatusText;
        // Use this for initialization
        void Start()
        {
            Application.targetFrameRate = 15;
            //save clear flags
            storedClearFlags = Camera.main.clearFlags;

            //save the culling mask
            storedCullingMask = Camera.main.cullingMask;

            sMainURL = "http://scouting.westaaustin.org";
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
            iNumInTeamPitScouts = 0;
            StopRender();
        }

        // Update is called once per frame
        void Update()
        {
            if(sCurrentPanel == "mainPanel" && sPrevDownloadStatus != sEventDownloadStatus)
            {
                eventStatusText.text = sEventDownloadStatus;
                //RenderOnce();
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

            if(!bIsSendingData && (bHasImagesToSend || bHasScoreScoutsToSend || bHasTeamMatchesToSend || bHasTeamPitScoutsToSend))
            {
                //Debug.Log("Sending Data");
                StartCoroutine(SendData());
            }
            /*
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
            */
        }
        public IEnumerator DownloadEvent ()
        {
            
            Debug.Log("Downloading Event from " + sGetEventURL + sEventCode + ".json");
            sEventDownloadStatus = "Downloading event " + sEventCode;
            WWW download = new WWW(sGetEventURL + sEventCode + ".json");
            yield return download;
            Debug.Log(download.text);
            currentEvent = JsonUtility.FromJson<FRCEvent>(download.text);
            if (currentEvent == null || currentEvent.sEventCode == "")
            {
                sEventDownloadStatus = "Failed to download " + sEventCode;
            }
            else
            {
                sEventDownloadStatus = "Successfully loaded " + sEventCode;
            }
            RenderOnce();
            foreach (ScheduleItem s in currentEvent.scheduleItemList)
            {
                if(s.sPersonResponsible == sUserName)
                {
                    Debug.Log("I am responsible for a match!");
                    scheduleItemList.Add(s);/*
                    switch (s.sItemType)
                    {
                        case "matchScout":
                            teamMatchesToScout.Add(new TeamMatch(s.iTeamNumber, s.iMatchNumber, s.bColor, sEventCode));
                            break;
                        case "scoreScout":
                            scoreScoutsToScout.Add(new ScoreScout(s.bColor, s.iMatchNumber, s.sEventCode));
                            break;
                    }
                    */
                }
            }
            if(currentEvent.teamPitScoutList != null)
            {
                Debug.Log("TeamPitScoutList isn't null!");
                foreach (TeamPitScout t in currentEvent.teamPitScoutList)
                {
                    Debug.Log(t.sPersonResponsible + " : " + sUserName);
                    if (t.sPersonResponsible == sUserName)
                    {
                        teamPitScoutsToScout.Add(t);
                    }
                }
            }
            else
            {
                Debug.Log("TeamPitScoutList is null!");
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
            StartRender();
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
                    DestroyImmediate(openPanel);
                    switch (scheduleItemList[iNumInSchedule].sItemType)
                    {
                        case "matchScout":
                            tempPanel = Instantiate(matchScoutPanel);
                            Debug.Log("Instantiated a new matchScout panel!");
                            break;
                        case "scoreScout":
                            tempPanel = Instantiate(scoreScoutPanel);
                            Debug.Log("Instantiated a new scoreScout panel!");
                            break;
                    }
                    openPanel = tempPanel;
                    rectTransform = openPanel.GetComponent<RectTransform>();
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
                DestroyImmediate(openPanel);
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
                DestroyImmediate(openPanel);
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
                uploadDataButton = GetComponentsInChildren< Button >()[4];
                uploadDataButton.onClick.AddListener(() => { StartCoroutine(SendData()); });
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
            else if (panel.Contains("comingMatchPanel:"))
            {
                sPrevPanel = sCurrentPanel;
                sCurrentPanel = panel;
                tempPanel = Instantiate(comingMatchDataPanel);
                rectTransform = tempPanel.GetComponent<RectTransform>();
                Destroy(openPanel);
                openPanel = tempPanel;
                openPanel.transform.SetParent(gameObject.transform);
                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);
                SimpleMatch sm = JsonUtility.FromJson<SimpleMatch>(panel.Substring(17));
                WWW downloadPreMatch = new WWW(sMainURL + "/Matches/" + sm.sEventPlayedAtCode + "_" + sm.sCompetitionLevel + "_" + sm.iMatchNumber + ".json");
                yield return downloadPreMatch;
                openPanel.GetComponent<UpcomingMatchPanel_ContentManager>().preMatch = JsonUtility.FromJson<PreMatch>(downloadPreMatch.text);
                eventStatusText = null;
                sPrevDownloadStatus = "";
            }
            StopRender();
            yield break;
        }

        public IEnumerator SendData()
        {
            if (!bIsSendingData)
            {
                bIsSendingData = true;
                Debug.Log("Sending data!");
                listTeamPitScoutFilePaths = new List<string>(Directory.GetFiles(Application.persistentDataPath, "pit_team*.json"));
                listTeamMatchFilePaths = new List<string>(Directory.GetFiles(Application.persistentDataPath, "team*.json"));
                listScoreScoutFilePaths = new List<string>(Directory.GetFiles(Application.persistentDataPath, "score_match*.json"));
                if (bHasTeamPitScoutsToSend)
                {
                    foreach (string s in listTeamPitScoutFilePaths)
                    {
                        WWW upload = new WWW(sMainURL + "/postPit", File.ReadAllBytes(s));
                        Debug.Log("Uploading " + s + "to " + sMainURL + "/postPit");
                        yield return upload;
                        if (!string.IsNullOrEmpty(upload.error))
                        {
                            Debug.Log("Error uploading: " + upload.error);
                        }
                        else if (upload.text == "Error")
                        {
                            Debug.Log("Unknown Upload Error");
                        }
                        else if (upload.text == "Success")
                        {
                            Debug.Log("Success!");
                        }
                        WWW testHasData = new WWW(sMainURL + "/getFileExistence?FILEPATH=" + "TeamMatches/" + s.Substring(s.IndexOf("/t")));
                        Debug.Log("Looking for server data: " + "TeamMatches/" + s.Substring(s.IndexOf("/t")));
                        yield return testHasData;
                        Debug.Log("Server Has Data: " + testHasData.text);
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
                    Debug.Log("Attempting to upload TeamMatches at: " + listTeamMatchFilePaths[0]);
                    bool bFailedToUpload = false;
                    foreach (string s in listTeamMatchFilePaths)
                    {
                        WWW upload = new WWW(sMainURL + "/postTeamMatch", File.ReadAllBytes(s));
                        Debug.Log("Uploading " + s + "to " + sMainURL + "/postTeamMatch");
                        yield return upload;
                        if (!string.IsNullOrEmpty(upload.error))
                        {
                            Debug.Log("Error uploading: " + upload.error);
                        }
                        else if (upload.text == "Error")
                        {
                            Debug.Log("Unknown Upload Error");
                        }
                        else if (upload.text == "Success")
                        {
                            Debug.Log("Success!");
                        }
                        WWW testHasData = new WWW(sMainURL + "/getFileExistence?FILEPATH=" + "TeamMatches" + s.Substring(s.IndexOf("/t")));
                        Debug.Log("Looking for server data: " + "TeamMatches" + s.Substring(s.IndexOf("/t")));
                        yield return testHasData;
                        Debug.Log("Server Has Data: " + testHasData.text);
                        bFailedToUpload = !(testHasData.text == "true");
                    }
                    bHasTeamMatchesToSend = bFailedToUpload;
                }
                if (bHasScoreScoutsToSend)
                {
                    bool bFailedToUpload = false;
                    foreach (string s in listTeamMatchFilePaths)
                    {
                        WWW upload = new WWW(sMainURL + "/postMatchScores", File.ReadAllBytes(s));
                        Debug.Log("Uploading " + s + "to " + sMainURL + "/postMatchScores");
                        yield return upload;
                        if (!string.IsNullOrEmpty(upload.error))
                        {
                            Debug.Log("Error uploading: " + upload.error);
                        }
                        else if (upload.text == "Error")
                        {
                            Debug.Log("Unknown Upload Error");
                        }
                        else if (upload.text == "Success")
                        {
                            Debug.Log("Success!");
                        }

                        WWW testHasData = new WWW(sMainURL + "/getFileExistence?FILEPATH=" + s);
                        yield return testHasData;
                        Debug.Log("Server Has Data: " + testHasData.text);
                        bFailedToUpload = (testHasData.text == "false");
                    }
                    bHasScoreScoutsToSend = bFailedToUpload;
                }
                bIsSendingData = false;
            }
        }

        public void SwitchDebug(bool b)
        {
            bIsInDebugMode = b;
            if (bIsInDebugMode)
            {
                sMainURL = "10.107.10.14:8080";
            }
            else
            {
                sMainURL = "http://scouting.westaaustin.org";
            }
            sGetEventURL = sMainURL + "/Events/";
            sGetTeamURL = sMainURL + "/Teams/";
        }
        void StartRender()
        {
            Debug.Log("Rendering Started!");
            //first change the clear flags to what is stored
            Camera.main.clearFlags = storedClearFlags;

            //now change the culling mask to what is stored
            Camera.main.cullingMask = storedCullingMask;
        }
        void StopRender()
        {
            Debug.Log("Rendering Stopped!");
            //first change the clear flags to nothing
            Camera.main.clearFlags = CameraClearFlags.Nothing;

            //now change the culling mask to nothing
            Camera.main.cullingMask = 0;
        }
        void RenderOnce()
        {
            Debug.Log("Rendering Once!");
            StartRender();
            Camera.main.Render();
            StopRender();
        }
    }
}