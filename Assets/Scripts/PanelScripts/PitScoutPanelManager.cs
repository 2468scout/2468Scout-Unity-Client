using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PitScoutPanelManager : MonoBehaviour
    {

        Text speedResponseText;
        TeamPitScout currentPitScout;
        UIManager manager;
        // Use this for initialization
        void Start()
        {
            manager = GetComponentInParent<UIManager>();
            GameObject content = GameObject.Find("Content");
            GameObject toolbar = GameObject.Find("ToolbarPanel");
            //content.GetComponent<RectTransform>().offsetMin = new Vector2(0,(float) (-Screen.height * 1.2));
            
            speedResponseText = GetComponentsInChildren<Text>()[3];
            currentPitScout = manager.teamPitScoutsToScout[manager.iNumInTeamPitScouts];
            GetComponentsInChildren<Text>()[2].text = "#"+currentPitScout.iTeamNumber;
            //Debug.Log(toolbar.GetComponentsInChildren<Text>()[2].text);
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
            currentPitScout.sPitScoutNotes = GetComponentsInChildren<Text>()[14].text;
            manager.iNumInTeamPitScouts++;
            

            GetComponentsInChildren<Text>()[3].text = "";
            GetComponentsInChildren<Text>()[6].text = "";
            GetComponentsInChildren<Toggle>()[0].isOn = false;
            GetComponentsInChildren<Toggle>()[1].isOn = false;
            GetComponentsInChildren<Toggle>()[2].isOn = false;
            GetComponentsInChildren<Toggle>()[3].isOn = false;
            GetComponentsInChildren<Toggle>()[4].isOn = false;
            GetComponentsInChildren<Toggle>()[5].isOn = false;
            Debug.Log("Speed: " + currentPitScout.iSpeed + " Fuel: " + currentPitScout.iFuelCapacity + " Can High Goal: " + currentPitScout.bCanHighGoal + " Can Low Goal: " + currentPitScout.bCanLowGoal + " Can Climb: " + currentPitScout.bCanClimb + " Can Gears: " + currentPitScout.bCanGears + " Can Hopper: " + currentPitScout.bCanHopper + " Can Intake: " + currentPitScout.bCanIntake);
            Save();
        }

        public IEnumerator UploadPicture()
        {
            //EditorUtility.OpenFilePanel()
            if (Application.platform == RuntimePlatform.Android)
            {

            } else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {

            }
            yield break;
        }
        public void Save()
        {
            Debug.Log("Filename: " + Application.persistentDataPath + "pit_team" + currentPitScout.iTeamNumber + "_event"+currentPitScout.sEventCode);
            FileStream file = File.Create(Application.persistentDataPath + "pit_team" + currentPitScout.iTeamNumber + "_event" + currentPitScout.sEventCode + ".json");
            manager.listTeamPitScoutFilePaths.Add(Application.persistentDataPath + "pit_team" + currentPitScout.iTeamNumber + "_event" + currentPitScout.sEventCode + ".json");
            file.Write(Encoding.ASCII.GetBytes(JsonUtility.ToJson(currentPitScout)), 0, Encoding.ASCII.GetByteCount(JsonUtility.ToJson(currentPitScout)));
            file.Dispose();
            manager.bHasTeamPitScoutsToSend = true;
        }
    }
}