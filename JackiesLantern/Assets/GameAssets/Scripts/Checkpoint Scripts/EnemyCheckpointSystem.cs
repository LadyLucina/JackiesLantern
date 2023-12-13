using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Author: Joshua G
 * Purpose: This is a script that will be attached to enemies like The Farmer and The Final Boss
 * When an enemy with this script attached touches the player, the player will be sent back to the most recent checkpoint
 * This script will reference the Checkpoint and CheckpointSystem script
 */

//Attach this script to the player!


public class EnemyCheckpointSystem : MonoBehaviour
{
    #region Inspector Variables

    public Checkpoint checkPoint;   //Reference to the Checkpoint script
    public CheckpointSystem checkpointSystem;   //Reference to the CheckpointSystem script
    private GameObject farmerEnemy;     //Reference to the player damage script
    private int currentCheckpointIndex = -1;    //Index to keep track of the current active checkpoint
    public List<Checkpoint> checkpoints;    //List of available checkpoints

    #endregion


    private void Start()
    {
        //Retrieve the variables from the CheckpointSystem script
        checkpointSystem = GetComponent<CheckpointSystem>();

        //Retrieve the variables from the Checkpoint script
        checkPoint = GetComponent<Checkpoint>();
    }

    //These lines of code set the player back to the last checkpoint when the player touches the farmer.
    private void OnTriggerEnter(Collider other)
    {
        //Check if the colliding object has the tag "Farmer"
        if (other.gameObject.CompareTag("Farmer"))
        {
            //Store a reference to the farmer enemy
            farmerEnemy = other.gameObject;

            //Retrieve the variables from the Checkpoint script attached to the farmer
            Checkpoint checkpoint = other.GetComponent<Checkpoint>();

            //Check if the checkpoint component is not null
            if (checkpoint != null)
            {
                //Check if there is an active checkpoint
                if (currentCheckpointIndex >= 0)
                {
                    //Respawn the player at the last active checkpoint
                    checkpoints[currentCheckpointIndex].RespawnPlayer();

                }
            }
        }
    }
}