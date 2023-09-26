using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void HelpButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Level 1");
    }
}
