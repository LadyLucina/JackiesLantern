using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*  Author: Joshua G.
 *  Details: This script is designed to manage and update a UI health bar in a Unity game. 
 *  This script uses a Slider component to display and control the health bar's visual representation.
 *  
 *  Edited by: Stephanie M.
 */

public class HealthBarScript : MonoBehaviour
{
    Slider healthSlider;

    private void Start()
    {
        healthSlider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int maxHealth)
    {

        //Testing debug log statement
        Debug.Log("Max Health Set: " + maxHealth);
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;

        //Testing debug log statement
        Debug.Log("Current Health Set: " + health);
    }
}
