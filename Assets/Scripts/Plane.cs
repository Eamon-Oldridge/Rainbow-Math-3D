using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    // ##### VVV   Color change variables   VVV #####
    private static Color darkBlue = new Vector4(.05f, .231f, .588f, 1);
    private static Color yellow = new Vector4(1f, .584f, .004f, 1);
    private static Color babyBlue = new Vector4(.604f, .714f, .929f, 1);
    private static Color purple = new Vector4(.470f, .247f, .416f, 1);

    private static Color blueL = new Vector4(.75f, .75f, 1f, 1);
    private static Color redL = new Vector4(1f, .75f, .75f, 1);
    private static Color greenL = new Vector4(.75f, 1f, .75f, 1);
    private static int numberOfColors = 3;

    Color colorStart = blueL;
    Color colorMiddle = greenL;
    Color colorEnd = redL;
    float duration = 32.0f;
    // ##### ^^^   Color change variables   ^^^ #####

    Renderer rend;

    void Start()
    {
        rend = this.GetComponent<Renderer>();
    }

    void Update()
    {
        
        // ##### VVV   Make the game field change colors   VVV #####
        float period = duration / numberOfColors;
        float curTime = Time.time % duration;

        if(curTime < (period)){ // beggining
            float lerp = curTime / period;
            rend.material.color = Color.Lerp(colorStart, colorMiddle, lerp);
        }
        else if(curTime > (2 * period)) { // end
            float lerp = curTime % period / period;
            rend.material.color = Color.Lerp(colorEnd, colorStart, lerp);
        }
        else { // middle
            float lerp = curTime % period / period;
            rend.material.color = Color.Lerp(colorMiddle, colorEnd, lerp);
        }
        // ##### ^^^   Make the game field change colors   ^^^ #####
        
    }
}