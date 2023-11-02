using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M.
 * Details: This script is to hold reference to the picked up "Key" objects and should not be placed
 * on any Game Objects.
 */

public class InventoryManager : MonoBehaviour
{
    private static Dictionary<int, bool> collectedKeys = new Dictionary<int, bool>();

    public static void CollectKey(int objectID)
    {
        if (!collectedKeys.ContainsKey(objectID))
        {
            collectedKeys[objectID] = true;
        }
    }

    public static bool HasKey(int objectID)
    {
        return collectedKeys.ContainsKey(objectID) && collectedKeys[objectID];
    }
}

