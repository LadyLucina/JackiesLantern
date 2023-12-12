using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LewisFootstep: MonoBehaviour
{
    [SerializeField]
    private AudioClip[] Stomp;


    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void LewisStep()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        {
                return Stomp[UnityEngine.Random.Range(0, Stomp.Length)];
           
        }

    }
}
