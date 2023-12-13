using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Author: Stephanie M.
 * Details: This script causes an area to be "locked" until the condition is met that the "Key" has been obtained by the Player.
 * This script allows multiple ObjectIDs to be set so that multiple locked areas can exist within the same scene.
 */

public class LevelUnlocker : MonoBehaviour
{
    public string nextSceneName = "";  //Specify the next scene in the Unity inspector.
    public Text messageText;  //Reference to the UI Text component.
    public CollectableSystem collectableSystem;  //Reference to the CollectableSystem script.

    private bool isUnlocked = false;  //Show if the area is unlocked.

    private void Start()
    {
        if (messageText)
        {
            //Set initial message text and hide it
            messageText.text = "You need to find all collectibles to unlock this area!";
            messageText.gameObject.SetActive(false);
        }

        //Check if all collectables have been collected.
        if (collectableSystem && collectableSystem.AreAllCollectablesCollected())
        {
            isUnlocked = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isUnlocked)
        {
            //Check if all collectables have been collected.
            if (collectableSystem && collectableSystem.AreAllCollectablesCollected())
            {
                isUnlocked = true;

                //Load the next scene
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                if (messageText)
                {
                    //Show the message if not all collectables are collected
                    messageText.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && messageText)
        {
            //Hide the message when the player exits the trigger zone
            messageText.gameObject.SetActive(false);
        }
    }
}