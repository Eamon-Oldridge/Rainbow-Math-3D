using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEditMenu : MonoBehaviour
{
    [SerializeField] ColorEditLegend optionsLegend;
    [SerializeField] GameObject optionsMenu;

    // Start is called before the first frame update
    public void BackToOptionsMenu()
    {
        optionsMenu.SetActive(true);
        optionsLegend.UpdateColors();
        gameObject.SetActive(false);
    }
}
