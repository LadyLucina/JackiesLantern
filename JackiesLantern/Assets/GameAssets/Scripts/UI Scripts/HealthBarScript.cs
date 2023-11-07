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
    public Sprite[] healthSprites; //Array of sprite images for different health levels
    public Image healthImage; //Reference to the UI image to display health
    public Text healthText; //Reference to the UI Text component to display health value

    private float maxHealth; //Store the maximum health
    private float currentHealth; //Store the current health
    private int spriteIndex = 0; //Index to track the current sprite
    private bool shouldCycle = true; // Flag to control sprite cycling

    private void Start()
    {
        gameOverUI.SetActive(false); // Ensure the GAME OVER UI is initially hidden
        maxHealth = 100;
        currentHealth = maxHealth;

        // Set the initial sprite to full health
        if (healthImage != null && healthSprites.Length > 0)
        {
            healthImage.sprite = healthSprites[healthSprites.Length - 1];
        }

        // Set the initial health text to 100
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void SetHealth(float health)
    {
        // Ensure health is within the valid range
        currentHealth = Mathf.Clamp(health, 0, maxHealth);

        // Calculate health percentage
        float healthPercentage = currentHealth / maxHealth;

        // Determine the sprite index based on health percentage
        if (healthImage != null && healthSprites.Length > 0)
        {
            int newSpriteIndex = Mathf.FloorToInt(healthPercentage * (healthSprites.Length - 1));

            if (shouldCycle && (currentHealth <= 80 || newSpriteIndex != spriteIndex))
            {
                spriteIndex = newSpriteIndex;

                // Update the displayed sprite
                healthImage.sprite = healthSprites[spriteIndex];
            }
        }

        // Update the health text
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString("F0");
        }

        // Check if health reaches 0 or below
        if (currentHealth <= 0)
        {
            // Pause the game
            Time.timeScale = 0;

            // Display the GAME OVER UI
            gameOverUI.SetActive(true);
        }

        // Check if health is between 30 and 20 and stop cycling sprites
        if (currentHealth <= 30 && currentHealth > 20)
        {
            shouldCycle = false;
        }
    }

    public void DecreaseHealth(float amount)
    {
        float previousHealth = currentHealth;
        SetHealth(currentHealth - amount);

        // Check if the health decrease crossed a multiple of 20
        if (shouldCycle && Mathf.FloorToInt(previousHealth / 20) > Mathf.FloorToInt(currentHealth / 20) && currentHealth > 0)
        {
            // Change to the next sprite (cycle through the sprites)
            if (healthImage != null && healthSprites.Length > 0)
            {
                spriteIndex = (spriteIndex + 1) % healthSprites.Length;
                healthImage.sprite = healthSprites[spriteIndex];
            }
        }
    }
}
