using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowCubeRendDebug : MonoBehaviour
{
    public Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        /*
        rend = this.gameObject.GetComponent<Renderer>();
        Debug.Log(rend);
        Debug.Log(rend.material);
        rend.material.color = Color.red;
        Debug.Log(rend.material.color);
        */
        int childCount = 0;
        foreach (Transform child in transform) // this is the thing that worked.
        {
            Debug.Log("Child# " + childCount);
            Debug.Log(transform.gameObject);
            childCount++;
            if (child.tag == "MeshMember")
            {
                child.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
