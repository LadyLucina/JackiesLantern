using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//Author: Stephanie M.

public class SceneLoader : MonoBehaviour
{
    [Tooltip("Name of Scene to Load on Collision")]
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        //Checks that the colliding object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            //Load specified scene - Can be adjusted within Inspector
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}