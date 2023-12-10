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
    #endregion  

    [Header("Respawn Settings")]
    public float respawnDelay = 3.0f; //Delay before the enemy respawns
    public bool isRespawning = false; //Flag to control the respawn state

    [Header("Damage Cooldown Setting")]
    [SerializeField] private float damageCooldown = 3.0f; //Damage cooldown timer
    private float lastDamageTime;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        thirdPersonMovement = GetComponent<ThirdPersonMovement>();
    }

    private void Update()
    {
        if (isRespawning)
        {
            if (Time.time - lastEnemyHitTime >= respawnDelay)
            {
                isRespawning = false;
                lastEnemyHitTime = 0f;
            }
        }
    }

    private float lastEnemyHitTime;
    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastDamageTime >= damageCooldown)
        {
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

    private void DamageEnemy(int damage, GameObject enemy)
    {
        if (!thirdPersonMovement.IsInvincible())
        {
            audioSource.PlayOneShot(takenDamage);

            //Damage the player's health using the HealthSystem
            healthSystem.damageHealth(damage);

            //Show the damage indicator when damage is taken
            damageIndicator.ShowDamageIndicator();

            //Update the last damage time
            lastDamageTime = Time.time;

            //Set respawn state
            lastEnemyHitTime = Time.time;
            isRespawning = true;
        }
    }
} 