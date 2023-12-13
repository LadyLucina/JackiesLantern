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
    private int lurkerDamage = 1;   //Value amount the Lurker will do for damage
    private int trapperDamage = 1;  //Value amount the Trapper will do for damage
    private int skullyDamage = 1;   //Value amount the Skulls will do for damage
    private int farmerDamage = 2;   //Value amount the Farmer will do for damage
    private int bossDamage = 2;     //Value amount the Boss wil ldo for damage
    #endregion  

    [Header("Respawn Settings")]
    public float respawnDelay = 3.0f; //Delay before the enemy respawns
    public bool isRespawning = false; //Flag to control the respawn state

    [Header("Damage Cooldown Setting")]
    [SerializeField] private float damageCooldown = 3.0f; //Damage cooldown timer
    private float lastDamageTime; //Tracks the time when the player last took damage.

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>(); //Reference to the HealthSystem component on the player.
        thirdPersonMovement = GetComponent<ThirdPersonMovement>(); //Reference to the ThirdPersonMovement component on the player.
    }

    private void Update()
    {
        //Check if the player is currently respawning.
        if (isRespawning)
        {
            //Check if enough time has passed since the last enemy hit for the respawn delay.
            if (Time.time - lastEnemyHitTime >= respawnDelay)
            {
                isRespawning = false; //Stop respawning.
                lastEnemyHitTime = 0f; //Reset the last enemy hit time.
            }
        }
    }

    private float lastEnemyHitTime; //Tracks the time when the player was last hit by an enemy.

    //Called when the player enters the trigger zone of an enemy.
    private void OnTriggerEnter(Collider other)
    {
        //Check if enough time has passed since the last damage for the damage cooldown.
        if (Time.time - lastDamageTime >= damageCooldown)
        {
            //Check the tag of the colliding object to determine the enemy type and apply damage accordingly.
            if (other.gameObject.CompareTag("Lurker"))
            {
                DamageEnemy(lurkerDamage, other.gameObject);
            }
            else if (other.gameObject.CompareTag("Trapper"))
            {
                DamageEnemy(trapperDamage, other.gameObject);
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

    //Applies damage to the player based on the enemy type.
    private void DamageEnemy(int damage, GameObject enemy)
    {
        if (!thirdPersonMovement.IsInvincible())
        {
            audioSource.PlayOneShot(takenDamage); //Play the sound effect for taking damage.

            //Damage the player's health using the HealthSystem.
            healthSystem.damageHealth(damage);

            //Show the damage indicator when damage is taken.
            damageIndicator.ShowDamageIndicator();

            //Update the last damage time.
            lastDamageTime = Time.time;

            //Set respawn state.
            lastEnemyHitTime = Time.time;
            isRespawning = true;
        }
    }
} 