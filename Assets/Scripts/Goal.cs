﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : VisualNumber
{
    Rigidbody body;
    public GameObject modelPositive;
    public GameObject modelNegative;

    public override void UpdateScale()
    {
        int absValue = Mathf.Abs(myValue);
        this.transform.localScale = new Vector3(Mathf.Pow(absValue, 0.33f), .1f, Mathf.Pow(absValue, 0.33f));
    }

    public override void SetRendModel()
    {
        if (myValue >= 0)
        {
            activeModel = modelPositive;
        }
        else
        {
            activeModel = modelNegative;
        }
    }

    public override void UpdateShape()
    {
        if(myValue >= 0)
        {
            modelNegative.SetActive(false);
            modelPositive.SetActive(true);
            activeModel = modelPositive;
        }
        else
        {
            modelPositive.SetActive(false);
            modelNegative.SetActive(true);
            activeModel = modelNegative;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        modelNegative.SetActive(false);
        modelPositive.SetActive(false);
        UpdateShape();
        UpdateScale();
        body = this.gameObject.GetComponent<Rigidbody>();
        SetMyColor(myValue); // calls SetColor() in activeModel's script
        body.isKinematic = false;
    }
}
