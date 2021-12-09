using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullCubeModel : VNModel
{
    // Start is called before the first frame update
    void Awake()
    {
        rend = this.gameObject.GetComponent<Renderer>();
    }

    public override void SetMyColor(Color color)
    {
        rend.material.color = color;
    }
}
