using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class TeamMatch{
    public int iTeamNumber, iMatchNumber, iAllianceNumber;
    public string sNotes, sFileName, sEventCode;
    public bool bColor; // False is blue, true is red
    // GAME SPECIFIC ELEMENTS

}
