using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VNModel : Colorable
{
    public Renderer rend;

    public override Color GetMyColor()
    {
        return rend.material.color;
    }

    public void SetActive(bool b)
    {
        gameObject.SetActive(b);
    }
}
