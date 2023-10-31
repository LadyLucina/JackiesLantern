using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M
 * Details: When the player comes within the defined detection range and one of the enemies
 * "Lurker", "Trapper", "Farmer" and/or "Boss", the lanterns light will switch to the color
 *  red, immediately alerting the player to the threat. When the player moves out of range, 
 *  the light will revert back to its original color. 
 * 
 */

public class EnemyDetection : MonoBehaviour
{
    public GameObject player; //Reference to the player object
    public Light objectLight; //Reference to the light component of the object to be controlled
    public float detectionRange = 10.0f; //Detection range for the enemies
    public Color originalLightColor = Color.white; //Original light color
    public Color enemyInRangeColor = Color.red; //Enemy alert color

    private void Start()
    {
        //Checks to verify the player object is assigned
        if (player == null)
        {
            Debug.LogError("Player object not assigned to the script.");
            enabled = false;
        }

        //Checks to verify the light component is assigned
        if (objectLight == null)
        {
            Debug.LogError("Light component not assigned to the script.");
            enabled = false;
        }

        //Initialize the light color to the original color
        objectLight.color = originalLightColor;
    }

    private void Update()
    {
        bool playerInRange = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (Collider collider in colliders)
        {
            //Check if the collider has one of the specified enemy tags
            if (collider.CompareTag("Lurker") || collider.CompareTag("Trapper") || collider.CompareTag("Farmer") || collider.CompareTag("Boss"))
            {
                playerInRange = true;
                break;
            }
        }

        //If an enemy is in range, change the light color to enemyInRangeColor
        if (playerInRange)
        {
            objectLight.color = enemyInRangeColor;
        }
        //If no enemies are in range, revert to the original light color
        else
        {
            objectLight.color = originalLightColor;
        }
    }

}
