using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M
 * Details: This script handles the player interactions with checkpoints.
 * When the player collides with a trigger zone with the "Checkpoint" tag, this
 * script will retrieve the checkpoints ID and communicates with the "CheckpointSystem" script
 * to set this checkpoint as the active one. 
 */

public class PlayerCheckpointController : MonoBehaviour
{

    public CheckpointSystem checkpointSystem;   //Reference to the CheckpointSystem script

    //Called when a collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        //Check if the colliding object has the tag "Checkpoint"
        if (other.CompareTag("Checkpoint"))
        {
            //Get the Checkpoint component from the collided object
            Checkpoint checkpoint = other.GetComponent<Checkpoint>();

            //Check if the Checkpoint component is not null
            if (checkpoint != null)
            {
                //Retrieve the checkpoint ID from the Checkpoint component
                int checkpointID = checkpoint.checkpointID;

                //Set the active checkpoint in the CheckpointSystem
                checkpointSystem.SetCheckpoint(checkpointID);
            }
        }
    }
}