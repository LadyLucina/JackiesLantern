using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Author:  iHeartGameDev and Mya
 * https://youtu.be/FF6kezDQZ7s
 * 
 * The script to play Jackie's Idle, Walking, & Running animations.
 * 
*/


public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isIdleHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        //increases performance
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isIdleHash = Animator.StringToHash("isIdle");

    }

    // Update is called once per frame
    void Update()
    {
        bool isIdle = animator.GetBool(isIdleHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool forwardPressed = Input.GetKey("w") | Input.GetKey("a") | Input.GetKey("s") | Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");



       //Toggle running state when sprint key is pressed
        if (runPressed && Input.GetKeyDown(KeyCode.W) && !isRunning)
        {
            isRunning = !isRunning;
            animator.SetBool(isRunningHash, isRunning);
        }

        //When Player is walking, sprint animation can be toggled
        if (!isRunning && (forwardPressed && runPressed))
        {
            isRunning = true;
            animator.SetBool(isRunningHash, true);
        }

        //Player stops running, isRunning = false
        if (isRunning && (!forwardPressed || !runPressed))
        {
            isRunning = false;
            animator.SetBool(isRunningHash, false);
        }

        //if player moves (WASD), play Walk animation & isWalking is set to true
        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);

        }

        //If player stops moving isWalking is set to false & Walk animation stops
        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }


        //Sets player to Idle
        if (!isWalking && !isRunning)
        {
            animator.SetBool(isIdleHash, true);
        }
        else
        {
            animator.SetBool(isIdleHash, false);
        }

        //When shift is held before player starts moving, the walking nor running animatoin plays...



    }
}
