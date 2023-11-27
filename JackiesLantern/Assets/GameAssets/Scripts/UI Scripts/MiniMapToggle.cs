using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapToggle : MonoBehaviour
{
     public GameObject minimap; // Reference to the minimap GameObject

    void Start()
    {
        // Ensure that minimap is assigned in the Unity Editor
        if (minimap == null)
        {
            Debug.LogError("Minimap GameObject not assigned in the inspector!");
        }
    }

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
