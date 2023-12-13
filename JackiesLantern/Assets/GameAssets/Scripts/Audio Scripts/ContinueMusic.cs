using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Author: Mya & Billy Man (https://youtu.be/ha6U8jHl9ak)
/*Edited by: Stephanie M.
 * Added a method where the audio for the MainMenu scene would restart when returning to it from a level.
 */

public class ContinueMusic : MonoBehaviour
{
    public static ContinueMusic instance;
    private AudioSource audioSource;
    private bool isPlaying = false;

    void Awake()
    {
        //Ensures only one instance of ContinueMusic exists. Destroys others
        if (instance != null)
            Destroy(gameObject);
        else
        {
            //Sets unique instance
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {//Stops main menu music from playing in levels 1,2,3,4, Cutscenes & the end credits
        if      
                (SceneManager.GetActiveScene().name == "Intro"   ||
                 SceneManager.GetActiveScene().name == "Level 1" ||
                 SceneManager.GetActiveScene().name == "TrasLVL1-LVL2" ||
                 SceneManager.GetActiveScene().name == "Level 2" ||
                 SceneManager.GetActiveScene().name == "TransLVL2-LVL3" ||
                 SceneManager.GetActiveScene().name == "Level 3" ||
                 SceneManager.GetActiveScene().name == "Lewis Intro" ||
                 SceneManager.GetActiveScene().name == "Level 4" ||
                 SceneManager.GetActiveScene().name == "Credits")
        {
            //If audio is currently playing, stop it and update the flag
            if (isPlaying)
            {
                audioSource.Stop();
                isPlaying = false;
            }
        }

        //Checks if current scene is the MainMenu
        else if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            //If audio is not currently playing, start it and update the flag
            if (!isPlaying)
            {
                audioSource.Play();
                isPlaying = true;
            }
        }
    }

}
