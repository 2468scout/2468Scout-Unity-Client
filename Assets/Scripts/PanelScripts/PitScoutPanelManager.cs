using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PitScoutPanelManager : MonoBehaviour
    {

        Text speedResponseText;
        TeamPitScout currentPitScout;
        // Use this for initialization
        void Start()
        {
            GameObject content = GameObject.Find("content");
            content.GetComponent<RectTransform>().offsetMin = new Vector2(0,(float) (-Screen.height * 1.2));
            speedResponseText = GetComponentsInChildren<Text>()[3];
            currentPitScout = new TeamPitScout();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Submit()
        {

            currentPitScout.iSpeed = int.Parse(GetComponentsInChildren<Text>()[3].text.Trim());
            currentPitScout.iFuelCapacity = int.Parse(GetComponentsInChildren<Text>()[6].text.Trim());
            currentPitScout.bCanHighGoal = GetComponentsInChildren<Toggle>()[0].isOn;
            currentPitScout.bCanLowGoal = GetComponentsInChildren<Toggle>()[1].isOn;
            currentPitScout.bCanClimb = GetComponentsInChildren<Toggle>()[2].isOn;
            currentPitScout.bCanGears = GetComponentsInChildren<Toggle>()[3].isOn;
            currentPitScout.bCanHopper = GetComponentsInChildren<Toggle>()[4].isOn;
            currentPitScout.bCanIntake = GetComponentsInChildren<Toggle>()[5].isOn;

            Debug.Log("Speed: " + currentPitScout.iSpeed + " Fuel: " + currentPitScout.iFuelCapacity + " Can High Goal: " + currentPitScout.bCanHighGoal + " Can Low Goal: " + currentPitScout.bCanLowGoal + " Can Climb: " + currentPitScout.bCanClimb + " Can Gears: " + currentPitScout.bCanGears + " Can Hopper: " + currentPitScout.bCanHopper + " Can Intake: " + currentPitScout.bCanIntake);

        }
    }
}