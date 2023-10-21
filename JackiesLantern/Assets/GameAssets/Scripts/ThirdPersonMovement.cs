using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Author: Joshua G.
 *Editor: Stephanie M.
 * Details: This script is the main charactor controller for the players movements.
 * It allows for controller compatibility when playing the game and includes physics
 * to prvent player ascension. 
 */

public class ThirdPersonMovement : MonoBehaviour
{
    private PlayerDamageController damageController; //Reference to the PlayerDamageController script
    public CharacterController controller;
    public Transform cam;

    [Header("Movement Speed")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float crouchSpeed = 3f;
    [SerializeField] private float sprintSpeed = 10f;

    //variable that sets turn speed
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public bool isCrouching = false;
    public bool isSprinting = false;

    //[Header("Ground Check")]
    //[SerializeField] private float groundRayLength = 1.0f;

    public void Start()
    {
        //Get a reference to the PlayerDamageController script
        damageController = GetComponent<PlayerDamageController>();
    }

    void Update()
    {
        if (!damageController.isStunned) //Check if the player is not stunned
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
                speed = 50f;
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
                    speed = 50;
                }
            }

            //Sets horizontal movement using "A" and "D" and arrow keys
            float horizontal = Input.GetAxis("Horizontal");

            //Sets vertical movement using "W" "S" and arrow keys
            float vertical = Input.GetAxis("Vertical");

            //This line prevents the player from moving up and down on the Y-axis and ensures movement only on the X and Z axes
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            //Handle movement
            if (!damageController.isFrozen) //Check if the player is not frozen
            {
                if (!isCrouching && !isSprinting)
                {
                    speed = 50;
                }

                if (direction.magnitude >= 0.1f)
                {
                    //Rotates the character with the camera
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    //Moves the character in the direction the camera is pointing
                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                    //Apply gravity
                    float gravity = Physics.gravity.y;
                    moveDir.y = gravity;

                    controller.Move(moveDir.normalized * speed * Time.deltaTime);

                }
            }
        }
    }

    //Reference to PlayerStun function in PlayerDamageController
    public void PlayerStun()
    {
        damageController.isStunned = true;
    }
}