using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneUI : MonoBehaviour
{
    [Header("Win Scene UI")]
    //Reference from the Win Scene Menu Game Object in Inspector
    public GameObject winMenu;
    //Reference from the Confirmation Pop Up Game Object in Inspector
    public GameObject confirmQuitPopup;

    private bool isPaused = false;

    void Start()
    {
        //Show the win scene menu and exit confirmation popup
        winMenu.SetActive(true);
        confirmQuitPopup.SetActive(false);
        PauseGame();
    }

    //Function for paused game
    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    //Function to display quit confirmation popup
    public void ShowQuitConfirm()
    {
        confirmQuitPopup.SetActive(true);
    }

    //This function will be called when the user confirms they wish to exit and return to Main Menu
    public void ReturnToMenu()
    {
        //Unpause the game
        Time.timeScale = 1;

        //Loads Main Menu scene
        SceneManager.LoadScene("MainMenu");
    }

    //This function will be called when the user decides to cancel the quit request
    public void QuitCancelled()
    {
        confirmQuitPopup.SetActive(false);
    }

    //Function to quit the game
    public void QuitGame()
    {
        //Show the exit confirmation popup for quitting
        ShowQuitConfirm();
    }

    //This function will be called when the user confirms they wish to quit the game entirely
    public void ConfirmQuit()
    {
        Debug.Log("Application closing...");
        //Quit the application
        Application.Quit();
    }

}


