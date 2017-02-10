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
        const string sGetAnalysisURL = "";
        public WWW getAnalysisData;
        public bool bRefreshing = false;
        private int iRefreshTimer;
        private Vector2 size;
        // Use this for initialization
        void Start()
        {
            manager = GetComponentInParent<UIManager>();
            content = GameObject.Find("Content");
            content.GetComponentsInChildren<Image>()[0].enabled = false;
            scrollview = GameObject.Find("Scroll View");
            foreach (SimpleTeam s in manager.currentEvent.simpleTeamList)
            {
                int i = manager.currentEvent.simpleTeamList.IndexOf(s);
                GameObject tempPanel = Instantiate(selectableTeamPanel);
                tempPanel.GetComponentInChildren<SelectableTeamPanelManager>().iNumInList = i;
                tempPanel.GetComponentInChildren<SelectableTeamPanelManager>().containedTeam = s;
                tempPanel.transform.SetParent(content.transform);
                tempPanel.GetComponent<Button>().onClick.AddListener(() => manager.CreatePanelWrapper("teamPanel:" + JsonUtility.ToJson(s)));
                content.GetComponent<RectTransform>().offsetMin = new Vector2(0, (-150 * i) - 150);
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            if ((content.GetComponent<RectTransform>().offsetMax.y < -115) && bRefreshing == false)
            {
                StartCoroutine(Refresh());
                size = content.GetComponent<RectTransform>().sizeDelta;
            }
            if (bRefreshing == true)
            {
                content.GetComponent<RectTransform>().offsetMin = new Vector2(0, (-150 * simpleTeamList.Count) - 150);
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
            }
        }

        public IEnumerator Refresh()
        {
            content.GetComponentsInChildren<Image>()[0].enabled = true;
            getAnalysisData = new WWW(sGetAnalysisURL);
            bRefreshing = true;
            scrollview.GetComponent<ScrollRect>().movementType = ScrollRect.MovementType.Unrestricted;
            scrollview.GetComponent<ScrollRect>().inertia = false;
            yield break;
        }

    }
}
