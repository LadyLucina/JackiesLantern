using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:  iHeartGameDev and Mya
//https://youtu.be/FF6kezDQZ7s


public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        //increases performance
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

    }

    // Update is called once per frame
    void Update()
    { 
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool forwardPressed = Input.GetKey("w") | Input.GetKey("a") | Input.GetKey("s") | Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");



        //Player is walking(not running) then presses L shift, isRunning is set to true & running animation plays
        if (!isRunning && (forwardPressed && runPressed))
        {
            animator.SetBool(isRunningHash, true);
        }

        //Player is walking/running then stops walking/running isRunning becomes false
        if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }

        //if player moves (WASD), play Walk animation & isWalking is set to true
        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);

        }

        //If player stops moving isWalking is set to false & Walk animation stops
        if (isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }

    }
}
