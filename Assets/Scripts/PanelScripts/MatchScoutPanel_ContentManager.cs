using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace Assets.Scripts{
    public class MatchScoutPanel_ContentManager : MonoBehaviour
    {
        string teamMatchURL = "http://frc2468.org/matchUpload";
        TeamMatch currentlyScoutingTeamMatch;
        // Use this for initialization
        void Start()
        {

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
    }
}