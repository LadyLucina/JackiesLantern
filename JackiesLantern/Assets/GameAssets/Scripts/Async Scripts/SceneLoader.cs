using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//Author: Stephanie M.
//Edited by: Joshua G

//This Script initiates Async loading between levels

public class SceneLoader : MonoBehaviour
{
    //Reference to the loading screen UI element
    public GameObject loadingScreen;

    //Name of the scene to be loaded, can be set in the Inspector
    public string sceneToLoad;

    //Reference to the canvas group for fading effects
    public CanvasGroup canvasGroup;


    public void Start()
    {
        //Ensure that this GameObject persists across scene changes
        DontDestroyOnLoad(gameObject);
    }

    //Method to initiate the scene loading process
    public void StartGame()
    {
        //Start the coroutine for loading the scene
        StartCoroutine(StartLoad());
    }

    //Coroutine to handle the loading process
    IEnumerator StartLoad()
    {
        //Activate the loading screen UI
        loadingScreen.SetActive(true);

        //Fade in the loading screen
        yield return StartCoroutine(FadeLoadingScreen(1, 1));

        //Asynchronously load the specified scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

        //Wait until the loading operation is complete
        while (!operation.isDone)
        {
            yield return null;
        }

        //Fade out the loading screen
        yield return StartCoroutine(FadeLoadingScreen(0, 1));

        //Deactivate the loading screen UI
        loadingScreen.SetActive(false);
    }

    //Coroutine to handle fading of the loading screen
    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        //Get the starting alpha value of the canvas group
        float startValue = canvasGroup.alpha;
        float time = 0;

        //Interpolate between the start and target alpha values over time
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        //Set the final alpha value
        canvasGroup.alpha = targetValue;
    }

    //Called when a collider enters the trigger zone
    [Tooltip("Name of Scene to Load on Collision")]
    private void OnTriggerEnter(Collider other)
    {
        //Checks that the colliding object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            //Load the specified scene (can be adjusted within the Inspector)
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}