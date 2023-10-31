// xxRafael Productions - Rafael Vicuna & Mya

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonSoundEffect : MonoBehaviour
{

    public AudioSource soundPlayer;
    public AudioClip hover;

    public void hoverSound()
    {
        soundPlayer.PlayOneShot(hover);
    }
    public void playClickSound()
    {

        soundPlayer.Play();
    }

}
