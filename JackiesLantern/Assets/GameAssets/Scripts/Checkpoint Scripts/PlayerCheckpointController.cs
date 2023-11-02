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
    public CheckpointSystem checkpointSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint")) //Make sure the checkpoint objects have the appropriate tag
        {
            Checkpoint checkpoint = other.GetComponent<Checkpoint>();
            if (checkpoint != null)
            {
                int checkpointID = checkpoint.checkpointID;
                checkpointSystem.SetCheckpoint(checkpointID);
            }
        }
    }
}
