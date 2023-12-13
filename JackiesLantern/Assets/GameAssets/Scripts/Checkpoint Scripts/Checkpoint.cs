using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  Author: Stephanie M
 *  Details: This script allows the user to mark a position in the game as a checkpoint.
 *  It tracks whether the checkpoint is enabled or disabled. 
 *  When the player collides with the checkpoint, it will be saved as the most recent/active
 *  checkpoint. The player can "Respawn" at this point by pressing 'R'.
 *  Checkpoint IDs can be assigned and will work with the Checkpoint System script. 
 */

public class Checkpoint : MonoBehaviour
{
    #region Inspector Variables
    //Reference for the player transform
    public Transform playerTransform;

    //Inspector option to start the checkpoint as enabled or disabled
    public bool startEnabled = false;

    //Indicates if the checkpoint is currently enabled
    public bool isEnabled = false;

    //The ID of the checkpoint
    public int checkpointID;

    //Static variable to keep track of the most recent checkpoint across all instances
    private static Checkpoint mostRecentCheckpoint = null;
    #endregion

    private void Start()
    {
        //Check if the checkpoint should start as enabled or disabled
        if (startEnabled)
        {
            Enable();
        }
        else
        {
            Disable();
        }
    }

    //Called when a collider enters the trigger zone of the checkpoint
    private void OnTriggerEnter(Collider other)
    {
        //Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            //Enable the checkpoint when the player enters the trigger zone
            Enable();
        }
    }

    //Method to enable the checkpoint
    public void Enable()
    {
        //Check if this is a different checkpoint from the most recent one
        if (this != mostRecentCheckpoint)
        {
            //Disable the previous most recent checkpoint if there was one
            if (mostRecentCheckpoint != null)
            {
                mostRecentCheckpoint.Disable();
            }

            //Set this checkpoint as the most recent one
            mostRecentCheckpoint = this;
        }

        //Enable the checkpoint
        isEnabled = true;
    }

    //Method to disable the checkpoint
    public void Disable()
    {
        //Disable the checkpoint
        isEnabled = false;
    }

    //Method to respawn the player at the checkpoint's position
    public void RespawnPlayer()
    {
        //Teleport the player to the checkpoint's position if this checkpoint is enabled
        if (isEnabled)
        {
            playerTransform.position = transform.position;
        }
    }

    private void Update()
    {
        //Check if the checkpoint is enabled and the player presses the "R" key
        if (isEnabled && Input.GetKeyDown(KeyCode.R))
        {
            //Respawn the player
            RespawnPlayer();
        }
    }
}