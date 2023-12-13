using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M.
 * Details: This script is to hold reference to the picked up "Key" objects and should not be placed
 * on any Game Objects.
 */

public class InventoryManager : MonoBehaviour
{
    //Dictionary to keep track of collected keys using their objectID as the key and a boolean indicating collection status
    private static Dictionary<int, bool> collectedKeys = new Dictionary<int, bool>();

    //Method to collect a key identified by its objectID
    public static void CollectKey(int objectID)
    {
        //Check if the key with the specified objectID is not already collected
        if (!collectedKeys.ContainsKey(objectID))
        {
            //Mark the key as collected by setting its value to true in the dictionary
            collectedKeys[objectID] = true;
        }
    }

    //Method to check if a key with the specified objectID has been collected
    public static bool HasKey(int objectID)
    {
        //Return true if the key with the specified objectID exists in the dictionary and is marked as collected
        return collectedKeys.ContainsKey(objectID) && collectedKeys[objectID];
    }
}