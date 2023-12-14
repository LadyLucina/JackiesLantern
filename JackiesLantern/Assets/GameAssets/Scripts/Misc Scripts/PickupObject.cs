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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.CollectKey(objectID);
            Destroy(gameObject);
        }
    }
}
