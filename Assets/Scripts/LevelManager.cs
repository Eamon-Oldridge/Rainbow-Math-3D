using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject gamePiece; // assigned in inspector

    private float columnSpacing = 1.75f; // the spacing for spawning the blocks with the level factory
    private float rowSpacing = 1.25f; // ^^
    private float goalSpacing = 1f; // how far away from the side the goal is
    private float verticalBuffer = 6f; // ?
    private float horizontalBuffer = 1f; // ?
    int PFLength = 20; // the length of the playfield
    private List<GameObject> activePieces = new List<GameObject>();
    private GameObject goal;
    private GameObject additionButton;
    private GameObject multiplicationButton;

    // Start is called before the first frame update
    void Start()
    {
        goal = GameObject.Find("Goal"); // should this just be assigned?
        /*if (goal != null)
        {
            Debug.Log("Goal found");
        }
        else
        {
            Debug.Log("ERROR: Goal not found");
        }*/
        additionButton = GameObject.Find("Addition");
        multiplicationButton = GameObject.Find("Multiplication");
    }

    private List<T> CreateList<T>(params T[] values)
    {
        return new List<T>(values);
    }

    public void ClearLevel()
    {
        goal.GetComponent<Goal>().myValue = 1;
        goal.transform.position = new Vector3(-8, .05f, 0);
        goal.GetComponent<Goal>().UpdateFeatures();

        additionButton.GetComponent<OperationButton>().count = 0;
        multiplicationButton.GetComponent<OperationButton>().count = 0;

        foreach(GameObject block in activePieces)
        {
            Destroy(block);
        }
    }

    // Makes a level using an array of block values, and three ints for the goal, adds, and mults
    public void MakeLevel(int[] blocks, int goalValue, int additions, int multiplications)
    {
        //Debug.Log("Making level...");
        //List<int> blocks = CreateList(blockArray);

        goal.GetComponent<Goal>().myValue = goalValue;
        int absGoal = Mathf.Abs(goalValue);
        //Debug.Log("   value assigned: " + goalValue);
        goal.transform.position = new Vector3(goalSpacing - (PFLength / 2) + (Mathf.Pow(absGoal, 0.33f) / 2), .05f, 0);
        //Debug.Log("   transform set: " + goal.transform.position);
        goal.GetComponent<Goal>().UpdateFeatures();
        //Debug.Log("   features updated");

        additionButton.GetComponent<OperationButton>().count = additions;
        //Debug.Log("Addition assigned");
        multiplicationButton.GetComponent<OperationButton>().count = multiplications;
        //Debug.Log("Multiplication assigned");

        //blocks.Sort();
        /*
         *  origin  (-x) ->
         *  y
         *  |
         *  V
         */
        Vector3 origin = new Vector3((PFLength/2) - horizontalBuffer, 0, -(PFLength/2) + verticalBuffer);
        int biggestInCol = 0;
        // curY is the bottom of the last block
        float curY = 0;
        // curX is centered using the first block of every column.
        float curX = 0;
        bool newColFlag = true;
        //Debug.Log("Making blocks...");
        // make all of the blocks
        foreach (int value in blocks)
        {
            GameObject newBlock = Instantiate(gamePiece);
            activePieces.Add(newBlock);
            //Debug.Log("   cube instantiated");
            newBlock.GetComponent<Cube>().myValue = value;
            //Debug.Log("      cube value set: " + value);
            int absValue = Mathf.Abs(value);
            float mySideLength = Mathf.Pow(absValue, 0.33f);

            // check to see if it would send us over the edge
            //          --curY is the bottom of the last block. So add spacing, add the sidelength, then see if we're more than the playfield length - another spacing.
            if ((curY + rowSpacing + mySideLength) > (PFLength - rowSpacing - verticalBuffer))
            {
                // the block would be too far down.
                newColFlag = true;
            }
            if (newColFlag == true)
            {
                // Start a new column.
                curY = 0;
                // set the center of this column to be the center of this block,
                //      spaced so that there will be colSpacing between the edges of this block and the ones to the left and right.
                //      last col center + half of a sidelength of the largest block in that col + spacer + half of a sidelength of the biggest block in new col (current block)
                curX = curX - (Mathf.Pow(biggestInCol, 0.33f) / 2) - columnSpacing - (mySideLength / 2);
                biggestInCol = absValue;
                newColFlag = false;
            }
            // update curY
            curY = curY + rowSpacing + mySideLength;

            // set position of new block
            Vector3 offset = new Vector3(curX, (mySideLength / 2), curY - (mySideLength / 2));
            /*
            Debug.Log("offset: " + offset);
            Debug.Log("offset + origin: " + (offset + origin));
            Debug.Log("mySideLength: " + mySideLength);
            Debug.Log("curX: " + curX);
            Debug.Log("curY: " + curY);
            */
            newBlock.transform.position = origin + offset;
        }
    }

    // returns false if level fails to load or is out of bounds
    public bool LoadLevel(int level)
    {
        int[] blocks;
        int goal;
        int adds;
        int mults;

        ClearLevel();

        switch (level)
        {
            /*  Template:
            case _:
                blocks = new int[] { _ };
                goal = _;
                adds = _;
                mults = _;
                break;
             */
            default:
                return false;
            case 0:
                blocks = new int[] { };
                goal = 1;
                adds = 0;
                mults = 0;
                break;
            case 1:
                blocks = new int[] { 2, 1 };
                goal = 3;
                adds = 1;
                mults = 0;
                break;
            case 2:
                blocks = new int[] { 4, 3 };
                goal = 12;
                adds = 0;
                mults = 1;
                break;
            case 3:
                blocks = new int[] { 3, 1, 1 };
                goal = 6;
                adds = 1;
                mults = 1;
                break;
            case 4:
                blocks = new int[] { 2, 2, 1 };
                goal = 4;
                adds = 1;
                mults = 1;
                break;
            case 5:
                blocks = new int[] { 4, 3, 2 };
                goal = 11;
                adds = 1;
                mults = 1;
                break;
            case 6:
                blocks = new int[] { 3, 2, 1, 1 };
                goal = 6;
                adds = 2;
                mults = 1;
                break;
            case 7:
                blocks = new int[] { 8, -7 };
                goal = 1;
                adds = 1;
                mults = 0;
                break;
            case 8:
                blocks = new int[] { 5, -3 };
                goal = 2;
                adds = 1;
                mults = 0;
                break;
            case 9:
                blocks = new int[] { 10, -1 };
                goal = -10;
                adds = 0;
                mults = 1;
                break;
            case 10:
                blocks = new int[] { 3, -2 };
                goal = -6;
                adds = 0;
                mults = 1;
                break;
            case 11:
                blocks = new int[] { -1, -3 };
                goal = 3;
                adds = 0;
                mults = 1;
                break;
            case 12:
                blocks = new int[] { -2, -4 };
                goal = 8;
                adds = 0;
                mults = 1;
                break;
            case 13:
                blocks = new int[] { 1, -1, -9 };
                goal = 10;
                adds = 1;
                mults = 1;
                break;
            case 14:
                blocks = new int[] { 1, -2, -5 };
                goal = 5;
                adds = 1;
                mults = 1;
                break;
            case 15:
                blocks = new int[] { 2, -3, -5 };
                goal = 9;
                adds = 1;
                mults = 1;
                break;
            case 16:
                blocks = new int[] { 2, -1, -7 };
                goal = -7;
                adds = 1;
                mults = 1;
                break;
            case 17:
                blocks = new int[] { 4, 2, 1, -1, -1 };
                goal = 10;
                adds = 3;
                mults = 1;
                break;
            case 18:
                blocks = new int[] { 4, 3, 2, 1, 1, -3 };
                goal = 9;
                adds = 3;
                mults = 2;
                break;
            case 19:
                blocks = new int[] { 2, 2, 1, -3, -5 };
                goal = 3;
                adds = 2;
                mults = 2;
                break;
        }
        MakeLevel(blocks, goal, adds, mults);
        return true;
    }
}
