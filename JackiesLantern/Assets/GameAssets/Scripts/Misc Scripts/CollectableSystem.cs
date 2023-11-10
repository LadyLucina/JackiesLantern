using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M.
 * Details: This script functions to provide the player with an objective to complete before they can proceed to the next level. 
 * If the player picks up an item with the "Collectable" tag, it will either add to the total items needed to proceed or will unlock the next level,
 * depending on the specified number of objects needed within the Inspector.
 */

public class CollectableSystem : MonoBehaviour
{
    [Header("Choco Loco Bars Found")]
    public int collectablesFound = 0;
    public int totalCollectables = 3;  //Specify the total number of collectables in the level.
    public AudioSource AudioSource;
    public AudioClip chocoAquired;
    public float volume;

    public CollectablesUI collectablesUIScript; //Reference to CollectablesUI script

    public bool AreAllCollectablesCollected()
    {
        return collectablesFound >= totalCollectables;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checks if the game object has the "Collectable" tag
        if (other.gameObject.CompareTag("Collectable"))
        {
            //Increment the total amount of candy bars found
            collectablesFound += 1;

            AudioSource.PlayOneShot(chocoAquired, volume);

            //Object is destroyed once passed through
            Destroy(other.gameObject);

            if (AreAllCollectablesCollected())
            {
                collectablesFound = totalCollectables;
                collectablesUIScript.UpdateCollectablesUI(collectablesFound, totalCollectables);
            }
            else
            {
                collectablesUIScript.UpdateCollectablesUI(collectablesFound, totalCollectables);
            }
        }
    }
}