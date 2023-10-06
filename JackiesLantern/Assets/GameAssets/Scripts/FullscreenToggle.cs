using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Author: Stephanie M.
 * Details: A UI button element that allows the player to toggle between windowed and fullscreen mode.
 */

public class FullscreenToggle : MonoBehaviour
{
    private Button toggleFullscreenButton;
    public bool isFullscreen;

    void Start()
    {
        //Find the UI button by its name
        toggleFullscreenButton = GameObject.Find("ToggleFS").GetComponent<Button>();

        //Attach the ToggleFullscreen method to the button's click event
        toggleFullscreenButton.onClick.AddListener(ToggleFullscreen);

    }

    public void ToggleFullscreen()
    {
        //Switch to windowed mode
        if (Screen.fullScreen)
        {
            Screen.fullScreen = false; 
        }

        //Switch to fullscreen mode
        else
        {
            Screen.fullScreen = true; 
        }
    }
}


