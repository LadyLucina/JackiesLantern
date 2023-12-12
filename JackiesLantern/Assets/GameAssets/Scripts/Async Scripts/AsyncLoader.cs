using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //for scene changing
using TMPro; //for text mesh pro
using UnityEngine.UI; //for basic ui

public class AsyncLoader : MonoBehaviour
{
    private bool sceneLoaded = false; //check if you have asked for the scene to be loaded
    public string sceneToLoad; //name of scene to load
    [SerializeField]
    private TextMeshProUGUI loadingText; //progress text
    [SerializeField]
    private TextMeshProUGUI spaceText; //press spacebar text
    [SerializeField]
    private Slider sliderBar; //slider for progress
    // Start is called before the first frame update
    void Start()
    {
        spaceText.enabled = false; //hide the spacebar text 
    }

    // Update is called once per frame
    void Update()
    {
        if (!sceneLoaded)
        {
            sceneLoaded = true;
            StartCoroutine(LoadNewScene(sceneToLoad));
        }
    }

    IEnumerator LoadNewScene(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName); //begins async operation
        async.allowSceneActivation = false; //makes it so it doesn't auto transition to next scene

        while (!async.isDone) //while async < .9 ASYNC ONLY LOADS FROM 0- 0.9
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f); //fancy math to get your progress in a nice number
            sliderBar.value = progress; //apply it to your slider
            loadingText.text = "Loading: " + (progress * 100f) + "%"; //print it to your text

            if (async.progress >= 0.9f) //if scene is "fully" loaded
            {
                spaceText.enabled = true; //enable space text
                if (Input.GetKeyDown(KeyCode.Space)) //if space is pressed
                {
                    async.allowSceneActivation = true; //allow scene transition
                }
            }
            yield return null;
        }
        
    }
}
