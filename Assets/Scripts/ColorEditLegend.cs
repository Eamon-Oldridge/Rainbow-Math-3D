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

    public void SetActiveNumber(ColorEditNumber n)
    {
        activeNumber = n;
    }
}
