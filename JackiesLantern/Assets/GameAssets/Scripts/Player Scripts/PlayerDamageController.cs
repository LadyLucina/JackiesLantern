using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M.
 * Details: This script  is designed to handle player interactions with a "Lurker" object in a Unity game. 
 * It introduces a damage and stun mechanic, along with the ability to destroy the enemy objects on collision. 
 */

public class PlayerDamageController : MonoBehaviour
{
    public DamageIndicator damageIndicator; //Reference to the DamageIndicator script
    private HealthSystem healthSystem; //Reference to the HealthSystem script
    public AudioSource audioSource; //Audio Source
    public AudioClip takenDamage; //AudioClip to play
    private ThirdPersonMovement thirdPersonMovement; //Reference to the ThirdPersonMovement script

    #region Enemy Damage Values
    private int lurkerDamage = 1;
    private int trapperDamage = 1;
    private int skullyDamage = 1;
    private int farmerDamage = 2;
    private int bossDamage = 3;
    private float initialStunDuration = 1.5f;
    #endregion  

    public bool isStunned = false; //Flag to control the player's stunned state
    public bool isFrozen = false; //Flag to control the player's frozen state
    private float stunTimer;

    private Vector3 initialPosition;
    private GameObject lastEnemyHit;
    private CharacterController characterController; //Reference to the CharacterController

    [Header("Respawn Settings")]
    public float respawnDelay = 3.0f; //Delay before the enemy respawns
    private bool isRespawning = false; //Flag to control the respawn state

    [Header("Damage Cooldown Setting")]
    [SerializeField] private float damageCooldown = 3.0f; //Damage cooldown timer
    private float lastDamageTime;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        stunTimer = initialStunDuration; //Initialize the stun timer
        characterController = GetComponent<CharacterController>();
        thirdPersonMovement = GetComponent<ThirdPersonMovement>();
    }

    private void Update()
    {
        if (isRespawning)
        {
            //Check if it's time to respawn
            if (Time.time - lastEnemyHitTime >= respawnDelay)
            {
                isRespawning = false;
                //Reactivate the enemy object
                //lastEnemyHit.SetActive(true);
                //Reset the timer
                lastEnemyHitTime = 0f;
            }
        }
        else if (isFrozen)
        {
            //Decrement the stun timer
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                //The stun duration has passed; unfreeze the player
                isFrozen = false;
                isStunned = false;

                //Reset the stun timer
                stunTimer = initialStunDuration;

                //Restore the players initial position
                transform.position = initialPosition;

                //Deactivate the collided enemy object
                if (lastEnemyHit != null)
                {
                    lastEnemyHit.SetActive(false);
                    lastEnemyHitTime = Time.time;
                    isRespawning = true;
                }

                //Re-enable the CharacterController
                characterController.enabled = true;
            }
        }
    }

    private float lastEnemyHitTime;
    private void OnTriggerEnter(Collider other)
    {
        if (!isFrozen && Time.time - lastDamageTime >= damageCooldown)
        {
            if (other.gameObject.CompareTag("Lurker") || other.gameObject.CompareTag("Trapper"))
            {
                DamageEnemy(lurkerDamage, other.gameObject);
            }
            else if (other.gameObject.CompareTag("Skully"))
            {
                DamageEnemy(skullyDamage, other.gameObject);
            }
            else if (other.gameObject.CompareTag("Farmer"))
            {
                DamageEnemy(farmerDamage, other.gameObject);
            }
            else if (other.gameObject.CompareTag("Boss"))
            {
                DamageEnemy(bossDamage, other.gameObject);
            }
        }
    }

    private void DamageEnemy(int damage, GameObject enemy)
    {
        if (!thirdPersonMovement.IsInvincible())
        {
            audioSource.PlayOneShot(takenDamage);

            //Damage the player's health using the HealthSystem
            healthSystem.damageHealth(damage);

            //Show the damage indicator when damage is taken
            damageIndicator.ShowDamageIndicator();

            //Store the player's initial position
            initialPosition = transform.position;

            //Store the recently collided enemy object
            lastEnemyHit = enemy;

            //Call the PlayerStun function to freeze the player
            PlayerStun();

            //Update the last damage time
            lastDamageTime = Time.time;

            //Set respawn state
            lastEnemyHitTime = Time.time;
            isRespawning = true;
        }
    }

    private void PlayerStun()
    {
        isStunned = true; //Set the stunned flag in PlayerDamageController
        isFrozen = true; //Freeze the player as well

        //Disable the character controller to avoid player movement during the stun
        characterController.enabled = false;
    }
}