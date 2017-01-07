using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace Assets.Scripts{
    public class MatchScoutPanel_ContentManager : MonoBehaviour
    {

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
            FileStream file = File.Create(Application.persistentDataPath + currentlyScoutingTeamMatch.sFileName + ".dat");
            bf.Serialize(file, null);
            file.Close();
        }
    }
}