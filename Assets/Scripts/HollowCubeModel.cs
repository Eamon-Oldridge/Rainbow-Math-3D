using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowCubeModel : VNModel
{
    void Awake()
    {
        rend = this.gameObject.GetComponent<Renderer>();
    }

    public override void SetMyColor(Color color)
    {
        rend.material.color = color;
        // int childCount = 0;
        foreach (Transform child in transform) // this is the thing that worked.
        {
            if (child.tag == "MeshMember")
            {
                child.GetComponent<Renderer>().material.color = color;
            }
        }
    }
}
