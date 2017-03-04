using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PopupMessagePanelManager : MonoBehaviour
    {
        int iTimeSinceCreation, iCreationTime;
        public string sMessageToDisplay;
        Text displayText;
        bool bIsFading;
        // Use this for initialization
        void Start()
        {
            Text displayText = GetComponentInChildren<Text>();
            Time t = new Time(System.DateTime.Now);
            iCreationTime = t.sumMilliseconds();
            bIsFading = false; 
        }

        // Update is called once per frame
        void Update()
        {
            if(displayText.text != sMessageToDisplay && sMessageToDisplay != "")
            {
                displayText.text = sMessageToDisplay;
            }
            Time t = new Time(System.DateTime.Now);
            if(t.sumMilliseconds() - iCreationTime >= 2000 && !bIsFading)
            {
                this.gameObject.GetComponent<Image>().CrossFadeAlpha(0, 1, false);
                bIsFading = true;
            }
            if(t.sumMilliseconds() - iCreationTime >= 3100)
            {
                Destroy(this);
            }
        }
    }
}