using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    public GameObject imagePopup; //Reference to the UI image popup
    public GameObject HealthUI; //Reference to the Health UI Game Object
    public GameObject CollectUI; //Reference to the Collect UI Game Object
    public GameObject MiniMapUI; //Reference to the Mini Map UI Game Object
    public Button closeButton;    //Reference to the UI button to close the image

    void Start()
    {
        //Show the image pop up on start up
        imagePopup.SetActive(true);

        //Subscribe to the button click event
        closeButton.onClick.AddListener(OnCloseButtonClick);

        //Show the image popup and pause the game time
        ShowImagePopup(true);

        //Disable the UI Elements on Start Up
        ShowUIElements(false);
        Time.timeScale = 0f;
    }

    void OnCloseButtonClick()
    {
        //Hide the image popup and resume the game time
        ShowImagePopup(false);
        ShowUIElements(true);;
        Time.timeScale = 1f;
    }

    void ShowImagePopup(bool show)
    {
        //Enable or disable the How To Play popup based on the 'show' parameter
        imagePopup.SetActive(show);
    }

    void ShowUIElements(bool show)
    {
        //Enable or disable the game object based on the 'show' parameter
        HealthUI.SetActive(show);
        CollectUI.SetActive(show);
        MiniMapUI.SetActive(show);

    }
}