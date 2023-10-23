using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playOnCollision : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip lurkerAlert;
    public float volume;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(lurkerAlert, volume);
            Debug.Log("yes");
        }
    }
}
