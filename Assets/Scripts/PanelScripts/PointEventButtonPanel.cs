using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointEventButtonPanel : MonoBehaviour {

    Button hopperLoadButton, highGoalStartButton, highGoalMissButton, lowGoalStartButton, lowGoalMissButton, gearScoreButton, defendingButton, climbSucceedButton, cancelButton;
	// Use this for initialization
	void Start () {
        Button[] buttonArray = GetComponentsInChildren<Button>();
        hopperLoadButton = buttonArray[0];
        highGoalStartButton = buttonArray[1];
        highGoalMissButton = buttonArray[2];
        lowGoalStartButton = buttonArray[3];
        lowGoalMissButton = buttonArray[4];
        gearScoreButton = buttonArray[5];
        defendingButton = buttonArray[6];
        climbSucceedButton = buttonArray[7];
        cancelButton = buttonArray[8];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
