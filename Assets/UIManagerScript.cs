using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManagerScript : MonoBehaviour {

    public RectTransform pitScoutTransform, matchScoutTransform, analyticsTransform;
    public Text titleText;
	// Use this for initialization
	void Start () {
        Text[] textArray = GetComponentsInChildren<Text>();
        titleText = textArray[3];
        RectTransform[] rectTransformArray = GetComponentsInChildren<RectTransform>();
        pitScoutTransform = rectTransformArray[1];
        matchScoutTransform = rectTransformArray[3];
        analyticsTransform = rectTransformArray[5];
        Vector2 rectSize = titleText.GetComponent<RectTransform>().sizeDelta;
        rectSize = new Vector2(rectSize.x, (1/4) * Screen.height);
        titleText.rectTransform.sizeDelta = rectSize;
        pitScoutTransform.sizeDelta = rectSize;
        matchScoutTransform.sizeDelta = rectSize;
        analyticsTransform.sizeDelta = rectSize;
    }

    // Update is called once per frame
    void Update () {
	    
	}
}
