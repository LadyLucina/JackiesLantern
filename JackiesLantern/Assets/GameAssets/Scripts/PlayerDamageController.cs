using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M.
 * Details: This script  is designed to handle player interactions with a "Lurker" object in a Unity game. 
 * It introduces a damage and stun mechanic, along with the ability to destroy the "Lurker" object upon collision.
 */


public class PlayerDamageController : MonoBehaviour
{
    [Header("Damage & Stunned Stats")]
    [SerializeField] public int damageAmount = 10;
    [SerializeField] private float stunDuration = 1.5f;

    private HealthSystem healthSystem; //Reference to the current HealthSystem Script
    private bool isFrozen = false;
    private Vector3 initialPosition;
    private GameObject lastLurkerHit;

    private Rigidbody playerRigidbody; //Reference to the player's Rigidbody component

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        playerRigidbody = GetComponent<Rigidbody>(); //Get the player's Rigidbody
    }

    private void Update()
    {
        if (isFrozen)
        {
            stunDuration -= Time.deltaTime;
            if (stunDuration <= 0f)
            {
                //The stun duration has passed; unfreeze the player.
                isFrozen = false;
                Debug.Log("Player is no longer stunned");

                //Set the player's Rigidbody to kinematic to prevent unwanted forces.
                playerRigidbody.isKinematic = true;

                //Restore the player's initial position.
                transform.position = initialPosition;

                //Destroy the recently collided "Lurker" object.
                if (lastLurkerHit != null)
                {
                    Destroy(lastLurkerHit);
                    Debug.Log("Lurker destroyed");
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lurker") && !isFrozen)
        {
            healthSystem.damageHealth(damageAmount);
            Debug.Log("Player is Stunned");

            //Store the player's initial position.
            initialPosition = transform.position;

            //Store the recently collided "Lurker" object.
            lastLurkerHit = collision.gameObject;

            //Call the PlayerStun function to freeze the player.
            PlayerStun();
        }
    }

    private void PlayerStun()
    {
        isFrozen = true;
        stunDuration = 1.5f;
        Debug.Log("Player is Stunned");

        //Set the player's Rigidbody to kinematic during the stun to avoid the player ascending once unfrozen
        playerRigidbody.isKinematic = true;
    }
}