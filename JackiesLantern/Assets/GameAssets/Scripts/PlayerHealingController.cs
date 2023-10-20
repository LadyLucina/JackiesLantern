using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Joshua G
//Purpose: This script will introduce a Healing mechanic.
//Once the player collides with "Candy Corn" it will heal the player and destroy the Candy Corn.
//Edited by: Stephanie M.

public class PlayerHealingController : MonoBehaviour
{
    public DamageIndicator healIndicator; //Reference to the DamageIndicator script 

    [Header("Healing Stats")]
    [SerializeField] public int healAmount = 15;

    HealthSystem healthSystem;
    private GameObject absorbCandyCorn;


    void Start()
    {
        //references HealthSystem script
        healthSystem = GetComponent<HealthSystem>();

    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks if the gameobject has the "Candy Corn" tag
        if (other.gameObject.CompareTag("Candy Corn"))
        {
            //Stores Candy Corn object
            absorbCandyCorn = other.gameObject;

            //Check if healing is needed (currentHealth < currentMaxHealth)
            if (healthSystem.Health < healthSystem.MaxHealth)
            {
                healIndicator.ShowHealIndicator();
                //Regenerates player's health when the object is collided with
                healthSystem.regenHealth(healAmount);
                Debug.Log("Player is healing");
            }
            else
            {
                Debug.Log("Cannot add. Max health exceeded!");
            }

            //Object destroys once passed through
            Destroy(absorbCandyCorn);
        }
    }
}
