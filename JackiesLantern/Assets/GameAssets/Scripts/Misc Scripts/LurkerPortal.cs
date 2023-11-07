using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Mya
 * DESCRIPTION: Plays the LurkerPortal particle effect when player is in range of a lurker.
*/

public class LurkerPortal : MonoBehaviour
{

    public ParticleSystem Portal;

    private void OnTriggerEnter(Collider oher)
    {

        Portal.Play();  //Plays portal particle effects

    }


    void Start()
    {
        Portal.Pause();
    }


    public void Dissapear()
    {
        Portal.Pause(); //Destroy's particle effect after hand animation finishes playing
    }

    
}
