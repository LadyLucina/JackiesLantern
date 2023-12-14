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
    // Reference the Checkpoint script
    public Checkpoint checkPoint;
    //Reference the CheckpointSystem script
    public CheckpointSystem checkpointSystem;
    //references the player damage script
    private GameObject farmerEnemy;
    private int currentCheckpointIndex = -1;
    public List<Checkpoint> checkpoints;

    private void Start()
    {
        //retrieves the variables from CheckpointSystem
        checkpointSystem = GetComponent<CheckpointSystem>();
        //rerieves the variables from Checkpoint
        checkPoint = GetComponent<Checkpoint>();
    }

    //These lines of code set the player back to the last checkpoint when the player touches the farmer.
    //
    //
    //FOR STEPHANIE ONLY. Please check lines 36-56.
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Farmer"))
        {
              farmerEnemy = other.gameObject;
            //rerieves the variables from Checkpoint
            Checkpoint checkpoint = other.GetComponent<Checkpoint>();
            if (checkpoint != null)
            {

                if (currentCheckpointIndex >= 0)
                {
                    checkpoints[currentCheckpointIndex].RespawnPlayer();
                    //To make sure that this function is working
                    Debug.Log("Farmer is attacking");
                }
            }
        }
    }
}
