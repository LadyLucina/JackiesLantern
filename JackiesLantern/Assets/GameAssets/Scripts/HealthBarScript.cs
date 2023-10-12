using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*  Author: Joshua G.
 *  Details: This script is designed to manage and update a UI health bar in a Unity game. 
 *  This script uses a Slider component to display and control the health bar's visual representation.
 *  
 *  Edited by: Stephanie M.
 */

public class HealthBarScript : MonoBehaviour
{
    Slider healthSlider;
    public GameObject gameOverUI; //Reference to the GAME OVER UI GameObject

    private void Start()
    {
        healthSlider = GetComponent<Slider>();
        gameOverUI.SetActive(false); //Ensure the GAME OVER UI is initially hidden
    }

    public void SetMaxHealth(int maxHealth)
    {
        //healthSlider.maxValue = maxHealth;
        //healthSlider.value = maxHealth;

        //Testing debug log statement
        Debug.Log("Max Health Set: " + maxHealth);
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;

        //Testing debug log statement
        Debug.Log("Current Health Set: " + health);

        //Check if health reaches 0 or below
        if (health <= 0)
        {
            //Pause the game
            Time.timeScale = 0;

            //Display the GAME OVER UI
            gameOverUI.SetActive(true);
        }
    }
}