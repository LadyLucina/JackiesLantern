using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Author: Stephanie M.
 * Details: This script is designed to adjust the in game audio by working with a UI slider.
 */

public class VolumeController : MonoBehaviour
{
    //Reference to the UI Slider
    public Slider volumeSlider; 

    private AudioSource audioSource;

    //Key to store volume setting in PlayerPrefs
    private const string VolumeKey = "Volume"; 

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); 

        //Load the volume setting from PlayerPrefs or use a set default value
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.5f);
        volumeSlider.value = savedVolume;
        if (audioSource != null)
        {
            audioSource.volume = savedVolume;
        }

        //Add a listener to the slider to update the volume when it changes
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float volume)
    {
        //Update the audio source volume when the slider value changes
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }

        //Save the volume setting to PlayerPrefs
        PlayerPrefs.SetFloat(VolumeKey, volume);
    }
}
