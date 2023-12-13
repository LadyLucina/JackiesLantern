using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M.
 * Details: This script is to be placed on an object with the "Key" tag. It will have a unique ID assigned from within
 * the Unity inspector to unlock a corresponding zone that also has the same ID. 
 */

public class PickupObject : MonoBehaviour
{
    public int objectID = 1;  //Unique Object ID for this key.

    //Called when another collider enters the trigger zone of this object.
    private void OnTriggerEnter(Collider other)
    {
        //Check if the colliding object has the "Player" tag.
        if (other.CompareTag("Player"))
        {
            //Collect the key with the specified object ID using the InventoryManager.
            InventoryManager.CollectKey(objectID);

            //Destroy this game object after the key is collected.
            Destroy(gameObject);
        }
    }
}