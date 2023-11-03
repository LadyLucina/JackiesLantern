using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Author: Mya
 * Details: Designed to manage the main menu interactions and scene transitions
 * with a specified time delay to allow On Click() audio be heard. 
 * Edited by: Stephanie M
 */

public class MainMenu : MonoBehaviour
{
    //Determine (within Inspector) how long to delay the scene load in seconds
    [Tooltip("Delay (in seconds) before loading the scene")]
    public float delayTime = 0.0f;

    public void PlayButton()
    {
        //Load Level 1
        StartCoroutine(LoadSceneWithDelay("Level 1"));
    }

    public void MainMenuButton()
    {
        //Load Main Menu Scene
        StartCoroutine(LoadSceneWithDelay("MainMenu"));
    }

    public void CreditsButton()
    {
        //Load Credits Scene
        StartCoroutine(LoadSceneWithDelay("Credits"));
    }

    public void ExitButton()
    {
        Application.Quit(); 
    }

    //Coroutine to load a scene with a specified delay
    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        //Wait for the specified delay time
        yield return new WaitForSeconds(delayTime);

        //Load the specified scene after the delay
        SceneManager.LoadScene(sceneName);
    }
}
