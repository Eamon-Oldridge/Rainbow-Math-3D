using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticDataTracker
{
    public static int curLevel = 1;

    // 1 :: Red
    public static Color myRed = new Vector4(1f, .1f, .1f, 1f);
    // 2 :: Orange //(.996f, .263f, .212f, 1f);
    public static Color myOrange = new Vector4(.996f, .4f, .15f, 1f);
    // 3 :: Yellow (1f, .922f, .231f, 1f);
    public static Color myYellow = new Vector4(1f, .922f, .2f, 1f);
    // 4 :: Green
    public static Color myGreen = new Vector4(.315f, .69f, .315f, 1f);
    // 5 :: Strong-Green
    public static Color myStrongGreen = new Vector4(.05f, .432f, .032f, 1f);
    // 6 :: Cyan
    public static Color myCyan = new Vector4(0, .737f, .831f, 1f);
    // 7 :: Blue
    public static Color myBlue = new Vector4(.039f, .4f, 1f, 1f);
    // 8 :: Indigo
    //public static Color myIndigo = new Vector4(.247f, .098f, 1f, 1f);
    public static Color myIndigo = new Vector4(.42745f, .302f, 1f, 1f);
    // 9 :: purple
    public static Color myPurple = new Vector4(.698f, .098f, 1f, 1f);
    // 0 :: Pink
    public static Color myPink = new Vector4(.901f, .117f, .894f, 1f);

    public static Color GetDefaultColor(int index)
    {
        switch (index)
        {
            case 0:
                return myPink;
            //break;
            case 1:
                return myRed;
            //break;
            case 2:
                return myOrange;
            //break;
            case 3:
                return myYellow;
            //break;
            case 4:
                return myGreen;
            //break;
            case 5:
                return myStrongGreen;
            //break;
            case 6:
                return myCyan;
            //break;
            case 7:
                return myBlue;
            //break;
            case 8:
                return myIndigo;
            //break;
            case 9:
                return myPurple;
            //break;

            default:
                return Color.gray;
                //break;
        }
    }

}
