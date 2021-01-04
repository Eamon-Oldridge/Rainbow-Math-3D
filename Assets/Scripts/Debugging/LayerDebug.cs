using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerDebug : MonoBehaviour
{
    private Vector3 origin = new Vector3(0, 0, 0);
    private GameObject playingField;
    private LayerMask PFMask;

    // Start is called before the first frame update
    void Start()
    {
        playingField = GameObject.Find("Plane");
        if (playingField != null)
        {
            Debug.Log("we found a playingfield");
            PFMask = LayerMask.GetMask("Playerfield");
            Debug.Log("PF Layer is: " + playingField.layer);
        }
        else
        {
            Debug.Log("no playingfield found!");
        }
    }
    //, PFMask   , LayerMask.GetMask("Playerfield")
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 collisionPoint;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layermask = 1 << 8;
            //layermask = layermask;
            //LayerMask = layermask;
            layermask = 1 << LayerMask.NameToLayer("Playfield");
            Debug.Log("layermask(PF) is: " + layermask);
            // make a mask for the raycast so that it only hits the playfield
            if (Physics.Raycast(ray.origin, Vector3.Normalize(ray.direction), out hit, 40.0f, layermask))
            {
                collisionPoint = hit.point;
                Debug.Log("colliding at at: " + collisionPoint);
            }

            else
            {
                Debug.Log("no target hit on layer " + ~layermask);
            }
        }
    }
}
