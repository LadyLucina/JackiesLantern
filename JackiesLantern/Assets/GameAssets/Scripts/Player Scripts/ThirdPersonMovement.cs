using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Toggles
    private bool superSpeedEnabled = false;
    private bool isInvincible = false; 


    // Store checkpoint position
    private Vector3 respawnPoint;

    public void Start()
    {
        //Get a reference to the PlayerDamageController script
        damageController = GetComponent<PlayerDamageController>();

        // Set initial respawn point
        respawnPoint = transform.position;
    }

    void Update()
    {
        if (!damageController.isStunned) //Check if the player is not stunned
        {
            // Detect crouch input (keyboard: Left Control, controller: B button on controller)
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

            /*  -----------------
             *   CHEAT KEY BINDS
             *  -----------------
             */

            //Toggle Invinciblity
            if (Input.GetKeyDown(KeyCode.I))
            {
                isInvincible = !isInvincible;
            }

            // Toggle super speed
            if (Input.GetKeyDown(KeyCode.Z))
            {
                superSpeedEnabled = !superSpeedEnabled;
            }

            // Toggle respawn
            if (Input.GetKeyDown(KeyCode.R))
            {
                Respawn();
            }

            // Level loading
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                LoadLevel("Level 1");
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                LoadLevel("Level 2");
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                LoadLevel("Level 3");
            }

           /*  ----------------
            *   END OF TOGGLES
            *  ----------------
            */

            //Sets horizontal movement using "A" and "D" and arrow keys
            float horizontal = Input.GetAxisRaw("Horizontal");

            //Sets vertical movement using "W" "S" and arrow keys
            float vertical = Input.GetAxisRaw("Vertical");

            //This line prevents the player from moving up and down on the Y-axis and ensures movement only on the X and Z axes
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            //Handle movement
            if (!damageController.isFrozen) //Check if the player is not frozen
            {
                if (!isCrouching && !isSprinting && !superSpeedEnabled)
                {
                    speed = 50;
                }
                else if (superSpeedEnabled)
                {
                    speed = 50 * 2; // Adjust as needed
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

    private void Respawn()
    {
        transform.position = respawnPoint;
    }

    private void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }
}