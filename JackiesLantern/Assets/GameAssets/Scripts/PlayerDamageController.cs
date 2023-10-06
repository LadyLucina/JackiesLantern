using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M.
 * Details: This script  is designed to handle player interactions with a "Lurker" object in a Unity game. 
 * It introduces a damage and stun mechanic, along with the ability to destroy the "Lurker" object upon collision.
 */

public class PlayerDamageController : MonoBehaviour
{
    public DamageIndicator damageIndicator; //Reference to the DamageIndicator script

    [Header("Damage & Stunned Stats")]
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float initialStunDuration = 1.5f;

    private HealthSystem healthSystem;
    public bool isStunned = false; //Flag to control player's stunned state
    public bool isFrozen = false; //Flag to control player's frozen state
    private float stunTimer;

    private float currentStunDuration;
    private Vector3 initialPosition;
    private GameObject lastLurkerHit;
    private Rigidbody playerRigidbody;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        playerRigidbody = GetComponent<Rigidbody>();
        currentStunDuration = initialStunDuration; //Initialize the stun duration.
        stunTimer = initialStunDuration; //Initialize the stun timer
    }

    private void Update()
    {
        if (isFrozen)
        {
            //Decrement the stun timer
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                //The stun duration has passed; unfreeze the player.
                isFrozen = false;
                isStunned = false;
                Debug.Log("Player is no longer stunned");

                //Reset the stun timer
                stunTimer = initialStunDuration;

                //Reset the player's Rigidbody to allow physics to affect it.
                playerRigidbody.isKinematic = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lurker") && !isFrozen)
        {
            healthSystem.damageHealth(damageAmount);
            Debug.Log("Player is Stunned");

            //Show the damage indicator when damage is taken
            damageIndicator.ShowDamageIndicator();

            //Store the player's initial position.
            initialPosition = transform.position;

            //Store the recently collided "Lurker" object.
            lastLurkerHit = other.gameObject;

            //Call the PlayerStun function to freeze the player.
            PlayerStun();
        }
    }

    private void PlayerStun()
    {
        isStunned = true; //Set the stunned flag in PlayerDamageController
        isFrozen = true; //Freeze the player as well
        Debug.Log("Player is Stunned");

        //Set the player's Rigidbody to kinematic during the stun to avoid unwanted forces.
        playerRigidbody.isKinematic = true;
    }
}