using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UpcomingMatchPanel_ContentManager : MonoBehaviour
    {
        public GameObject redTeamText1, redTeamText2, redTeamText3, blueTeamText1, blueTeamText2, blueTeamText3;
        public PreMatch preMatch;
        public List<SimpleTeam> simpleTeamListRed, simpleTeamListBlue;
        public List<HeatmapsData> heatMapsRed, heatMapsBlue;

        // Use this for initialization
        void Start()
        {
            redTeamText1 = GameObject.Find("RedTeamText1");
            redTeamText2 = GameObject.Find("RedTeamText2");
            redTeamText3 = GameObject.Find("RedTeamText3");
            blueTeamText1 = GameObject.Find("BlueTeamText1");
            blueTeamText2 = GameObject.Find("BlueTeamText2");
            blueTeamText3 = GameObject.Find("BlueTeamText3");
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateData()
        {
            redTeamText1.GetComponent<Text>().text = preMatch.redSimpleTeams[1].ToString();
            redTeamText2.GetComponent<Text>().text = preMatch.redSimpleTeams[2].ToString();
            redTeamText3.GetComponent<Text>().text = preMatch.redSimpleTeams[3].ToString();
            blueTeamText1.GetComponent<Text>().text = preMatch.blueSimpleTeams[1].ToString();
            blueTeamText2.GetComponent<Text>().text = preMatch.blueSimpleTeams[2].ToString();
            blueTeamText3.GetComponent<Text>().text = preMatch.blueSimpleTeams[3].ToString();

        }
    }
}