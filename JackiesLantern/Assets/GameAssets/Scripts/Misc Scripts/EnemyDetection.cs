using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M
 * Details: When the player comes within the defined detection range and one of the enemies
 * "Lurker", "Trapper", "Farmer" and/or "Boss", the lanterns light will switch to the color
 *  red, immediately alerting the player to the threat. When the player moves out of range, 
 *  the light will revert back to its original color. 
 */

public class EnemyDetection : MonoBehaviour
{
    public GameObject player;  //Reference to player object
    public Light objectLight;  //Reference to light component
    public float detectionRange = 10.0f;  //Range within the enemys presence is detected
    public Color originalLightColor;  //The original color of the light
    public Color enemyInRangeColor;  //The color the light should be when the enemy is in range

    private void Start()
    {
        //Check if the player object is assigned. If not, display an error message and disable the script.
        if (player == null)
        {
            Debug.LogError("Player object not assigned to the script.");
            enabled = false;
        }

        //Check if the light component is assigned. If not, display an error message and disable the script.
        if (objectLight == null)
        {
            Debug.LogError("Light component not assigned to the script.");
            enabled = false;
        }

        //Set the initial color of the light to originalLightColor.
        objectLight.color = originalLightColor;
    }

    private void Update()
    {
        float closestEnemyDistance = Mathf.Infinity;

        //Iterate through a list of enemy tags and find the closest enemy from each tag.
        foreach (string enemyTag in new string[] { "Lurker", "Trapper", "Farmer", "Skully", "Boss" })
        {
            //Find all game objects with the specified enemy tag.
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

            foreach (GameObject enemy in enemies)
            {
                //Check the distance to each enemy and find the closest one.
                float distance = Vector3.Distance(player.transform.position, enemy.transform.position);

                //Update the closestEnemyDistance if a closer enemy is found.
                if (distance < closestEnemyDistance)
                {
                    closestEnemyDistance = distance;
                }
            }
        }

        //Calculate the lerp factor based on the distance to the closest enemy
        float lerpFactor = Mathf.InverseLerp(0, detectionRange, closestEnemyDistance);

        //Interpolate between the original color and enemyInRangeColor
        objectLight.color = Color.Lerp(enemyInRangeColor, originalLightColor, lerpFactor);
    }
}