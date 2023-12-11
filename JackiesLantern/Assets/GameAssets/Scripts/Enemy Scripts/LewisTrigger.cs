using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LewisTrigger : MonoBehaviour
{
    //Reference to the GameObject you want to enable/disable
    public GameObject targetObject;

    //Check if the Player enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        //Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            //Enable the targetObject
            if (targetObject != null)
            {
                targetObject.SetActive(true);
            }
        }
    }
}