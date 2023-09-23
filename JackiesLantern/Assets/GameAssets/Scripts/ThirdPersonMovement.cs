using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Joshua G.

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
   
    //references player camera
    public Transform cam;

    //Speed variable that dictates how fast the player moves
    public float speed = 6f;
    
    //variable that sets turn spped
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    void Update()
    {
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