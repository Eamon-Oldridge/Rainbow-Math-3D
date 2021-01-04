using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullCubeModel : VNModel
{
    // Start is called before the first frame update
    void Start()
    {
        rend = this.gameObject.GetComponent<Renderer>();
    }

    public override void SetColor(Color color)
    {
        this.gameObject.GetComponent<Renderer>().material.color = color;
    }
}
