using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts
{
    public class SelectableTeamPanelManager : MonoBehaviour
    {
        Text displayText;
        public SimpleTeam containedTeam;
        RectTransform transform;
        public int iNumInList;
        Button openTeamPanelButton;
        UIManager manager;
        // Use this for initialization
        void Start()
        {
            transform = this.gameObject.GetComponent<RectTransform>();
            //transform.offsetMin = new Vector2(0, -150 - (150 * iNumInList));
            //transform.offsetMax = new Vector2(0, -150 * iNumInList);
            displayText = GetComponentInChildren<Text>();
            manager = GetComponentInParent<UIManager>();
            if (containedTeam != null)
            {
                displayText.text = containedTeam.ToString();
                openTeamPanelButton = GetComponent<Button>();
                openTeamPanelButton.onClick.AddListener(() => { manager.ChangePanel("teamPanel:" + containedTeam.iTeamNumber); });
            }
        }

        // Update is called once per frame
        void Update()
        {
            //transform.position = new Vector3(0, -150 * iNumInList - 75);
        }
    }
}