using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Sprites;
using UnityEngine.UI;

public class PlayPauseCutscene : MonoBehaviour
{
    private VideoPlayer player;
    public Button button;
    public Sprite play;
    public Sprite pause;
    public Sprite blank;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
    }

    public void ChangeButton()
    {
        if (player.isPlaying == false)
        {
            player.Play();
            button.image.sprite = pause;
        }
        else
        {
            player.Pause();
            button.image.sprite = play;
        }
    }

    public void SkipCutscene()
    {
        player.Pause();
        Destroy(button);
        this.gameObject.SetActive(false);
        //button.image.sprite = blank;
    }

}
