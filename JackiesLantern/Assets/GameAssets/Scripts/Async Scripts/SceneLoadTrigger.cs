using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Author: Joshua G


public class SceneLoadTrigger : MonoBehaviour
{
    public string targetScene;
    public void LoadScene()
    {
        LoadingData.sceneToLoad = targetScene;
        SceneManager.LoadScene("Loading");
    }

}
