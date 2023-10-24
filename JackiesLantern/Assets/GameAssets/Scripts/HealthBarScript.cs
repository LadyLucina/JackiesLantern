using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*  Original Author: Joshua G.
 *  Original HealthBarScript Details: This script is designed to manage and update a UI health bar in a Unity game. 
 *  This script uses a Slider component to display and control the health bar's visual representation.
 *  
 *  -----------------------------------------------------------------------------------------------------------------------------------
 *  Edited by: Stephanie M.
 *  Updated HealthBarScript Details: The new script now operates off of a sprite cycler that will change
 *  the sprite depending on the health percentage range the player falls into. 
 *  It will initially display the fullHealthSprite with a Health of 100 until damage occurs.
 *  
 *  Steps on how to use this script:
 *  --------------------------------
 *  Step 1: Within the Unity project, create a UI image and title it 'healthImage'
 *  Step 2: Create a UI text element using Legacy
 *  Step 3: Add the HealthBarScript to the UI Image
 *  Step 4: Assign the appropriate sprites to the fields
 *  Step 5: Assign the image titled 'healthImage' to the Health Image field
 *  Step 6: Attach the 'healthTXT' to the Health Text field
 *  Step 7: Attach the 'GameOverScene' to the Game Over UI field
 *  
 *  Lastly, ensure that the Health System script on the player object has the 'Health Bar' field updated to the 'healthImage' element
 *  -----------------------------------------------------------------------------------------------------------------------------------
 */

public class HealthBarScript : MonoBehaviour
{
    public GameObject gameOverUI; //Reference to the GAME OVER UI GameObject
    public Sprite fullHealthSprite;
    public Sprite mediumHealthSprite;
    public Sprite lowHealthSprite;
    public Image healthImage; //Reference to the UI image to display health
    public Text healthText; //Reference to the UI Text component to display health value

    private float maxHealth; //Store the maximum health

    private void Start()
    {
        gameOverUI.SetActive(false); //Ensure the GAME OVER UI is initially hidden
        maxHealth = 100;

        //Set the initial sprite to fullHealthSprite
        if (healthImage != null)
        {
            healthImage.sprite = fullHealthSprite;
        }

        //Set the initial health text to 100
        if (healthText != null)
        {
            healthText.text = "Health: 100";
        }
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void SetHealth(float health)
    {
        //Calculate health percentage
        float healthPercentage = health / maxHealth;

        //Change the sprite based on health percentage
        if (healthImage != null)
        {
            if (healthPercentage >= 0.7f)
            {
                healthImage.sprite = fullHealthSprite;
            }
            else if (healthPercentage >= 0.5f)
            {
                healthImage.sprite = mediumHealthSprite;
            }
            else
            {
                healthImage.sprite = lowHealthSprite;
            }
        }

        //Update the health text
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString();
        }

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

