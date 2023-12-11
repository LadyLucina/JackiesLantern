using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public string nextSceneName = "";

    void OnEnable()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
