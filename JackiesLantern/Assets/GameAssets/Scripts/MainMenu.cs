using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Author: Mya

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("Settings");
    }

    public void HelpButton()
    {
        SceneManager.LoadScene("Help");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitButton()
    {
        Application.Quit(); 
    }
}
