using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : VisualNumber
{

    public GameObject modelPositive;
    public GameObject modelNegative;

    Rigidbody body;

    private List<GameObject> intersectingCubeList = new List<GameObject>();
    public int intersectCount = 0;
    private float previewingCombine = 0.5f; // 0.5 used as "null", otherwise it's always an int

    // Detect collisions with GameObjects with Colliders attached
    //       (Collision events are only sent if one of the colliders also has a non-kinematic rigidbody attached)
    void OnCollisionEnter(Collision collision)
    {
        // Check for a match with the "Mathable" tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Mathable")
        {
            // check to see if it's a cube
            GameObject other = collision.gameObject;
            if(other.GetComponent<Cube>() != null)
            {
                // add this collision to our list of ongoing collisions
                AddCollision(other);
                //Debug.Log(myValue + "'s intersectCount: " + intersectCount);
                // preview combination
                switch (FindOperator())
                {
                    default:
                        Debug.Log("FindOperator Defaulted");
                        break;
                    case "Addition":
                        int sumValue = other.GetComponent<Cube>().myValue + myValue;
                        // preview summation result
                        SetMyColor(sumValue);
                        other.GetComponent<Cube>().SetMyColor(sumValue);
                        previewingCombine = sumValue;
                        // Debug.Log("myValue: " + myValue + ", theirValue: " + other.GetComponent<Cube>().myValue + ", SUM = " + sumValue);
                        break;
                    case "Multiplication":
                        int productValue = other.GetComponent<Cube>().myValue * myValue;
                        // preview result
                        SetMyColor(productValue);
                        other.GetComponent<Cube>().SetMyColor(productValue);
                        previewingCombine = productValue;
                        // Debug.Log("myValue: " + myValue + ", theirValue: " + other.GetComponent<Cube>().myValue + ", PRODUCT = " + productValue);
                        break;
                }
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // remove from list of ongoing collisions
        RemoveCollision(collision.gameObject);
        //Debug.Log(myValue + "'s intersectCount: " + intersectCount);
        // end combination preview
        SetMyColor(myValue);
        previewingCombine = 0.5f;
    }

    private void RemoveCollision(GameObject other)
    {
        intersectingCubeList.Remove(other);
        intersectCount = intersectingCubeList.Count;
    }
    private void AddCollision(GameObject other)
    {
        intersectingCubeList.Add(other);
        intersectCount = intersectingCubeList.Count;
    }
    
    //given a mathable object we're colliding with, find what operation should be performed if combined
    private string FindOperator()
    {
        GameObject manager = GameObject.Find("GameManager");
        return manager.GetComponent<GameManager>().curOperator.name;
    }

    public void Combine()
    {
        GameObject other = intersectingCubeList.Find(x => x.GetComponent<Cube>() != null);
        // looks like we're set to combine. Find the operator and do it.
        switch (FindOperator())
        {
            default:
                Debug.Log("FindOperator Defaulted");
                break;
            case "Addition":
                Sum(other);
                break;
            case "Multiplication":
                Multiply(other);
                break;
        }
        UpdateShape();
    }

    public bool CombineCheck()
    {
        // check to make sure we're only intersecting with one other cube
        //Debug.Log("COMBINE CHECK -- ");
        //Debug.Log("My intersectCount: " + intersectCount);
        if (intersectingCubeList.Count == 1)
        {
            // get the cube from the list
            GameObject other = intersectingCubeList.Find(x => x.GetComponent<Cube>() != null);
            // check to make sure that cube isn't intersecting with any other cubes
            //Debug.Log(other.GetComponent<Cube>().myValue + "'s intersectCount: " + other.GetComponent<Cube>().intersectCount);
            if (other.GetComponent<Cube>().intersectCount == 1)
            {
                //Debug.Log("Combine Check returns true");
                return true;
            }
        }
        return false;
    }

    // multiplies this block with one it's intersecting with, returns new value
    private int Multiply(GameObject other)
    {
        // update our value
        myValue *= other.GetComponent<Cube>().myValue;
        SetMyColor(myValue);
        // remove it from our collision list
        RemoveCollision(other);
        //Debug.Log(myValue + "'s intersectCount: " + intersectCount);
        // destroy it
        Destroy(other);
        this.transform.position = new Vector3(this.transform.position.x, Mathf.Pow(Mathf.Abs(myValue), 0.33f) / 2 + 0.75f, this.transform.position.z);
        return myValue;
    }

    // sums this block with one it's intersecting with, returns new value
    private int Sum(GameObject other)
    {
        // update our value
        myValue += other.GetComponent<Cube>().myValue;
        SetMyColor(myValue);
        // remove it from our collision list
        RemoveCollision(other);
        //Debug.Log(myValue + "'s intersectCount: " + intersectCount);
        // destroy it
        Destroy(other);
        this.transform.position = new Vector3(this.transform.position.x, Mathf.Pow(Mathf.Abs(myValue), 0.33f) / 2 + 0.75f, this.transform.position.z);
        return myValue;
    }

    public override void UpdateScale()
    {
        int absValue = Mathf.Abs(myValue);
        this.transform.localScale = new Vector3(Mathf.Pow(absValue, 0.33f), Mathf.Pow(absValue, 0.33f), Mathf.Pow(absValue, 0.33f));
    }

    public override void SetRendModel()
    {
        if(myValue >= 0)
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
        if (myValue >= 0)
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
        modelPositive = this.gameObject.transform.GetChild(0).gameObject;
        modelNegative = this.gameObject.transform.GetChild(1).gameObject;
        SetRendModel();

        body = this.gameObject.GetComponent<Rigidbody>();
        modelNegative.SetActive(false);
        modelPositive.SetActive(false);
        UpdateShape();
        UpdateScale();
        // make it start sitting on the ground
        this.transform.position = new Vector3(this.transform.position.x, Mathf.Pow(Mathf.Abs(myValue), 0.33f) / 2, this.transform.position.z);
        SetMyColor(myValue); // calls SetColor() in activeModel's script
        body.mass = myValue;
        body.useGravity = true;
        body.isKinematic = false;
        body.maxDepenetrationVelocity = 2;
        body.maxAngularVelocity = 1.25f;
        body.drag = 1;
        //body.freezeRotation = true;
    }

        // Update is called once per frame
        void Update()
    {
        // update color
        if (previewingCombine != 0.5f)
        {
            float previewPeriod = 2.0f; // in seconds
            float curTime = Time.time % previewPeriod;
            float lerp = curTime;
            int previewValue = (int) previewingCombine;
            SetMyColor(Color.Lerp(PlayerPrefsValueToColor(myValue), PlayerPrefsValueToColor(previewValue), lerp));
            //other.GetComponent<Cube>().SetMyColor(Color.Lerp(GetColor(other.GetComponent<Cube>().myValue), GetColor(sumValue), lerp));
        }
        // update size based off value (area == value)
        UpdateScale();
        //set selection indicators (none for now) ((not working anyways?))
    }
}
