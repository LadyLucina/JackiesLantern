using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  Author: Stephanie M
    Details: Enables/Disables the target object based on the players collision with the trigger object.
 */

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
                //If the target object is not null, then set true
                targetObject.SetActive(true);
            }
        }
    }
}