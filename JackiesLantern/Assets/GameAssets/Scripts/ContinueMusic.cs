using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Author: Mya & Billy Man (https://youtu.be/ha6U8jHl9ak)

public class ContinueMusic : MonoBehaviour
{
    public static ContinueMusic instance;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {//Stops main menu music from playing in levels 1,2,3 & 4 & the end credits
        if (SceneManager.GetActiveScene().name == "Level 1")
            ContinueMusic.instance.GetComponent<AudioSource>().Pause();

        if (SceneManager.GetActiveScene().name == "Level 2")
            ContinueMusic.instance.GetComponent<AudioSource>().Pause();

        if (SceneManager.GetActiveScene().name == "Level 3")
            ContinueMusic.instance.GetComponent<AudioSource>().Pause();

        if (SceneManager.GetActiveScene().name == "Level 4")
            ContinueMusic.instance.GetComponent<AudioSource>().Pause();

        if (SceneManager.GetActiveScene().name == "Credits")
            ContinueMusic.instance.GetComponent<AudioSource>().Pause();

    }

}
