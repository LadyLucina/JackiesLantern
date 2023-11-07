using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSystem : MonoBehaviour
{
    [Header("Choco Loco Bars Found")]
    public int collectablesFound = 0;
    public int totalCollectables = 3;  //Specify the total number of collectables in the level.

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

            //Object is destroyed once passed through
            Destroy(other.gameObject);

            if (AreAllCollectablesCollected())
            {
                collectablesFound = totalCollectables;
            }
        }
    }
}