using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//Author: Stephanie M.
//Edited by: Joshua G

//This Script initiates Async loading between levels

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public string sceneToLoad;
    public CanvasGroup canvasGroup;
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void StartGame()
    {
        StartCoroutine(StartLoad());
    }
    IEnumerator StartLoad()
    {
        loadingScreen.SetActive(true);
        yield return StartCoroutine(FadeLoadingScreen(1, 1));
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!operation.isDone)
        {
            yield return null;
        }
        yield return StartCoroutine(FadeLoadingScreen(0, 1));
        loadingScreen.SetActive(false);
    }
    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetValue;
    }
    [Tooltip("Name of Scene to Load on Collision")]

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