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


    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public bool isCrouching = false;
    public bool isSprinting = false;
    private bool isWalkingBackward = false;

    //Toggles
    private bool superSpeedEnabled = false;
    private bool isInvincible = false;

    //Store checkpoint position
    private Vector3 respawnPoint;

    void Start()
    {
        //Get a reference to the PlayerDamageController script
        damageController = GetComponent<PlayerDamageController>();

        //Set initial respawn point
        respawnPoint = transform.position;
    }

    void Update()
    {
        
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        //Check the current scene name
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        //Detect crouch input (keyboard: Left Control, controller: B button on controller)
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetButtonDown("Crouch"))
        {
            isCrouching = !isCrouching;
            speed = isCrouching ? crouchSpeed : 50f;
            if (isCrouching)
            {
                isSprinting = false;
            }
        }

        //Detect sprint input (keyboard: Left Shift, controller: Right Trigger)
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Sprint"))
        {
            isSprinting = !isSprinting;
            speed = isSprinting ? sprintSpeed : (isCrouching ? crouchSpeed : 50f);
            if (isSprinting)
            {
                isCrouching = false;
            }
        }

        //Sets horizontal movement using "A" and "D" and arrow keys
        float horizontal = Input.GetAxisRaw("Horizontal");

        //Sets vertical movement using "W" "S" and arrow keys
        float vertical = Input.GetAxisRaw("Vertical");

        //Get input direction
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical);

        //Check if the player is walking backward
        isWalkingBackward = vertical < 0;

        if (inputDirection.magnitude >= 0.1f)
        {
            //Calculate the target angle based on the camera direction
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            //Conditionally apply rotation only if not walking backward or in Level 4
            if (!isWalkingBackward || sceneName == "Level 4")
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }

            //Calculate the movement direction based on the rotated angle
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //Apply gravity
            float gravity = Physics.gravity.y;
            moveDir.y = gravity;

            //Move the player in the calculated direction
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }


#if UNITY_EDITOR
    void HandleCheats()
    {
        //Toggle Invincibility
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInvincible = !isInvincible;
        }

        //Toggle super speed
        if (Input.GetKeyDown(KeyCode.Z))
        {
            superSpeedEnabled = !superSpeedEnabled;
        }

        //Toggle respawn
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }

        //Level loading
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
    }
#endif

    void Respawn()
    {
        transform.position = respawnPoint;
    }

    void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    public bool IsCrouching()
    {
        return isCrouching;
    }
}