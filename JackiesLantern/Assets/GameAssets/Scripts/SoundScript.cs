using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Jonathon T.
 * Details: This script provides a versatile audio playback system for Unity, 
 * allowing the user to manage and control the playback of multiple audio clips, 
 * including options for looping and randomization.
 */

[RequireComponent(typeof(AudioSource))]
public class SoundScript : MonoBehaviour
{
    //Determines whether the audio should loop.
    public bool shouldLoop = false;

    //A list of audio clips to play.
    public List<AudioClip> audioClips = new List<AudioClip>();

    //Reference to the AudioSource component.
    private AudioSource audioSource = null;

    //Index to keep track of the currently selected audio clip.
    private int index = 0;


    private void Awake()
    {
        //Get the AudioSource component attached to this GameObject.
        audioSource = GetComponent<AudioSource>();
    }

    //Plays the next audio clip in the list.
    public void NextClip()
    {
        index = ++index % audioClips.Count;
        PlayClip();
    }

    //Plays the previous audio clip in the list.
    public void PreviousClip()
    {
        index = --index % audioClips.Count;
        PlayClip();
    }

    //Plays a random audio clip from the list.
    public void RandomClip()
    {
        index = Random.Range(0, audioClips.Count);
        PlayClip();
    }

    //Plays the audio clip at the specified index.
    public void PlayAtIndex(int value)
    {
        // Ensure the index is within the valid range of audioClips.
        index = Mathf.Clamp(value, 0, audioClips.Count);
        PlayClip();
    }

    //Pauses the currently playing audio clip.
    public void PauseClip()
    {
        audioSource.Pause();
    }

    //Stops the currently playing audio clip.
    public void StopClip()
    {
        audioSource.Stop();
    }

    //Plays the currently selected audio clip.
    public void PlayCurrentClip()
    {
        PlayClip();
    }

    //Internal method to play the audio clip at the current index.
    private void PlayClip()
    {
        //Ensure the index is within the valid range of audioClips.
        audioSource.clip = audioClips[Mathf.Abs(index)];
        audioSource.Play();
    }

    //This method is called in the Unity Editor when a value is changed in the Inspector.
    private void OnValidate()
    {
        //Set the AudioSource's loop property based on the shouldLoop variable.
        audioSource.loop = shouldLoop;
    }
}