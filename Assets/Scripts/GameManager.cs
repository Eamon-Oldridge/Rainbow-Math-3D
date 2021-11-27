using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*  this class manages
            clicking and moving blocks

        planned features:
            update UI
            Handle levels
            handle scenes?
    */
    private bool isMouseDragging = false;
    private GameObject target;
    private GameObject playingField;
    private float targetOriginalY;
    public OperationButton curOperator;
    public OperationButton additionButtonScript;
    public OperationButton multiplicationButtonScript;
    public GameObject levelManagerObject;
    private LevelManager levelManager;
    //[SerializeField] private int curLevel = 1; // set to initial level
    private List<OperationButton> allOperationButtons = new List<OperationButton>();

    // this one's bitmask calculations were working but i might as well manually set it
    int defaultMask = 1;
    // I'M PISSED i have no idea why manually setting it here is necessary i tried copying the exact code that i had working in LayerDebug here and it was totally messed up.
    int PFMask = 256;

    // Start is called before the first frame update
    void Start()
    {
        playingField = GameObject.Find("Plane");
        /*if(playingField != null)
        {
            Debug.Log("we found a playingfield");
        }
        else
        {
            Debug.Log("no playingfield found!");
        }*/
        additionButtonScript = GameObject.Find("Addition").GetComponent<OperationButton>();
        multiplicationButtonScript = GameObject.Find("Multiplication").GetComponent<OperationButton>();
        allOperationButtons.Add(additionButtonScript);
        allOperationButtons.Add(multiplicationButtonScript);
        this.curOperator = additionButtonScript;
        // make a mask for the raycast so that it doesn't hit the playfield
        //  layermasks are confusing. Check this out: https://answers.unity.com/questions/8715/how-do-i-use-layermasks.html
        // this absolute LAD helped so much https://answers.unity.com/questions/1164722/raycast-ignore-layers-except.html
        /*
        Debug.Log("defaultMask set to: " + defaultMask);
        Debug.Log("PFMask set to: " + PFMask);
        */
        levelManager = levelManagerObject.GetComponent<LevelManager>();
        /*
        if (levelManager != null)
        {
            Debug.Log("levelManager assigned");
        }
        */
        levelManager.setAdditionButton(additionButtonScript.gameObject);
        levelManager.setMultiplicationButton(multiplicationButtonScript.gameObject);
        levelManager.LoadLevel(0);
        levelManager.LoadLevel(StaticDataTracker.curLevel);
        isMouseDragging = false;
        foreach (OperationButton button in allOperationButtons)
        {
            button.UpdateDisplay();
        }

    }

    // returns the Cube GameObject hit by a raycast from the camera, null if miss
    GameObject ReturnClickedCube(out RaycastHit hit)
    {
        GameObject targetObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // cast ray only for collision with default layer
        if (Physics.Raycast(ray.origin, Vector3.Normalize(ray.direction), out hit, 40.0f, defaultMask))
        {
            targetObject = hit.collider.gameObject;
            targetOriginalY = targetObject.transform.position.y;
        }
        return targetObject;
    }

    public void OnOperationButtonPress(OperationButton operation)
    {
        this.curOperator = operation;
        //Debug.Log("The operation is now: " + curOperator.name);
    }

    // Update is called once per frame
    void Update()
    {
        // if we're clicking
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            // get the first cube hit by our click
            target = ReturnClickedCube(out hitInfo);
            if (target != null)
            {
                //Debug.Log("target hit!");
                isMouseDragging = true;
                //Debug.Log("our cube's position :" + target.transform.position);
                //Debug.Log("The object's selction is " + objSelected);
            }
            /*else
            {
                Debug.Log("no target hit");
            }*/
        }

        // if we're not clicking
        if (Input.GetMouseButtonUp(0))
        {
            // if we have something selected
            if(target != null)
            {
                // set it to its xz position and drop to originalY
                target.transform.position = new Vector3(target.transform.position.x, targetOriginalY, target.transform.position.z);
                if(target.GetComponent<Cube>() != null)
                {
                    // if its a cube, combine check (CombineCheck will combine them if appropriate
                    if (target.GetComponent<Cube>().CombineCheck())
                    {
                        //Debug.Log("Start Combining... curOperator is " + curOperator.name);
                        if (curOperator.count > 0)
                        {
                            curOperator.count--;
                            curOperator.UpdateDisplay();
                            target.GetComponent<Cube>().Combine();
                        }
                        /*else
                        {
                            Debug.Log("curOperator count is too low to combine.");
                        }*/
                    }
                }
            }
            isMouseDragging = false;
        }

        if (isMouseDragging)
        {
            // cast a ray from the camera to the playfield, and find the intersect value. Then set that block's position to that value.
            Vector3 pointOnPlayfield;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit PFHit;
            // cast ray that only collides with Playfield layer
            
            //Debug.Log("PFMask set to: " + PFMask);
            if (Physics.Raycast(ray.origin, Vector3.Normalize(ray.direction), out PFHit, 40.0f, PFMask))
            {
                pointOnPlayfield = PFHit.point;
                target.transform.position = new Vector3(pointOnPlayfield.x, targetOriginalY + 0.75f, pointOnPlayfield.z);
                //Debug.Log("plane intersect at: " + pointOnPlayfield);
            }
            else
            {
                //Debug.Log("no plane was intersected");
            }
        } // end isMouseDragging

        if (Input.GetKeyDown(KeyCode.DownArrow)) // reload current level
        {
            levelManager.LoadLevel(StaticDataTracker.curLevel);
            isMouseDragging = false;
            foreach (OperationButton button in allOperationButtons)
            {
                button.UpdateDisplay();
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // next level
        {
            StaticDataTracker.curLevel++;
            if (!levelManager.LoadLevel(StaticDataTracker.curLevel))
            {
                StaticDataTracker.curLevel--;
                levelManager.LoadLevel(StaticDataTracker.curLevel);
            }
            isMouseDragging = false;
            foreach (OperationButton button in allOperationButtons)
            {
                button.UpdateDisplay();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // prev level
        {
            StaticDataTracker.curLevel--;
            if (!levelManager.LoadLevel(StaticDataTracker.curLevel))
            {
                StaticDataTracker.curLevel++;
                levelManager.LoadLevel(StaticDataTracker.curLevel);
            }
            isMouseDragging = false;
            foreach (OperationButton button in allOperationButtons)
            {
                button.UpdateDisplay();
            }
        }
    } // end update
}
