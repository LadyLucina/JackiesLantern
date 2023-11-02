using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M
 * Details: This script will manage and respawn the player at checkpoints set within the game.
 * By pressing the 'R' key, the player will respawn at the most recent checkpoint. 
 * Checkpoints are represented as a list of "checkpoint" objects, each associated with a specific 
 * checkpoint ID. 
 * This script will ensure that only one checkpoint is active at a time and will disable previous ones
 * when a new checkpoint is enabled. 
 */

public class CheckpointSystem : MonoBehaviour
{
    //Inspector variables
    public KeyCode respawnKey = KeyCode.R;
    public List<Checkpoint> checkpoints;

    private int currentCheckpointIndex = -1;

    private void Update()
    {
        if (Input.GetKeyDown(respawnKey))
        {
            RespawnAtLastCheckpoint();
        }
    }

    public void SetCheckpoint(int checkpointID)
    {
        if (checkpointID >= 0 && checkpointID < checkpoints.Count)
        {
            //If this is the same checkpoint as the current one, no need to do anything
            if (checkpointID == currentCheckpointIndex)
            {
                return;
            }

            //Disable the previous checkpoint if there was one
            if (currentCheckpointIndex >= 0)
            {
                checkpoints[currentCheckpointIndex].Disable();
            }

            currentCheckpointIndex = checkpointID;
            checkpoints[currentCheckpointIndex].Enable();
        }
    }

    public void RespawnAtLastCheckpoint()
    {
        if (currentCheckpointIndex >= 0)
        {
            checkpoints[currentCheckpointIndex].RespawnPlayer();
        }
    }
}