using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PointEventButtonPanel : MonoBehaviour
    {

        Button hopperLoadButton, highGoalStartButton, touchpadUnlitButton, lowGoalStartButton, climbAttemptButton, gearScoreButton, defendingButton, climbSucceedButton, cancelButton;
        public MatchEvent currentEvent;
        Text timeRemainingText;

        // Use this for initialization
        void Start()
        {
            Button[] buttonArray = GetComponentsInChildren<Button>();
            hopperLoadButton = buttonArray[0];
            highGoalStartButton = buttonArray[1];
            defendingButton = buttonArray[2];
            lowGoalStartButton = buttonArray[3];
            touchpadUnlitButton = buttonArray[4];
            gearScoreButton = buttonArray[5];
            climbAttemptButton = buttonArray[6];
            climbSucceedButton = buttonArray[7];
            cancelButton = buttonArray[8];


            cancelButton.onClick.AddListener(() => { this.SetMatchEventCode("cancel"); });
            hopperLoadButton.onClick.AddListener(() => { this.SetMatchEventCode("LOAD_HOPPER"); });
            highGoalStartButton.onClick.AddListener(() => { this.SetMatchEventCode("HIGH_GOAL_START"); });
            defendingButton.onClick.AddListener(() => { this.SetMatchEventCode("DEFENDING"); });
            lowGoalStartButton.onClick.AddListener(() => { this.SetMatchEventCode("LOW_GOAL_START"); });
            touchpadUnlitButton.onClick.AddListener(() => { this.SetMatchEventCode("TOUCHPAD_UNLIT"); });
            gearScoreButton.onClick.AddListener(() => { this.SetMatchEventCode("GEAR_SCORE"); });
            climbAttemptButton.onClick.AddListener(() => { this.SetMatchEventCode("CLIMB_ATTEMPT"); });
            climbSucceedButton.onClick.AddListener(() => { this.SetMatchEventCode("TOUCHPAD_LIT"); });

        }

        // Update is called once per frame
        void Update()
        {

        }

        void SetMatchEventCode(string s)
        {
            Debug.Log(currentEvent == null);
            Debug.Log("("  + currentEvent.loc.x + ", " + currentEvent.loc.y + ")");
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

            }
            currentEvent.sEventName = s;
            Debug.Log(JsonUtility.ToJson(currentEvent));
            Destroy(this.gameObject);
        }
    }
}