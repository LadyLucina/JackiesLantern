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
    public int collectablesFound = 0;  //Number of collected Choco Loco bars
    public int totalCollectables = 3;  //Specify the total number of collectables in the level.
    public AudioSource AudioSource;  //Audio source for playing sounds
    public AudioClip chocoAquired;  //Sound played when a Choco Loco bar is acquired
    public AudioClip allChocoAquired;  //Sound played when all Choco Loco bars are collected
    public float volume;  //Volume for audio playback

    public CollectablesUI collectablesUIScript;  // Reference to CollectablesUI script

    public GameObject TurnOn;  //Can set Object to Active
    public GameObject TurnOn2;  //Can set Object to Active
    public GameObject TurnOff;  //Can set Object to Inactive

    private Animator OpenGate;  //Animator for gate opening animation

    //Checks if all collectables are collected
    public bool AreAllCollectablesCollected()
    {
        return collectablesFound >= totalCollectables;
    }

    //Triggered when a collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        //Checks if the game object has the "Collectable" tag
        if (other.gameObject.CompareTag("Collectable"))
        {
            //Increment the total amount of Choco Loco bars found
            collectablesFound += 1;

            //Play Choco Loco acquired sound
            AudioSource.PlayOneShot(chocoAquired, volume);

            //Destroy the Choco Loco bar object once passed through
            Destroy(other.gameObject);

            if (AreAllCollectablesCollected())
            {
                //If all Choco Loco bars are collected
                collectablesFound = totalCollectables;

                //Update the Collectables UI
                collectablesUIScript.UpdateCollectablesUI(collectablesFound, totalCollectables);

                //Play all Choco Loco acquired sound
                AudioSource.PlayOneShot(allChocoAquired, volume);

                //Turn On/Off specified game objects
                TurnOn.SetActive(true);
                TurnOn2.SetActive(true);
                TurnOff.SetActive(false);

                //Trigger the gate opening animation
                OpenGate.SetBool("openGate", true);
            }
            else
            {
                //If not all Choco Loco bars are collected, update the Collectables UI
                collectablesUIScript.UpdateCollectablesUI(collectablesFound, totalCollectables);
            }
        }
    }
}