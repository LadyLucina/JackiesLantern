using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Author: Stephanie M.
 * Details: Loads the next specified level on Player collsiion
 */

public class LevelLoader : MonoBehaviour
{
    public string nextLevelName; //Name of the next level to load

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //Checks if the object colliding has the "Player" tag
        {
            //Load the next level
            SceneManager.LoadScene(nextLevelName);
        }    
    }
}
