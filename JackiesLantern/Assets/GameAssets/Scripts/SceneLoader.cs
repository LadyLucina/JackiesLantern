using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//Author: Jett G

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load
    public GameObject loadingUI; // Reference to the loading UI (Canvas)

    // Call this function to load the scene
    public void LoadScene()
    {
        // Show the loading UI
        loadingUI.SetActive(true);

        // Use a Coroutine to load the scene asynchronously
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        // Load the scene asynchronously in the background
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        // Wait until the asynchronous operation is complete
        while (!asyncLoad.isDone)
        {
            // Calculate the loading progress and update the UI (e.g., a loading bar)
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // 0.9 is the maximum progress value

            // Update your loading UI (e.g., set a loading bar fill amount)
            // loadingBar.fillAmount = progress;

            yield return null; // Wait for the next frame
        }
    }
}