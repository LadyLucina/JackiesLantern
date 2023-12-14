using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBounce : MonoBehaviour
{
    public float bounceHeight = 1.0f;
    public float bounceSpeed = 1.0f;

    private Vector3 initialPosition;
    private float groundLevel;

    void Start()
    {
        //Store the initial position of the GameObject
        initialPosition = transform.position;

        //Set the ground level based on the initial position
        groundLevel = initialPosition.y;
    }

    void Update()
    {
        //Calculate the vertical position based on time
        float verticalOffset = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;

        //Update the position of the GameObject with the bouncing effect
        transform.position = new Vector3(initialPosition.x, groundLevel + verticalOffset, initialPosition.z);
    }
}