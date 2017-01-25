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
        public List<SimpleTeam> simpleTeamList = new List<SimpleTeam>();
        public List<SimpleMatch> simpleMatchList;
        UIManager manager;
        // Use this for initialization
        void Start()
        {
            simpleTeamList.Add(new SimpleTeam("Team Appreciate", 2468));
            simpleTeamList.Add(new SimpleTeam("Team Appreciate", 2468));
            simpleTeamList.Add(new SimpleTeam("Team Appreciate", 2468));
            simpleTeamList.Add(new SimpleTeam("Team Appreciate", 2468));
            simpleTeamList.Add(new SimpleTeam("Team Appreciate", 2468));
            simpleTeamList.Add(new SimpleTeam("Team Appreciate", 2468));
            simpleTeamList.Add(new SimpleTeam("Team Appreciate", 2468));
            manager = GetComponentInParent<UIManager>();
            content = GameObject.Find("Content");
            foreach (SimpleTeam s in simpleTeamList)
            {
                int i = simpleTeamList.IndexOf(s);
                GameObject tempPanel = Instantiate(selectableTeamPanel);
                tempPanel.GetComponentInChildren<SelectableTeamPanelManager>().iNumInList = i;
                tempPanel.GetComponentInChildren<SelectableTeamPanelManager>().containedTeam = s;
                tempPanel.transform.SetParent(content.transform);
                tempPanel.GetComponent<Button>().onClick.AddListener(() => manager.CreatePanelWrapper("teampanel:" + JsonUtility.ToJson(s)));
                content.GetComponent<RectTransform>().offsetMin = new Vector2(0, (-150 * i) - 150);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}