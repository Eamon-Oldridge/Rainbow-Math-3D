using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextDisplay : MonoBehaviour
{
    public void UpdateDisplay()
    {
        this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Level: " + StaticDataTracker.curLevel;
    }
}
