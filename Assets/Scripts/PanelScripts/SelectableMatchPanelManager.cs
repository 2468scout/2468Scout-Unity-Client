using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SelectableMatchPanelManager : MonoBehaviour
    {
        Text displayText;
        Button openMatchPanelButton;
        public SimpleMatch containedMatch;
        UIManager manager;
        // Use this for initialization
        void Start()
        {
            displayText = GetComponentInChildren<Text>();
            manager = GetComponentInParent<UIManager>();
            if (containedMatch != null)
            {
                displayText.text = containedMatch.ToString();
                openMatchPanelButton = GetComponent<Button>();
                openMatchPanelButton.onClick.AddListener(() => { manager.ChangePanel("teamPanel:" + JsonUtility.ToJson(containedMatch)); });
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}