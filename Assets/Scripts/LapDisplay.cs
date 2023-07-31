using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapDisplay : MonoBehaviour
{
    static int lapCount = 0;
    public Text lapText;
    public void updateLap(int lapsCompleted)
    {
        if(lapCount < lapsCompleted)
        {
            lapCount = lapsCompleted;
        }
        lapText.text = "Lap " + lapCount + "/5";
        Debug.Log(lapCount);
    }
}
