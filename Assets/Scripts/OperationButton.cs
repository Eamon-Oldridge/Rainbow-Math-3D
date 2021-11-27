using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationButton : MonoBehaviour
{
    public string myName;
    public int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        myName = this.gameObject.name;
        //Debug.Log("Button name is " + name);
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        this.gameObject.GetComponentInChildren<Text>().text = myName + ": " + count;
    }

}
