using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    Slider mySlider;

    // Start is called before the first frame update
    void Start()
    {
        mySlider = this.gameObject.GetComponent<Slider>();
        mySlider.value = PlayerPrefs.GetFloat("masterVolume");
    }

    public void UpdateVolumeSettings()
    {
        PlayerPrefs.SetFloat("masterVolume", mySlider.value);
    }
}
