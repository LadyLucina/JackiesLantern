using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //References the Player
    public GameObject player;

    //Creates new Vector3 that determines the cameras position relative to the player
    [SerializeField]
    private Vector3 cameraOffset;

    void Update()
    {
        //Sets the camera position to the players position + the offset
        transform.position = player.transform.position + cameraOffset;
    }
}