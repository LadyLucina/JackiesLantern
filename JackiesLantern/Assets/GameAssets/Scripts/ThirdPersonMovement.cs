using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Author: Joshua G.
 *Editor: Stephanie M.
 * Note: Added isCrouching and isSprinting to the script so that the player is able to do both.
 * Included compatibility for controller support as well.
 */

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    //references player camera
    public Transform cam;

    //Speed variable that dictates how fast the player moves
    public float speed;

    //Declare crouchSpeed and sprintSpeed
    public float crouchSpeed = 3f;
    public float sprintSpeed = 10f;
    
    //variable that sets turn spped
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public bool isCrouching = false;
    public bool isSprinting = false;

    void Update()
    {
        //Detect crouch input (keyboard: Left Control, controller: B button on controller)
        bool isCrouchInput = Input.GetKey(KeyCode.LeftControl) || Input.GetButton("Crouch");
        if (isCrouchInput && !isCrouching)
        {
            isCrouching = true;
            speed = crouchSpeed;
        }
        else if (!isCrouchInput && isCrouching)
        {
            isCrouching = false;
            speed = 100f;
        }


        //Detect sprint input (keyboard: Left Shift, controller: Right Trigger)
        bool isSprintInput = Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Sprint");

        if (isSprintInput && !isSprinting)
        {
            isSprinting = true;
            speed = sprintSpeed;
        }
        else if (!isSprintInput && isSprinting)
        {
            isSprinting = false;
            if (!isCrouching)
            {
                speed = 100f;
            }
        }

        //sets horizontal movement using "A" and "D" and arrow keys
        float horizontal = Input.GetAxisRaw("Horizontal");

        //sets vertical movement using "W" "S" and arrow keys
        float vertical = Input.GetAxisRaw("Vertical");

        //this line prevents player from moving up and down on the Y axis and ensures that in only travels on X and Z axis
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        if (direction.magnitude >= 0.1f)
        {
            //rotates character with camera
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //moves character in the direction camera is pointing
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //this allows the player to actually move 
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}