using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {

        public GameObject mainPanel, matchScoutPanel, pointEventButtonPanel, pitScoutPanel, analyticsPanel, loginPanel, teamPanel, openPanel;
        public string sUserName, sEventCode, sPrevEventCode, sPrevUserName, sPrevPanel, sCurrentPanel;
        public List<TeamMatch> teamMatchListToScout;
        public static readonly string sMainURL = "localhost";
        public static readonly string sGetEventURL = sMainURL + ":8080/Events/";
        public static readonly string sGetTeamURL = sMainURL + ":8080/Teams/";
        bool hasStarted = false;
        FRCEvent currentEvent;
        // Use this for initialization
        void Start()
        {
            Screen.fullScreen = false;
            currentEvent = new FRCEvent();
        }

        // Update is called once per frame
        void Update()
        {
            if(sPrevPanel != null)
            {
                Input.backButtonLeavesApp = false;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    StartCoroutine(ChangePanel(sPrevPanel));
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
                DownloadEvent();
                sPrevEventCode = sEventCode;
            }
            if(sPrevEventCode != sEventCode || sPrevUserName != sUserName)
            {
                List<int> teamMatchPositions = new List<int>();
                if(currentEvent != null)
                {
                    for (int i = 0; i < currentEvent.teamMatchList.Count; i++)
                    {
                        string s = currentEvent.teamMatchList[i].sPersonScouting;
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
        }
        public IEnumerator DownloadEvent ()
        {
            WWW download = new WWW(sGetEventURL + sEventCode + ".json");
            yield return download;
            currentEvent = JsonUtility.FromJson<FRCEvent>(download.text);
        }

        public void CreatePanelWrapper(string panel)
        {
            StartCoroutine(ChangePanel(panel));
        }

        public IEnumerator ChangePanel(string panel)
        {
            GameObject tempPanel = null;
            RectTransform rectTransform = null;
            if (panel == "matchScoutPanel")
            {
                sPrevPanel = sCurrentPanel;
                sCurrentPanel = panel;
                Debug.Log(sPrevPanel + "," + sCurrentPanel);
                tempPanel = Instantiate(matchScoutPanel);
                rectTransform = tempPanel.GetComponent<RectTransform>();
                Destroy(openPanel);
                openPanel = tempPanel;
                openPanel.transform.SetParent(gameObject.transform);
                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);
                openPanel.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => { StartCoroutine(ChangePanel(sPrevPanel)); });

            }
            else if (panel == "pitScoutPanel")
            {
                sPrevPanel = sCurrentPanel;
                sCurrentPanel = panel;
                Debug.Log(sPrevPanel + "," + sCurrentPanel);
                tempPanel = Instantiate(pitScoutPanel);
                rectTransform = tempPanel.GetComponent<RectTransform>();
                Destroy(openPanel);
                openPanel = tempPanel;
                openPanel.transform.SetParent(gameObject.transform);
                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);
                openPanel.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => { StartCoroutine(ChangePanel(sPrevPanel)); });
            }
            else if (panel == "analyticsPanel")
            {
                sPrevPanel = sCurrentPanel;
                sCurrentPanel = panel;
                Debug.Log(sPrevPanel + "," + sCurrentPanel);
                tempPanel = Instantiate(analyticsPanel);
                rectTransform = tempPanel.GetComponent<RectTransform>();
                Destroy(openPanel);
                openPanel = tempPanel;
                openPanel.transform.SetParent(gameObject.transform);
                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);
                openPanel.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => { StartCoroutine(ChangePanel(sPrevPanel)); });
            }
            else if (panel == "mainPanel")
            {
                sPrevPanel = sCurrentPanel;
                sCurrentPanel = panel;
                Debug.Log(sPrevPanel + "," + sCurrentPanel);
                tempPanel = Instantiate(mainPanel);
                rectTransform = tempPanel.GetComponent<RectTransform>();
                Destroy(openPanel);
                openPanel = tempPanel;
                openPanel.transform.SetParent(gameObject.transform);
                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);
                openPanel.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => { StartCoroutine(ChangePanel("pitScoutPanel")); });
                openPanel.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => { StartCoroutine(ChangePanel("matchScoutPanel")); });
                openPanel.GetComponentsInChildren<Button>()[2].onClick.AddListener(() => { StartCoroutine(ChangePanel("analyticsPanel")); });
                openPanel.GetComponentsInChildren<Button>()[3].onClick.AddListener(() => { StartCoroutine(ChangePanel("loginPanel")); });
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
            }
            /*
            else if (panel == )
            {

            }
            */
            yield break;
        }
    }
}