using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Author: Joshua G
Purpose: This script is a toggle for the minimap
Press M to toggle it on and off
*/

public class MiniMapToggle : MonoBehaviour
{
     public GameObject minimap; // Reference to the minimap GameObject

    void Update()
    {
        // Check for 'M' key press
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMinimap();
        }
    }

    void ToggleMinimap()
    {
        // Toggle the active state of the minimap GameObject
        minimap.SetActive(!minimap.activeSelf);

        // Log the state change
        if (minimap.activeSelf)
        {
            Debug.Log("Minimap enabled!");
        }
        else
        {
            Debug.Log("Minimap disabled!");
        }
    }
}
