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
        public List<SimpleMatch> simpleMatchList;
        UIManager manager;
        public bool bRefreshing = false;
        private int iRefreshTimer;
        private Vector2 size;
        int iTeamDataPanelHeight = (Screen.height / 5);
        // Use this for initialization
        void Start()
        {
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
            content.GetComponentsInChildren<Image>()[0].enabled = false;
            scrollview = GameObject.Find("Scroll View");
            foreach (SimpleTeam s in manager.currentEvent.simpleTeamList)
            {
                int i = manager.currentEvent.simpleTeamList.IndexOf(s);
                GameObject tempPanel = Instantiate(selectableTeamPanel);
                tempPanel.GetComponentInChildren<SelectableTeamPanelManager>().iNumInList = i;
                tempPanel.GetComponentInChildren<SelectableTeamPanelManager>().containedTeam = s;
                tempPanel.GetComponent<RectTransform>().offsetMax = new Vector2(0, iTeamDataPanelHeight * i);
                tempPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, iTeamDataPanelHeight * (i + 1));
                Debug.Log("Changed offsetmax to " + (iTeamDataPanelHeight * i) + " and offsetMin to " + (iTeamDataPanelHeight * (i + 1)));
                tempPanel.transform.SetParent(content.transform);
                tempPanel.GetComponent<Button>().onClick.AddListener(() => manager.CreatePanelWrapper("teamPanel:" + JsonUtility.ToJson(s)));
                content.GetComponent<RectTransform>().offsetMin = new Vector2(0, (-iTeamDataPanelHeight * i) - iTeamDataPanelHeight);
            }
        }
        // Update is called once per frame
        void Update()
        {
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
            bRefreshing = false;
            yield break;
        }

    }
}
