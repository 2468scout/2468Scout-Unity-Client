using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class AnalyticsPanel_ContentManager : MonoBehaviour
    {
        string objectType, prevSearch, currentSearch;
        public GameObject selectableItemPanel;
        public GameObject content, scrollview, searchBar, analyticsTypeDropdown;
        public List<SimpleTeam> simpleTeamList = new List<SimpleTeam>();
        public List<SimpleTeam> displayedTeamList = new List<SimpleTeam>();
        public List<SimpleMatch> simpleMatchList = new List<SimpleMatch>();
        public List<SimpleMatch> displayedMatchList = new List<SimpleMatch>();
        List<GameObject> teamPanelList = new List<GameObject>();
        UIManager manager;
        public bool bRefreshing = false;
        public string sItemTypeDisplaying;
        private int iRefreshTimer;
        private Vector2 size;
        int iTeamDataPanelHeight = (Screen.height / 5);
        // Use this for initialization
        void Start()
        {
            analyticsTypeDropdown = GameObject.Find("AnalyticsTypeDropdown");
            if(analyticsTypeDropdown != null)
            {
                Debug.Log("Found analytics dropdown!");
            }
            else
            {
                Debug.Log("Missing analytics dropdown....");
            }
            sItemTypeDisplaying = "Team Analytics";
            searchBar = GameObject.Find("SearchBar");
            Debug.Log("iTeamDataPanelHeight: " + iTeamDataPanelHeight);
            manager = GetComponentInParent<UIManager>();
            content = GameObject.Find("Content");
            searchBar.GetComponent<InputField>().onValueChanged.AddListener((value) => { Search(value); });
            if(manager.currentEvent.simpleTeamList != null)
            {
                Debug.Log("Manager isn't null!");
                simpleTeamList.AddRange(manager.currentEvent.simpleTeamList);
            }
            if(manager.currentEvent.simpleMatchList != null)
            {
                simpleMatchList.AddRange(manager.currentEvent.simpleMatchList);
            }
            content.GetComponentsInChildren<Image>()[0].enabled = false;
            scrollview = GameObject.Find("Scroll View");
            if (content.GetComponent<RectTransform>().sizeDelta.y < 900)
            {
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, 900);
            }
            displayedTeamList.AddRange(simpleTeamList);
            displayedMatchList.AddRange(simpleMatchList);
            Debug.Log("Currently found teams in manager: " + manager.currentEvent.simpleTeamList.Count);
            Debug.Log("Currently found teams in simpleTeamList: " + simpleTeamList.Count);
            Debug.Log("Currently found teams in displayedTeamList: " + displayedTeamList.Count);

            Debug.Log("Currently found matches in manager: " + manager.currentEvent.simpleMatchList.Count);
            Debug.Log("Currently found matches in simpleMatchList: " + simpleMatchList.Count);
            Debug.Log("Currently found matches in displayedMatchList: " + displayedMatchList.Count);
            analyticsTypeDropdown.GetComponent<Dropdown>().onValueChanged.AddListener((val) => { ChangeDisplayedItem(); });
            analyticsTypeDropdown.GetComponent<Dropdown>().template.offsetMin = new Vector2(0, -(float)(Screen.height * 0.1));
            //analyticsTypeDropdown.GetComponentsInChildren<RectTransform>()[3].gameObject.SetActive(true);
            //analyticsTypeDropdown.GetComponentsInChildren<RectTransform>()[3].offsetMin = new Vector2(0, (float) (Screen.height * 0.1));
            //analyticsTypeDropdown.GetComponentsInChildren<RectTransform>()[6].offsetMin = new Vector2(0, (float)(Screen.height * 0.05));
            //analyticsTypeDropdown.GetComponentsInChildren<RectTransform>()[3].gameObject.SetActive(false);
            RefreshDisplayedItems();
        }
        // Update is called once per frame
        void Update()
        {
            /*
            if(searchText.text != null && searchText.text != "" && ((currentSearch == null && prevSearch == null) || currentSearch != prevSearch))
            {
                currentSearch = searchText.text;
                Debug.Log("Searching for: " + currentSearch);
                displayedTeamList = new List<SimpleTeam>();
                foreach(SimpleTeam t in simpleTeamList)
                {
                    if(t.iTeamNumber.ToString().Contains(searchText.text) || t.sTeamName.Contains(searchText.text))
                    {
                        Debug.Log("Adding to displayedTeamList");
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
            */
            if ((content != null && content.GetComponent<RectTransform>().offsetMax.y < -115) && bRefreshing == false)
            {
                StartCoroutine(Refresh());
                size = content.GetComponent<RectTransform>().sizeDelta;
            }
            if (bRefreshing == true)
            {
                content.GetComponent<RectTransform>().offsetMin = new Vector2(0, (-iTeamDataPanelHeight * simpleTeamList.Count) - iTeamDataPanelHeight);
                content.GetComponent<RectTransform>().sizeDelta = size;
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
            prevSearch = currentSearch;
        }
        
        public void Search(string currentSearch)
        {
            Debug.Log("Searching for: " + currentSearch);
            displayedTeamList = new List<SimpleTeam>();
            if(currentSearch == "" || currentSearch == null)
            {
                displayedTeamList.AddRange(simpleTeamList);
            }
            else
            {
                foreach (SimpleTeam t in simpleTeamList)
                {
                    if (t.iTeamNumber.ToString().Contains(currentSearch) || t.sTeamName.Contains(currentSearch))
                    {
                        Debug.Log("Adding to displayedTeamList");
                        displayedTeamList.Add(t);
                    }
                }
            }
            RefreshDisplayedItems();
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

        public void RefreshDisplayedItems()
        {
            foreach (GameObject panel in teamPanelList)
            {
                Destroy(panel);
            }
            switch (sItemTypeDisplaying)
            {
                case "Team Analytics":
                    foreach (SimpleTeam s in displayedTeamList)
                    {
                        Debug.Log("Displaying: " + s.iTeamNumber);
                        int i = displayedTeamList.IndexOf(s);
                        GameObject tempPanel = Instantiate(selectableItemPanel);
                        tempPanel.GetComponentInChildren<SelectableTeamPanelManager>().iNumInList = i;
                        tempPanel.GetComponentInChildren<SelectableTeamPanelManager>().containedTeam = s;
                        tempPanel.transform.SetParent(content.transform);
                        tempPanel.GetComponent<Button>().onClick.AddListener(() => manager.CreatePanelWrapper("teamPanel:" + JsonUtility.ToJson(s)));
                        content.GetComponent<RectTransform>().offsetMin = new Vector2(0, (-iTeamDataPanelHeight * i) - iTeamDataPanelHeight);
                        content.GetComponent<RectTransform>().offsetMax = new Vector2(-20, 0);
                        teamPanelList.Add(tempPanel);
                        tempPanel.GetComponent<RectTransform>().offsetMax = new Vector2(0, -(iTeamDataPanelHeight * i));
                        tempPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, -(iTeamDataPanelHeight * (i + 1)));
                        Debug.Log("Setting tempPanel offsets to: MAX: " + tempPanel.GetComponent<RectTransform>().offsetMax + ", MIN: " + tempPanel.GetComponent<RectTransform>().offsetMin);
                    }
                    break;

                case "Match Analytics":
                    foreach (SimpleMatch s in displayedMatchList)
                    {
                        Debug.Log("Displaying: " + s.ToString());
                        int i = displayedMatchList.IndexOf(s);
                        GameObject tempPanel = Instantiate(selectableItemPanel);
                        tempPanel.GetComponentInChildren<SelectableMatchPanelManager>().iNumInList = i;
                        tempPanel.GetComponentInChildren<SelectableMatchPanelManager>().containedMatch = s;
                        tempPanel.transform.SetParent(content.transform);
                        tempPanel.GetComponent<Button>().onClick.AddListener(() => manager.CreatePanelWrapper("teamPanel:" + JsonUtility.ToJson(s)));
                        content.GetComponent<RectTransform>().offsetMin = new Vector2(0, (-iTeamDataPanelHeight * i) - iTeamDataPanelHeight);
                        content.GetComponent<RectTransform>().offsetMax = new Vector2(-20, 0);
                        teamPanelList.Add(tempPanel);
                        tempPanel.GetComponent<RectTransform>().offsetMax = new Vector2(0, -(iTeamDataPanelHeight * i));
                        tempPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, -(iTeamDataPanelHeight * (i + 1)));
                        Debug.Log("Setting tempPanel offsets to: MAX: " + tempPanel.GetComponent<RectTransform>().offsetMax + ", MIN: " + tempPanel.GetComponent<RectTransform>().offsetMin);
                    }
                    break;
            }
        }

        public void ChangeDisplayedItem()
        {
            switch (analyticsTypeDropdown.GetComponent<Dropdown>().value)
            {
                case 0:
                    sItemTypeDisplaying = "Team Analytics";
                    break;
                case 1:
                    sItemTypeDisplaying = "Match Analytics";
                    break;
            }
            RefreshDisplayedItems();
        }
    }
}