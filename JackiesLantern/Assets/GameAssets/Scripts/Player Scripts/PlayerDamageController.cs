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

    [Header("Damage & Stunned Stats")]
    [SerializeField] private int lurkerDamage = 10;
    [SerializeField] private int trapperDamage = 15;
    [SerializeField] private int farmerDamage = 20;
    [SerializeField] private int bossDamage = 25;
    [SerializeField] private float initialStunDuration = 1.5f;

    public bool isStunned = false; //Flag to control player's stunned state
    public bool isFrozen = false; //Flag to control player's frozen state
    private float stunTimer;

    private Vector3 initialPosition;
    private GameObject lastEnemyHit;
    private CharacterController characterController; //Reference to the CharacterController

    //public float destroyTime = 0.5f;
    //Note: Delays time before enemy is destroyed so animation can play. Not in use right now. -Mya

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        stunTimer = initialStunDuration; //Initialize the stun timer

        characterController = GetComponent<CharacterController>();

        thirdPersonMovement = GetComponent<ThirdPersonMovement>();
    }

    private void Update()
    {
        if (isFrozen)
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

                //Restore the player's initial position
                transform.position = initialPosition;

                //Destroy the recently collided enemy object
                if (lastEnemyHit != null)
                {
                    Destroy(lastEnemyHit);
                }

                //Re-enable the CharacterController
                characterController.enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isFrozen)
        {
            if (other.gameObject.CompareTag("Lurker"))
            {
                DamageEnemy(lurkerDamage, other.gameObject);
            }
            else if (other.gameObject.CompareTag("Trapper"))
            {
                DamageEnemy(trapperDamage, other.gameObject);
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
        //Checks if the player is invincible
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
        }
    }

    private void PlayerStun()
    {
        isStunned = true; //Set the stunned flag in PlayerDamageController
        isFrozen = true; //Freeze the player as well

        //Disable character controller to avoid player movement during the stun
        characterController.enabled = false;
    }
}