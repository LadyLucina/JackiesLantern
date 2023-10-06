using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Joshua G
//Purpose: This script will introduce a Healing mechanic.
//Once the player collides with "Candy Corn" it will heal the player and destroy the Candy Corn.

public class PlayerHealingController : MonoBehaviour
{
    [Header("Healing Stats")]
    [SerializeField] public int healAmount = 15;

    private HealthSystem healthSystem;
    private Rigidbody playerRigidbody;
    private GameObject absorbCandyCorn;


    void Start()
    {
        //references HealthSystem script
        healthSystem = GetComponent<HealthSystem>();
        //references player rigidbody
        playerRigidbody = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks if the gameobject has the "Candy Corn" tag
        if (other.gameObject.CompareTag("Candy Corn"))
        {
            //Stores Candy Corn object
            absorbCandyCorn = other.gameObject;
            //regenerates players health when the object is collided with
            healthSystem.regenHealth(healAmount);
            //check that it is working properly
            Debug.Log("Player is healing");
            //Object destroys once passed through
            Destroy(absorbCandyCorn);
    
        }
    }
}
