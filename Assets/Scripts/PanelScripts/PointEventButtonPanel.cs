using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PointEventButtonPanel : MonoBehaviour
    {

        Button hopperLoadButton, highGoalStartButton, highGoalMissButton, lowGoalStartButton, lowGoalMissButton, gearScoreButton, defendingButton, climbSucceedButton, cancelButton;
        public MatchEvent currentEvent;
        Text timeRemainingText;

        // Use this for initialization
        void Start()
        {
            currentEvent = new MatchEvent();
            Button[] buttonArray = GetComponentsInChildren<Button>();
            hopperLoadButton = buttonArray[0];
            highGoalStartButton = buttonArray[1];
            highGoalMissButton = buttonArray[2];
            lowGoalStartButton = buttonArray[3];
            lowGoalMissButton = buttonArray[4];
            gearScoreButton = buttonArray[5];
            defendingButton = buttonArray[6];
            climbSucceedButton = buttonArray[7];
            cancelButton = buttonArray[8];


            cancelButton.onClick.AddListener(() => { this.SetMatchEventCode("cancel"); });
            hopperLoadButton.onClick.AddListener(() => { this.SetMatchEventCode("LOAD_HOPPER"); });
            highGoalStartButton.onClick.AddListener(() => { this.SetMatchEventCode("HIGH_GOAL_START"); });
            highGoalMissButton.onClick.AddListener(() => { this.SetMatchEventCode("HIGH_GOAL_MISS"); });
            lowGoalStartButton.onClick.AddListener(() => { this.SetMatchEventCode("LOW_GOAL_START"); });
            lowGoalMissButton.onClick.AddListener(() => { this.SetMatchEventCode("LOW_GOAL_MISS"); });
            gearScoreButton.onClick.AddListener(() => { this.SetMatchEventCode("GEAR_SCORE"); });
            defendingButton.onClick.AddListener(() => { this.SetMatchEventCode("DEFENDING"); });
            climbSucceedButton.onClick.AddListener(() => { this.SetMatchEventCode("CLIMB_SUCCEED"); });

        }

        // Update is called once per frame
        void Update()
        {

        }

        void SetMatchEventCode(string s)
        {
            switch (s)
            {

                case "cancel":
                    GetComponentInParent<MatchScoutPanel_ContentManager>().currentlyScoutingTeamMatch.matchEventList.Remove(this.currentEvent);
                    Destroy(this.gameObject);
                    break;

                case "HIGH_GOAL_START":
                    GetComponentInParent<MatchScoutPanel_ContentManager>().MatchEventStart(currentEvent, "HIGH_GOAL_STOP");
                    break;

                case "LOW_GOAL_START":
                    GetComponentInParent<MatchScoutPanel_ContentManager>().MatchEventStart(currentEvent, "LOW_GOAL_STOP");
                    break;

                case "HIGH_GOAL_MISS":
                    break;

                case "LOW_GOAL_MISS":
                    break;
            }
            currentEvent.sEventName = s;
            Debug.Log(JsonUtility.ToJson(currentEvent));
            Destroy(this.gameObject);
        }
    }
}