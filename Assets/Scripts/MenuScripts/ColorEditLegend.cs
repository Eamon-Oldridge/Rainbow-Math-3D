using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorEditLegend : MonoBehaviour
{
    ColorEditNumber activeNumber;
    [SerializeField] bool isEditor = false;

    void Start()
    {
        if (!isEditor)
        {
            foreach (Button b in GetComponentsInChildren<Button>())
            {
                b.interactable = false;
            }
        }
    }

    public void SaveColors()
    {
        if (activeNumber != null) { activeNumber.SetColorFromFCP(); }
    }

    public void UpdateColors()
    {
        foreach (ColorEditNumber n in GetComponentsInChildren<ColorEditNumber>())
        {
            n.SetMyColor();
        }
    }

    public void ResetColor(int val)
    {
        for (int j = 0; j < 3; j++)
        {
            PlayerPrefs.DeleteKey(val.ToString() + j.ToString());
        }
    }
    public void ResetColors()
    {
        ColorEditNumber[] nums = GetComponentsInChildren<ColorEditNumber>();
        for (int i = 0; i < 10; i++)
        {
            ResetColor(i);
        }
        foreach (ColorEditNumber n in nums)
        {
            n.myTMP.color = n.GetDefaultColor(n.myVal % 10);
        }
    }

    public void SetActiveNumber(ColorEditNumber n)
    {
        activeNumber = n;
    }
}
