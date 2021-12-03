using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEditLegend : MonoBehaviour
{
    ColorEditNumber activeNumber;

    public void SaveColors()
    {
        if (activeNumber != null) { activeNumber.SetColorFromFCP(); }
    }

    public void ResetColors()
    {
        ColorEditNumber[] nums = GetComponentsInChildren<ColorEditNumber>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                PlayerPrefs.DeleteKey(i.ToString() + j.ToString());
            }
        }
        foreach (ColorEditNumber n in nums)
        {
            n.myTMP.color = StaticDataTracker.GetDefaultColor(n.myVal % 10);
        }
    }

    public void SetActiveNumber(ColorEditNumber n)
    {
        activeNumber = n;
    }
}
