using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Stephanie M

public class PlayerDamageController : MonoBehaviour
{
    [Header("Damage & Stunned Stats")]
    [SerializeField] public int damageAmount = 10;
    [SerializeField] private float stunDuration = 1.5f; 

    private HealthSystem healthSystem; //Reference to the current HealthSystem script
    private bool isFrozen = false;
    private Vector3 initialPosition;
    private GameObject lastLurkerHit; //Store the recently collided "Lurker" object

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Update()
    {
        if (isFrozen)
        {
            stunDuration -= Time.deltaTime;
            if (stunDuration <= 0f)
            {
                // The stun duration has passed; unfreeze the player.
                isFrozen = false;
                Debug.Log("Player is no longer stunned"); //Console log to check if script is functioning as expected. 

                // Restore the player's initial position.
                transform.position = initialPosition;

                //Destroy the recently collided "Lurker" object.
                if (lastLurkerHit != null)
                {
                    Destroy(lastLurkerHit);
                    Debug.Log("Lurker destroyed");  //Console log to check if script is functioning as expected. 
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lurker") && !isFrozen)
        {
            healthSystem.damageHealth(damageAmount);
            Debug.Log("Player is Stunned"); //Console log to check if script is functioning as expected. 

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
        Debug.Log("Player is Stunned"); //Console log to check if script is functioning as expected. 
    }
}