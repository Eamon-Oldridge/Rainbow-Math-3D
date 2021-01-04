using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowCubeModel : VNModel
{
    void Start()
    {
        rend = this.gameObject.GetComponent<Renderer>();
    }

    public override void SetColor(Color color)
    {
        this.gameObject.GetComponent<Renderer>().material.color = color;
        // int childCount = 0;
        foreach (Transform child in transform) // this is the thing that worked.
        {
            //Debug.Log("Child# " + childCount);
            //Debug.Log(transform.gameObject);
            //childCount++;
            if (child.tag == "MeshMember")
            {
                child.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
