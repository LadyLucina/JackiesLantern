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
    #region Inspector Variables
    public KeyCode respawnKey = KeyCode.R;   //The key used for manual respawn
    public List<Checkpoint> checkpoints;      //List of available checkpoints

    //Index to keep track of the current active checkpoint
    private int currentCheckpointIndex = -1;
    #endregion


    private void Update()
    {
        //Check if the player presses the respawn key
        if (Input.GetKeyDown(respawnKey))
        {
            //Respawn the player at the last checkpoint
            RespawnAtLastCheckpoint();
        }
    }

    //Method to set the active checkpoint by its ID
    public void SetCheckpoint(int checkpointID)
    {
        //Check if the provided checkpoint ID is within the valid range
        if (checkpointID >= 0 && checkpointID < checkpoints.Count)
        {
            //If this is the same checkpoint as the current one, then do nothing
            if (checkpointID == currentCheckpointIndex)
            {
                return;
            }

            //Disable the previous checkpoint if there was one
            if (currentCheckpointIndex >= 0)
            {
                checkpoints[currentCheckpointIndex].Disable();
            }

            //Update the current checkpoint index and enable the new checkpoint
            currentCheckpointIndex = checkpointID;
            checkpoints[currentCheckpointIndex].Enable();
        }
    }

    //Method to respawn the player at the last active checkpoint
    public void RespawnAtLastCheckpoint()
    {
        //Check if there is an active checkpoint
        if (currentCheckpointIndex >= 0)
        {
            //Respawn the player at the last active checkpoint
            checkpoints[currentCheckpointIndex].RespawnPlayer();
        }
    }
}