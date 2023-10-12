using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsAudio : MonoBehaviour
{
    //private AudioSource audioSource;

    private AudioSource footStep;

    private void Awake()
    {
        footStep = GetComponent<AudioSource>();
    }

    private void Step()
    {
        footStep.Play();
    }

    //public void playFootStepSound()
    //{

    //footStep.Play();

    //}
}

   


/*
public AudioSource footStep;

public void playFootStepSound()
{

footStep.Play();
}


}
  */