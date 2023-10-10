using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M.
 * Details: Plays an audio source when Player is within range of trigger object
 */
public class AudioInRange : MonoBehaviour
{
    public AudioSource audioSource;
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
                audioSource.Play();
                isInRange = true;
            }
        }
        else
        {
            //If the player is out of range and audio is playing, stop it.
            if (isInRange && audioSource.isPlaying)
            {
                audioSource.Stop();
                isInRange = false;
            }
        }
    }
}