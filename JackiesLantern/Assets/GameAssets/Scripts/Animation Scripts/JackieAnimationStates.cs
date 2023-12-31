using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JackieAnimationStates : MonoBehaviour
{
    private Animator myAnim;
    private bool isSprinting = false;

    void Start()
    {
        myAnim = this.GetComponent<Animator>();
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        //Check if the player wants to toggle sprint
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Toggle sprinting state
            isSprinting = !isSprinting;
        }

        //Set animation states based on the sprinting state and movement input
        if (isSprinting && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) ||  sceneName == "Level 4")
        {
            myAnim.SetBool("isRunning", true);
            myAnim.SetBool("isIdle", false);
            myAnim.SetBool("isWalking", false);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isIdle", false);
            myAnim.SetBool("isWalking", true);
        }
        //If no movement
        else
        {
            myAnim.SetBool("isRunning", false);
            myAnim.SetBool("isIdle", true);
            myAnim.SetBool("isWalking", false);
        }
    }
}
