using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Mya
 * Plays enemy grabbing animation. Also changes material from transparent to Trapper Material.
 * 
 */
public class LurkerAnimation : MonoBehaviour
{
    private Animator lurkerAnim;
    public Material Material1;
    public Renderer Object;

    private void Start()
    {
        lurkerAnim = this.GetComponent<Animator>();
        

    }

    //When player is in range, animation plays
    private void OnTriggerEnter(Collider other)
    {
        lurkerAnim.SetBool("inRange", true);
        Object.material = Material1;  //Changes material of hand when player is in range
        
    }
}
