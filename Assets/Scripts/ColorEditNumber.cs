using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorEditNumber : MonoBehaviour
{
    public Material myMat;
    public FlexibleColorPicker fcp;
    public TMPro.TextMeshProUGUI myTMP;
    public int myVal = 1;

    // Start is called before the first frame update
    void Start()
    {
        myMat = this.gameObject.GetComponent<Material>();
        fcp = FindObjectOfType<FlexibleColorPicker>();
        myTMP = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
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
            rgb[i] = PlayerPrefs.GetInt(colString);
        }
        return new Color(rgb[0], rgb[1], rgb[2]);
    }
}
