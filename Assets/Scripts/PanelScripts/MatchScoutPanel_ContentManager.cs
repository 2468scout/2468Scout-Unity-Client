using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

namespace Assets.Scripts{
    public class MatchScoutPanel_ContentManager : MonoBehaviour
    {
        string teamMatchURL = "http://frc2468.org/matchUpload";
        GameObject fieldImage;
        RectTransform fieldImageRectTransform;
        float aspectRatio;
        TeamMatch currentlyScoutingTeamMatch;
        Button backButton, matchStartButton, menuButton, fieldImageButton;
        // Use this for initialization
        void Start()
        {
            fieldImage = GameObject.Find("FieldImage");
            fieldImageRectTransform = fieldImage.GetComponent<RectTransform>();
            fieldImageRectTransform.offsetMin = new Vector2(0, -(GameObject.Find("FieldPanel").GetComponent<RectTransform>().rect.width /2048) * 784 / 2);
            fieldImageRectTransform.offsetMax = new Vector2(0, (GameObject.Find("FieldPanel").GetComponent<RectTransform>().rect.width / 2048) * 784 / 2);
            Debug.Log("FieldPanel width:" + GameObject.Find("FieldPanel").GetComponent<RectTransform>().rect.width + ", FieldPanel height: " + GameObject.Find("FieldPanel").GetComponent<RectTransform>().rect.height + ", calculated height:"  + ((GameObject.Find("FieldPanel").GetComponent<RectTransform>().rect.width / 2048) * 784));
            Button[] buttonArray = GetComponentsInChildren<Button>();
            backButton = buttonArray[0];
            menuButton = buttonArray[1];
            matchStartButton = buttonArray[2];
            fieldImageButton = buttonArray[3];
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public void SaveTeamMatch()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + currentlyScoutingTeamMatch.sFileName + ".json");
            bf.Serialize(file, null);
            file.Close();
        }
        public IEnumerator SendTeamMatch ()
        {
            SaveTeamMatch();
            WWWForm form = new WWWForm();
            form.AddField("obj", JsonUtility.ToJson(currentlyScoutingTeamMatch));
            WWW download = new WWW(teamMatchURL, form);
            yield return download;
            if (!string.IsNullOrEmpty(download.error))
            {
                print("Error uploading: " + download.error);
            }
            else if(download.text == "Error")
            {
                print("Unknown Upload Error");
            }
            else if(download.text == "Success")
            {
                print("Success!");
            }            
        }

        public void CreatePointMatch()
        {

        }
    }
}