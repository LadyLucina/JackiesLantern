using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Author: Stephanie M.
 * Details: A UI button element that allows the player to toggle between windowed and fullscreen mode.
 * It will update the buttons text based on current screen mode.
 */

public class FullscreenToggle : MonoBehaviour
{
    private Button toggleFullscreenButton;

    void Start()
    {
        //Find the UI button by its name
        toggleFullscreenButton = GameObject.Find("ToggleFS").GetComponent<Button>();

        //Attach the ToggleFullscreen method to the button's click event
        toggleFullscreenButton.onClick.AddListener(ToggleFullscreen);

        //Set the initial text of the button based on the current fullscreen mode
        UpdateButtonLabel();
    }
    public void ToggleFullscreen()
    {
        //Toggle fullscreen mode
        if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        //Update the button label to reflect the current fullscreen mode
        UpdateButtonLabel();
    }

    void UpdateButtonLabel()
    {
        //Update the button's text to reflect the current fullscreen mode
        if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            toggleFullscreenButton.GetComponentInChildren<Text>().text = "Go Fullscreen";
        }
        else
        {
            toggleFullscreenButton.GetComponentInChildren<Text>().text = "Exit Fullscreen";
        }
    }
}