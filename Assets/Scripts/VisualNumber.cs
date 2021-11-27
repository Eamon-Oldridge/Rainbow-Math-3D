using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VisualNumber : MonoBehaviour
{
    public int myValue;

    // 1 :: Red
    public Color myRed = new Vector4(1f, .1f, .1f, 1f);
    // 2 :: Orange //(.996f, .263f, .212f, 1f);
    public Color myOrange = new Vector4(.996f, .4f, .15f, 1f);
    // 3 :: Yellow (1f, .922f, .231f, 1f);
    public Color myYellow = new Vector4(1f, .922f, .2f, 1f);
    // 4 :: Green
    public Color myGreen = new Vector4(.315f, .69f, .315f, 1f);
    // 5 :: Strong-Green
    public Color myStrongGreen = new Vector4(.05f, .432f, .032f, 1f);
    // 6 :: Cyan
    public Color myCyan = new Vector4(0, .737f, .831f, 1f);
    // 7 :: Blue
    public Color myBlue = new Vector4(.012f, .4f, 1f, 1f);
    // 8 :: Magenta
    public Color myMagenta = new Vector4(.612f, .153f, .69f, 1f);
    // 9 :: Pink
    public Color myPink = new Vector4(.914f, .118f, .388f, 1f);
    // 10 :: Bright-Pink
    public Color myBrightPink = new Vector4(.975f, .45f, .55f, 1f);
    // 12 :: Indigo
    public Color myIndigo = new Vector4(.247f, .318f, .71f, 1f);
    // 11 :: soft-blue
    public Color mySoftBlue = new Vector4(.012f, .663f, 1f, 1f);
    // 14 :: Soft-Black
    public Color myBlack = new Vector4(.07f, .02f, .02f, 1f);
    // 13 :: Rose-White
    public Color myWhite = new Vector4(.9f, .8f, .8f, 1f);
    // -- :: Soft-Orange
    public Color mySoftOrange = new Vector4(1f, .596f, 0, 1f);

    public GameObject activeModel;

    /*void Start()
    {
        SetRend();
        SetMyColor(myValue);
    }*/
    public void UpdateFeatures()
    {
        UpdateScale();
        SetMyColor(myValue);
        UpdateShape();
    }
    public Color GetColor(int value)
    {
        //Debug.Log("color val: " + (Mathf.Abs(value) % 10));
        switch (Mathf.Abs(value) % 10)
        {
            case 0:
                return myBrightPink;
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
                return myMagenta;
            //break;
            case 9:
                return myPink;
            //break;

            default:
                return Color.gray;
                //break;
        }
    }

    // sets this block's color when given a color .. depricated?
    public void SetMyColor(Color color)
    {
        SetRendModel(); // updates activeModel
        activeModel.GetComponent<VNModel>().SetColor(color); 
    }
    // sets this block's color when given an integer value
    public void SetMyColor(int value)
    {
        SetRendModel();
        if(activeModel.GetComponent<VNModel>() != null)
        {
            activeModel.GetComponent<VNModel>().SetColor(value); // possible future improvement is to generalize this in VN, and allow different shapes to set their colors more dynamically
        }
        /*
        foreach (Transform child in activeRend.gameObject.transform)
        {
            if (child.tag == "MeshMember")
            {
                child.GetComponent<Renderer>().material.color = GetColor(value);
            }
        }
        */
        /*
        int childCount = 0;
        foreach (Transform child in activeRend.gameObject.transform) // this is the thing that worked.
        {
            Debug.Log("Child# " + childCount);
            Debug.Log(transform.gameObject);
            childCount++;
            if (child.tag == "MeshMember")
            {
                child.GetComponent<Renderer>().material.color = GetColor(value);
            }
        }
        */
    }

    public abstract void UpdateScale();
    public abstract void SetRendModel();
    public abstract void UpdateShape();
}
