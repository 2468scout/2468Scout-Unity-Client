using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class AnalyticsPanel_ContentManager : MonoBehaviour
    {
        string objectType;
        public GameObject selectableTeamPanel, selectableMatchPanel;
        public GameObject content;
        public GameObject scrollview;
        public List<SimpleTeam> simpleTeamList = new List<SimpleTeam>();
        public List<SimpleTeam> displayedTeamList = new List<SimpleTeam>();
        public List<SimpleMatch> simpleMatchList;
        List<GameObject> teamPanelList;
        Text searchText;
        UIManager manager;
        public bool bRefreshing = false;
        private int iRefreshTimer;
        private Vector2 size;
        int iTeamDataPanelHeight = (Screen.height / 5);
        // Use this for initialization
        void Start()
        {
            searchText = GetComponentsInChildren<Text>()[2];
            Debug.Log("iTeamDataPanelHeight: " + iTeamDataPanelHeight);
            manager = GetComponentInParent<UIManager>();
            content = GameObject.Find("Content");
            if(content != null)
            {
                Debug.Log("Content Found!");
            }
            else
            {
                Debug.Log("Content not found");
            }
            simpleTeamList.AddRange(manager.currentEvent.simpleTeamList);
            content.GetComponentsInChildren<Image>()[0].enabled = false;
            scrollview = GameObject.Find("Scroll View");
            displayedTeamList.AddRange(simpleTeamList);
            Debug.Log("Currently found teams in manager: " + manager.currentEvent.simpleTeamList.Count);
            Debug.Log("Currently found teams in simpleTeamList: " + simpleTeamList.Count);
            Debug.Log("Currently found teams in displayedTeamList: " + displayedTeamList.Count);
            RefreshDisplayedTeams();
        }
        // Update is called once per frame
        void Update()
        {
            if(searchText.text != null && searchText.text != "")
            {
                displayedTeamList = new List<SimpleTeam>();
                foreach(SimpleTeam t in simpleTeamList)
                {
                    if(t.iTeamNumber.ToString().Contains(searchText.text) || t.sTeamName.Contains(searchText.text))
                    {
                        displayedTeamList.Add(t);
                    }
                }
                RefreshDisplayedTeams();
            }
            if(displayedTeamList.Count < simpleTeamList.Count && (searchText.text == null || searchText.text == ""))
            {
                Debug.Log("Searching Stopped");
                displayedTeamList = new List<SimpleTeam>();
                displayedTeamList.AddRange(simpleTeamList);
                RefreshDisplayedTeams();
            }
            if ((content != null && content.GetComponent<RectTransform>().offsetMax.y < -115) && bRefreshing == false)
            {
                StartCoroutine(Refresh());
                size = content.GetComponent<RectTransform>().sizeDelta;
            }
            if (bRefreshing == true)
            {
                content.GetComponent<RectTransform>().offsetMin = new Vector2(0, (-iTeamDataPanelHeight * simpleTeamList.Count) - iTeamDataPanelHeight);
                iRefreshTimer++;
                if (content.GetComponent<RectTransform>().offsetMax.y > -115)
                {
                    content.GetComponent<RectTransform>().offsetMax = new Vector2(0, -115);
                }
                if (content.GetComponent<RectTransform>().offsetMax.y < -115)
                {
                    scrollview.GetComponent<ScrollRect>().movementType = ScrollRect.MovementType.Elastic;
                }
                else
                {
                    scrollview.GetComponent<ScrollRect>().movementType = ScrollRect.MovementType.Unrestricted;
                }
                /*
                if (getAnalysisData.isDone && iRefreshTimer >= 100)
                {
                    bRefreshing = false;
                    content.GetComponentsInChildren<Image>()[0].enabled = false;
                    scrollview.GetComponent<ScrollRect>().movementType = ScrollRect.MovementType.Elastic;
                    scrollview.GetComponent<ScrollRect>().inertia = true;
                    iRefreshTimer = 0;
                    content.GetComponent<RectTransform>().sizeDelta = size;
                    StopCoroutine(Refresh());
                }
                */
            }
        }

        public IEnumerator Refresh()
        {
            content.GetComponentsInChildren<Image>()[0].enabled = true;
            bRefreshing = true;
            scrollview.GetComponent<ScrollRect>().movementType = ScrollRect.MovementType.Unrestricted;
            scrollview.GetComponent<ScrollRect>().inertia = false;
            GameObject.Find("Scrollbar Vertical").GetComponent<Scrollbar>().interactable = false;
            yield return manager.DownloadEvent();
            GameObject.Find("Scrollbar Vertical").GetComponent<Scrollbar>().interactable = true;
            content.GetComponentsInChildren<Image>()[0].enabled = false;
            scrollview.GetComponent<ScrollRect>().movementType = ScrollRect.MovementType.Elastic;
            scrollview.GetComponent<ScrollRect>().inertia = true;
            iRefreshTimer = 0;
            content.GetComponent<RectTransform>().sizeDelta = size;
            content.GetComponent<RectTransform>().offsetMax = new Vector2(0, 20);
            bRefreshing = false;
            yield break;
        }

        public void RefreshDisplayedTeams()
        {
            foreach (GameObject panel in teamPanelList)
            {
                Destroy(panel);
            }
            foreach (SimpleTeam s in displayedTeamList)
            {
                int i = displayedTeamList.IndexOf(s);
                GameObject tempPanel = Instantiate(selectableTeamPanel);
                tempPanel.GetComponentInChildren<SelectableTeamPanelManager>().iNumInList = i;
                tempPanel.GetComponentInChildren<SelectableTeamPanelManager>().containedTeam = s;
                tempPanel.GetComponent<RectTransform>().offsetMax = new Vector2(0, -(iTeamDataPanelHeight * i));
                tempPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, -(iTeamDataPanelHeight * (i + 1)));
                tempPanel.transform.SetParent(content.transform);
                tempPanel.GetComponent<Button>().onClick.AddListener(() => manager.CreatePanelWrapper("teamPanel:" + JsonUtility.ToJson(s)));
                content.GetComponent<RectTransform>().offsetMin = new Vector2(0, (-iTeamDataPanelHeight * i) - iTeamDataPanelHeight);
                teamPanelList.Add(tempPanel);
            }
        }
    }
}
