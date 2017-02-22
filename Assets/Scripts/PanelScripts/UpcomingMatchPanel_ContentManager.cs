using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class UpcomingMatchPanel_ContentManager : MonoBehaviour
    {
        public GameObject redTeamText1, redTeamText2, redTeamText3, blueTeamText1, blueTeamText2, blueTeamText3;
        public PreMatch preMatch;
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
    }
}