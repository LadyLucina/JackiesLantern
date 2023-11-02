using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M.
 * Details: Plays an audio source when Player is within range of trigger object
 */
public class AudioInRange : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float volume;

    [Header("Player Specific Settings")]
    public Transform player;
    public float maxRange = 10.0f;

    private bool isInRange = false;


    void Update()
    {
        //Calculate the distance between the player and this object.
        float distance = Vector3.Distance(player.position, transform.position);

        //Check if the player is within the range.
        if (distance <= maxRange)
        {
            //If not already playing, start playing the audio.
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip, volume);
                isInRange = true;
            }
        }
        else
        {
            //If the player is out of range and audio is playing, stop it.
            if (isInRange && audioSource.isPlaying)
            {
                isInRange = false;
            }
        }
    }
}