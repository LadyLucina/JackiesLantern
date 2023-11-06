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
    //public Material Material2;
    public Renderer Object;
    private Animator anim;

    //public Material[] materialList;

    private void Start()
    {
        lurkerAnim = this.GetComponent<Animator>();
        anim = GetComponent<Animator>();
    }

    //When player is in range, animation plays
    private void OnTriggerEnter(Collider oher)
    {
        /*
        foreach (Material material in materialList)
        {
            materialList[0].SetActive(false);
        }
        */

        lurkerAnim.SetBool("inRange", true);
        Object.material = Material1;  //Changes material of hand when player is in range

        //Plays specific animation from the Animator
        anim.Play("Grab_1");



    }

    public void Dissapear()
    {
        Destroy(gameObject);
    }
}
