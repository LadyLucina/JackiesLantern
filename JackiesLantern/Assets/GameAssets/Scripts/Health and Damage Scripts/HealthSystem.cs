using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Joshua G.
 * Details: This script is responsible for managing the health-related functionality of a game character or entity.
 * This script serves as the central component for managing the health of a character or entity in a game, 
 * ensuring that health values are correctly adjusted and reflected in the associated health bar.
 * 
 * Edited by: Stephanie M.
 */

public class HealthSystem : MonoBehaviour
{
    public HealthBarScript healthBar; //Reference to the Health Bar Script

    //Serialized fields for initial health and max health values
    [SerializeField] int initialHealth = 100;
    [SerializeField] int initialMaxHealth = 100;

    //Initialize variables
    int currentHealth;
    int currentMaxHealth;

    //Properties for health value
    public int Health
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;

            //Updates the health bar whenever health changes
            healthBar.SetHealth(currentHealth);
        }
    }

    public int MaxHealth
    {
        get { return currentMaxHealth; }
        set
        {
            //Ensure that the max health doesn't exceed the default value (100)
            currentMaxHealth = Mathf.Min(value, 100);

            //Update the health bar whenever max health changes
            healthBar.SetMaxHealth(currentMaxHealth);
        }
    }

    //Method to initialize health system
    void Start()
    {
        currentHealth = initialHealth;
        MaxHealth = initialMaxHealth;
    }

    public void damageHealth(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;

            //Update the health bar when taking damage
            healthBar.SetHealth(currentHealth);
        }
    }

    public void regenHealth(int healAmount)
    {
        if (currentHealth < currentMaxHealth)
        {
            currentHealth += healAmount;

            //Ensure the health does not exceed max health
            currentHealth = Mathf.Min(currentHealth, currentMaxHealth);

            //Update the health bar when regenerating health
            healthBar.SetHealth(currentHealth);
        }
    }
}