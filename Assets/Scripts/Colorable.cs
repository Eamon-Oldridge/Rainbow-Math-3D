using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Colorable : MonoBehaviour
{

    public Color GetDefaultColor(int index)
    {
        switch (Mathf.Abs(index) % 10)
        {
            case 0:
                return StaticDataTracker.myPink;
            //break;
            case 1:
                return StaticDataTracker.myRed;
            //break;
            case 2:
                return StaticDataTracker.myOrange;
            //break;
            case 3:
                return StaticDataTracker.myYellow;
            //break;
            case 4:
                return StaticDataTracker.myGreen;
            //break;
            case 5:
                return StaticDataTracker.myStrongGreen;
            //break;
            case 6:
                return StaticDataTracker.myCyan;
            //break;
            case 7:
                return StaticDataTracker.myBlue;
            //break;
            case 8:
                return StaticDataTracker.myIndigo;
            //break;
            case 9:
                return StaticDataTracker.myPurple;
            //break;

            default:
                return Color.gray;
                //break;
        }
    }

    public abstract void SetMyColor(Color c);

    public void SetMyColor(int val)
    {
        SetMyColor(PlayerPrefsValueToColor(val));
    }

    public abstract Color GetMyColor();

    public Color PlayerPrefsValueToColor(int val)
    {
        int[] rgb = { 0, 0, 0 };
        string keyString = "";

        for (int i = 0; i < 3; i++)
        {
            keyString = val.ToString() + i.ToString();
            if (!PlayerPrefs.HasKey(keyString))
            {
                //Debug.Log("Number " + myVal + " doens't have a PP Color saved");
                return GetDefaultColor(val);
            }
            rgb[i] = PlayerPrefs.GetInt(keyString);
            //Debug.Log("Pulling PP pair " + keyString + " : " + rgb[i].ToString() + " to rgb: " + i.ToString());
        }
        return new Color(rgb[0] / 255f, rgb[1] / 255f, rgb[2] / 255f);
    }

}
