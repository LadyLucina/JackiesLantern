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
    public AudioSource audioSource;
    public AudioClip eatingCandy; //Audio Source

    [Header("Healing Stats")]
    public int healAmount = 1;

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
                audioSource.PlayOneShot(eatingCandy);
                Debug.Log("Player is healing");
                //Object destroys once passed through
                Destroy(absorbCandyCorn);
            }
            else
            {
                Debug.Log("Cannot add. Max health exceeded!");
            }

        }
    }
}
