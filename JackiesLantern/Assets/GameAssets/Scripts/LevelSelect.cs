using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{

    public void LoadLevel1()
    {
        //Load level 1 scene
        SceneManager.LoadScene("Level 1");
    }

    public void LoadLevel2()
    {
        //Load level 2 scene
        SceneManager.LoadScene("Level 2");
    }

    public void LoadLevel3()
    {
        //Load level 3 scene
        SceneManager.LoadScene("Level 3");
    }

    public void LoadLevel4()
    {
        //Load level 4 scene
        SceneManager.LoadScene("Level 4");
    }
}