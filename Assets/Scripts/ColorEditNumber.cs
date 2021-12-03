using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorEditNumber : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public TMPro.TextMeshProUGUI myTMP;
    public int myVal = 1;

    // Start is called before the first frame update
    void Start()
    {
        myTMP = gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        fcp = FindObjectOfType<FlexibleColorPicker>();
        myTMP.text = myVal.ToString();
        SetMyColor(PPValueToColor(myVal));
    }

    public void SetMyColor(Color color)
    {
        myTMP.material.color = color;
    }

    Color PPValueToColor(int val)
    {
        int[] rgb = { 0, 0, 0 };
        string colString = "";

        for (int i = 0; i < 3; i++){
            colString = myVal.ToString() + i.ToString();
            if (!PlayerPrefs.HasKey(colString)) { return myTMP.material.color; }
            rgb[i] = PlayerPrefs.GetInt(colString);
        }
        return new Color(rgb[0], rgb[1], rgb[2]);
    }
}
