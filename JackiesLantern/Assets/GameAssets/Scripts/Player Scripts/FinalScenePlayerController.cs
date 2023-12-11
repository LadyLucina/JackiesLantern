using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M.
 * Details: This camera is the new player camera for the game. 
 * It is a traditional Third Person camera. However, the player can walk towards the camera if they choose
 * and have the original camera feel with it. 
 */

public class FinalScenePlayerController : MonoBehaviour
{
    #region How The Camera Feels Settings
    [Header("Camera Feel Settings")]
    public Transform player;           //The reference to the player to follow
    public float distance = 5.0f;      //Distance from the player
    public float height = 2.0f;        //Height above the player
    public float pitchLimit = 80.0f;   //Maximum pitch angle
    public float smoothSpeed = 5.0f;   //Smoothing speed for camera movement
    public Vector3 offset;             //Additional offset from the player
    #endregion

    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = transform;
    }

    void Update()
    {
        // Calculate the desired position of the camera
        Vector3 desiredPosition = player.position - player.forward * distance + Vector3.up * height + offset;

        // Smoothly interpolate between the current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(cameraTransform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        cameraTransform.position = smoothedPosition;

        // Look at the player's face
        Quaternion targetRotation = Quaternion.LookRotation(player.position - cameraTransform.position);
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetRotation, smoothSpeed * Time.deltaTime);

        // Limit the pitch angle of the camera to prevent it from facing down into the player
        float pitch = cameraTransform.eulerAngles.x;
        pitch = Mathf.Clamp(pitch, 0, pitchLimit);
        cameraTransform.eulerAngles = new Vector3(pitch, cameraTransform.eulerAngles.y, cameraTransform.eulerAngles.z);
    }
}