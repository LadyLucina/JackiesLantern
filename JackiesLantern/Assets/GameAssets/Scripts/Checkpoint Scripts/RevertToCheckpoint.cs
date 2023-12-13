using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Joshua Guerrero
 * Purpose: This script will have the player respawn at the most recent checkpoint
 * when the player is at 0 health
 */
public class RevertToCheckpoint : MonoBehaviour
{
    #region Inspector Variables
    private HealthSystem healthSystem; //Reference to the HealthSystem script
    private Checkpoint checkpoint;  //Reference to the Checkpoint script
    public Transform playerTransform;   //Reference to the playerTransform obkect
    #endregion

    void Start()
    {
        healthSystem = GetComponent<HealthSystem>(); //Grab component of the HealthSystem script
        checkpoint = GetComponent<Checkpoint>();    //Grab component of the Checkpoint script
    }

    private void Update()
    {
        //If the players health is less than or equal to 1, do the following:
        if (healthSystem.currentHealth <= 1)
        {
            //Respawn the player based on the RespawnPlayer() method in the Checkpoint script
            checkpoint.RespawnPlayer();
        }
    }       
  
}
