using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M.
 * Details: This is for the final scene camera that will be in style of our OG camera.
 * It is strictly dedicated for the Lewis Chase Scene!
 */

public class FinalScenePlayerController : MonoBehaviour
{
    public Transform player; //Reference to the player's transform
    public float smoothSpeed = 0.125f; //Speed of camera follow
    public Vector3 offset = new Vector3(0f, 2f, -5f); //Adjust the offset as needed

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        //Calculate the desired position for the camera with the offset
        Vector3 desiredPosition = player.position + offset;

        //Use Mathf.Lerp to smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        //Update the camera position
        transform.position = smoothedPosition;
    }
}