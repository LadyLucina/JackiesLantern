using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AUTHOR: Mya
 * The (2nd version) script to play Jackie's Idle, Walking, & Running animations.
*/

public class JackieAnimationStates_V2 : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        bool forwardPressed = Input.GetKey("w") | Input.GetKey("a") | Input.GetKey("s") | Input.GetKey("d"); //States inputs for WASD
        bool runPressed = Input.GetKey("left shift"); //Input for Left Shift key
        
        if (forwardPressed)
        {
            //WALKING animation
            animator.SetFloat("Speed", 0.5f);
        }
        else if (runPressed && forwardPressed)
        {
            //RUNNING animation
            animator.SetFloat("Speed", 1);
        }
        else
        {
            //IDLE animation
            animator.SetFloat("Speed", 0);
        }


    }
}
