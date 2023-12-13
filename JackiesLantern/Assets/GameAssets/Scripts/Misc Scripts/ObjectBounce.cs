using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBounce : MonoBehaviour
{
    public float bounceHeight = 1.0f;   //The height of the bouncing effect.
    public float bounceSpeed = 1.0f;    //The speed of the bouncing effect.

    private Vector3 initialPosition;    //The initial position of the GameObject.
    private float groundLevel;          //The ground level based on the initial position.

    void Start()
    {
        //Store the initial position of the GameObject
        initialPosition = transform.position;

        //Set the ground level based on the initial position
        groundLevel = initialPosition.y;
    }

    void Update()
    {
        //Calculate the vertical position based on time using a sine function for bouncing effect
        float verticalOffset = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;

        //Update the position of the GameObject with the bouncing effect
        transform.position = new Vector3(initialPosition.x, groundLevel + verticalOffset, initialPosition.z);
    }
}