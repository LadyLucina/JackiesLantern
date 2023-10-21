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
    public Transform playerTransform;
    public bool startEnabled = false; //Inspector option to start the checkpoint as enabled or disabled
    public bool isEnabled = false; //Indicates if the checkpoint is currently enabled
    public int checkpointID; //The ID of the checkpoint

    private static Checkpoint mostRecentCheckpoint = null; //Static variable to keep track of the most recent checkpoint

    private void Start()
    {
        if (startEnabled)
        {
            Enable();
        }
        else
        {
            Disable();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Enable();
        }
    }

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

    public void Disable()
    {
        //Disable the checkpoint
        isEnabled = false;
    }

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
        if (isEnabled && Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }
}