using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSystem : MonoBehaviour
{
    [Header("Choco Loco Bars Found")]
    public int collectablesFound = 0;


    private GameObject absorbCollectable;
    private void OnTriggerEnter(Collider other)
    {
        //Checks if the gameobject has the "Collectable" tag
        if (other.gameObject.CompareTag("Collectable"))
        {
            //Stores Collectable object
            absorbCollectable = other.gameObject;

            //Adds +1 to total amounf of candy bars found
            collectablesFound = (collectablesFound + 1);

            //Object destroys once passed through
            Destroy(absorbCollectable);
        }
    }
}