using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Author: Stephanie M.
 * Details: This script manages the pausing of the game, displaying a pause menu, and handling the
 * confirmation popup for quitting the game. It provides the user with options to resume the game, 
 * return to main menu, or quit the game entirely, with appropriate confirmations and pausing/unpausing
 * of the game as needed.
 */

public class PauseManager : MonoBehaviour
{
    [Header("Pause Menu UI")]
    //Reference from the Pause Menu Game Object in Inspector
    public GameObject pauseMenu;
    //Reference from the Confirmation Pop Up Game Object in Inspector
    public GameObject confirmQuitPopup;

    private bool isPaused = false;

    void Start()
    {
        //Hide the pause menu and exit confirmation popup
        pauseMenu.SetActive(false);
        confirmQuitPopup.SetActive(false);
    }

    void Update()
    {
        //Checks for the ESC key to toggle game pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    //Function for paused game
    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        pauseMenu.SetActive(true);
    }

    //Function to resume game from paused state
    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.SetActive(false);
        confirmQuitPopup.SetActive(false);
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

        //Open Survey 
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSeF2SJ41Yn9FNxXzIqnOEkkS9N3vo4Ikow5ijKuD3o6UsgtbA/viewform");

        //Quit the application
        Application.Quit();
    }

    //This function will be called when the user confirms they wish to quit the game entirely
    public void ConfirmQuit()
    {
        Debug.Log("Application closing...");

        //Open Survey 
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSeF2SJ41Yn9FNxXzIqnOEkkS9N3vo4Ikow5ijKuD3o6UsgtbA/viewform");

        //Quit the application
        Application.Quit();
    }

    //This function will reload the players current scene
    public void RestartCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneName);

    }

}


