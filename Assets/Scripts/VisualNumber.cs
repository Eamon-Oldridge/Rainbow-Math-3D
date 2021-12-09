using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VisualNumber : Colorable
{
    public int myValue;

    public GameObject activeModel;

    public void UpdateFeatures()
    {
        UpdateScale();
        SetMyColor(myValue);
        UpdateShape();
    }
    public override Color GetMyColor()
    {
        return PlayerPrefsValueToColor(myValue);
    }

    // sets this block's color when given a color .. depricated?
    public override void SetMyColor(Color c)
    {
        SetRendModel(); // updates activeModel
        if(activeModel.GetComponent<VNModel>() != null)
        {
            activeModel.GetComponent<VNModel>().SetMyColor(c);
        }
    }
    // sets this block's color when given an integer value

    public abstract void UpdateScale();
    public abstract void SetRendModel();
    public abstract void UpdateShape();
}
