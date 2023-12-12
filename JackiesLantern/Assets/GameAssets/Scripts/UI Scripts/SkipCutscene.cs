using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipCutscene : MonoBehaviour
{
    [SerializeField] private string nextSceneName; //Set this in the Unity Inspector

    //This function is called when the attached button is pressed
    public void LoadNextSceneOnClick()
    {
        //Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //Calculate the index of the next scene
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        //Load the next scene
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadSpecificScene(string sceneName)
    {
        //Load the next scene
        SceneManager.LoadScene(sceneName);
    }
}

