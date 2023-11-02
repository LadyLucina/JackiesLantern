using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackieAnimationStates : MonoBehaviour
{
    private Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if sprinting
        if (Input.GetButton("Sprint") && (Input.GetAxisRaw("Horizontal") > .01f || Input.GetAxisRaw("Horizontal") < -.01f || Input.GetAxisRaw("Vertical") > .01f || Input.GetAxisRaw("Vertical") < -.01f))
        {
            myAnim.SetBool("isRunning", true);
            myAnim.SetBool("isIdle", false);
            myAnim.SetBool("isWalking", false);
        }
        //if any movement--other special movement, like crouch would go between these two
        else if (Input.GetAxisRaw("Horizontal") > .01f || Input.GetAxisRaw("Horizontal") < -.01f || Input.GetAxisRaw("Vertical") > .01f || Input.GetAxisRaw("Vertical") < -.01f)
        {
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isIdle", false);
            myAnim.SetBool("isWalking", true);
        }
        //if no movement
        else
        {
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isIdle", true);
            myAnim.SetBool("isWalking", false);
        }
        
    }
}
