using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorEditNumber : Colorable
{
    public FlexibleColorPicker fcp;
    public TMPro.TextMeshProUGUI myTMP;
    public int myVal = 1;
    Button myButton;
    ColorEditLegend legend;

    // Start is called before the first frame update
    void Start()
    {
        myTMP = gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        fcp = FindObjectOfType<FlexibleColorPicker>();
        myTMP.text = myVal.ToString();
        myTMP.color = PlayerPrefsValueToColor(myVal);
        myButton = gameObject.GetComponent<Button>();
        legend = gameObject.GetComponentInParent<ColorEditLegend>();
        myButton.onClick.AddListener(() => {
            legend.SetActiveNumber(this);
            SetFCPColor();
        });
    }

    public override Color GetMyColor()
    {
        return myTMP.color;
    }

    public void SetFCPColor()
    {
        fcp.color = myTMP.color;
    }

    public void SetColorFromFCP()
    {
        SaveColor(fcp.color);
    }

    public override void SetMyColor(Color c)
    {
        myTMP = gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        myTMP.color = c;
    }
    public void SetMyColor() { SetMyColor(PlayerPrefsValueToColor(myVal)); }

    public void SaveColor(Color c)
    {
        SetMyColor(c);
        int[] rgb = { Mathf.FloorToInt(c.r*255f),
                      Mathf.FloorToInt(c.g*255f),
                      Mathf.FloorToInt(c.b*255f) };
        string keyString = "";

        for (int i = 0; i < 3; i++)
        {
            keyString = myVal.ToString() + i.ToString();
            PlayerPrefs.SetInt(keyString, rgb[i]);
            //Debug.Log("Setting PP key " + keyString + " to: " + rgb[i].ToString());
        }
    }

}
