using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Author: Stephanie M.
 * Details: This script creates a visual cue that briefly appears on the screen
 * when the player takes damage.
 */

public class DamageIndicator : MonoBehaviour
{
    public Image damageImage; //Reference to the UI image for the damage indicator
    private bool showingDamageIndicator = false; //Controls if the damage indicator is currently displayed
    private float damageIndicatorDuration = 0.5f; //How long the damage indicator should stay visible
    private float damageIndicatorTimer = 0.0f; //Tracks the remaining time for displaying the damage indicator

    private void Start()
    {
        //Hides damage indicator UI when the game plays
        damageImage.enabled = false;
    }

    private void Update()
    {
        if(showingDamageIndicator)
        {
            //If the damage indicator is active, decrease the timer based on the elapsed time
            damageIndicatorTimer -= Time.deltaTime;

            //If the timer reaches zero, hide the UI
            if(damageIndicatorTimer <= 0f)
            {
                showingDamageIndicator = false;
                damageImage.enabled = false; //Hide UI element
            }
        }
    }

    public void ShowDamageIndicator()
    {
        showingDamageIndicator = true; //Indicate that damage is being shown
        damageIndicatorTimer = damageIndicatorDuration; //Reset the timer to specified duration
        damageImage.enabled = true; //Show the damage indicator UI 
    }

}
