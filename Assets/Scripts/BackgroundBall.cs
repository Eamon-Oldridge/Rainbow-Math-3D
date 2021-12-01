using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBall : MonoBehaviour
{
    [SerializeField] private float xyBoundaries = 50;
    [SerializeField] private float zBoundary = 50;
    [SerializeField] private float camBoundary = 10;
    [SerializeField] private float maxVel = 10;
    [SerializeField] private float minVel = 3;

    private float[] vels = { 3, 3, 3 };
    Color myColor;
    Renderer rend;
    Rigidbody rb;
    
    private bool dVFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        rend = this.gameObject.GetComponent<Renderer>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        for (int i = 0; i < 3; i++)
        {
            vels[i] = Random.Range(minVel, maxVel);
            rb.velocity = new Vector3(vels[0], vels[1], vels[2]);
            myColor = new Color(Random.value, Random.value, Random.value);
            rend.material.color = myColor;
            //Debug.Log("my color is:" + rend.material.color.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.x < (0 - xyBoundaries) || this.gameObject.transform.position.x > xyBoundaries) { vels[0] = vels[0] * -Random.Range(0.5f, 2f); dVFlag = true; }
        if (this.gameObject.transform.position.y < (0 - xyBoundaries) || this.gameObject.transform.position.y > xyBoundaries) { vels[1] = vels[1] * -Random.Range(0.5f, 2f); dVFlag = true; }
        if (this.gameObject.transform.position.z < (camBoundary) || this.gameObject.transform.position.z > zBoundary) { vels[2] = vels[2] * -Random.Range(0.5f, 2f); dVFlag = true; }
        for (int i = 0; i < 3; i++) {
            //Debug.Log("preclamp: " + vels[i]);
            if (vels[i] < 0) { vels[i] = Mathf.Clamp(vels[i], -maxVel, -minVel); }
            else { vels[i] = Mathf.Clamp(vels[i], minVel, maxVel); }
            //Debug.Log("clamped: " + vels[i]);
        }
        
        if (dVFlag) {
            dVFlag = false;
            rb.velocity = new Vector3(vels[0], vels[1], vels[2]);
        }
        myColor = new Color(1 - (1 / (xyBoundaries * 2)) * (this.gameObject.transform.position.x + xyBoundaries), (1/(xyBoundaries * 2)) * (this.gameObject.transform.position.y + xyBoundaries), (1 / (zBoundary - camBoundary)) * (this.gameObject.transform.position.z - camBoundary));
        rend.material.color = myColor;
        //Debug.Log("color changed to " + myColor.ToString());
    }
}
