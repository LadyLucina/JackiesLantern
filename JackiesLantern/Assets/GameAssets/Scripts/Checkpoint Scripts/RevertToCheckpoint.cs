using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Joshua Guerrero
 * Purpose: This script will have the player respawn at the most recent checkpoint
 * when the player is at 0 health
 */
public class RevertToCheckpoint : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Checkpoint checkpoint;
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        checkpoint = GetComponent<Checkpoint>();
    }

    private void Update()
    {
        if (healthSystem.currentHealth <= 1)
        {
            checkpoint.RespawnPlayer();
        }
    }
        
  
}
