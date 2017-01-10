using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace Assets.Scripts{
    public class MatchScoutPanel_ContentManager : MonoBehaviour
    {
        string teamMatchURL = "http://frc2468.org/matchUpload";
        GameObject fieldPanel;
        RectTransform fieldPanelRectTransform;
        float aspectRatio;
        TeamMatch currentlyScoutingTeamMatch;
        // Use this for initialization
        void Start()
        {
            fieldPanel = GameObject.Find("FieldPanel");
            fieldPanelRectTransform = fieldPanel.GetComponent<RectTransform>();
            aspectRatio = (fieldPanelRectTransform.rect.height) / fieldPanelRectTransform.rect.width;
        }

        // Update is called once per frame
        void Update()
        {
            if(aspectRatio != 784 / 2048)
            {
                if (aspectRatio > 784 / 2048) //Taller than it should be
                {
                    int rightHeight = (int)((fieldPanelRectTransform.rect.width / 2048) * (784));
                    int heightMod = (int)(rightHeight - fieldPanelRectTransform.rect.height);
                    fieldPanelRectTransform.offsetMax = new Vector2(0, heightMod / 2);
                    fieldPanelRectTransform.offsetMin = new Vector2(0, - (heightMod / 2));
                    aspectRatio = (fieldPanelRectTransform.rect.height) / fieldPanelRectTransform.rect.width;
                }
                else                         //Wider than it should be
                {
                    int rightWidth = (int)((fieldPanelRectTransform.rect.height / 784) * (2048));
                    int widthMod = (int)(rightWidth - fieldPanelRectTransform.rect.width);
                    fieldPanelRectTransform.offsetMax = new Vector2(widthMod/2, 0);
                    fieldPanelRectTransform.offsetMin = new Vector2(- (widthMod/2), 0);
                    aspectRatio = (fieldPanelRectTransform.rect.height) / fieldPanelRectTransform.rect.width;
                }
            }
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