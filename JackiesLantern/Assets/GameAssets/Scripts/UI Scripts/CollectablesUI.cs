using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Author: Stephanie M.
 * Details: This script displays the total amount of Choco Bars needing to be found before unlocking the next level.
 * This script also includes a UI text box prompt at the beginning of the level and when all Choco Bars have been found. 
 */

public class CollectablesUI : MonoBehaviour
{
    [Header("Collectables UI")]
    public Text notificationText;
    public Image collectablesImage;
    public Sprite[] collectablesSprites;
    public float displayTime = 5f;  //Time to display initial and final texts

    private bool isDisplaying;

    public void Start()
    {
        //Initialize UI elements
        notificationText.text = "Find all the Choco Loco bars!";
        collectablesImage.sprite = collectablesSprites[0];
        isDisplaying = true;

        //Set up initial display time
        Invoke("HideNotification", displayTime);
    }

    public void UpdateCollectablesUI(int collectablesFound, int totalCollectables)
    {
        if (isDisplaying) return;

        //Update UI based on collectables found
        collectablesImage.sprite = collectablesSprites[Mathf.Clamp(collectablesFound, 0, totalCollectables - 1)];

        if (collectablesFound == totalCollectables)
        {
            ShowNotification("All Choco Loco bars have been found!");
        }
    }

    private void ShowNotification(string text)
    {
        notificationText.text = text;
        isDisplaying = true;

        //Set up final display time
        Invoke("HideNotification", displayTime);
    }

    private void HideNotification()
    {
        notificationText.text = "";
        isDisplaying = false;
    }
}