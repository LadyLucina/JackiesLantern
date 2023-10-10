using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    
    public Vector3 rotationSpeed = new Vector3(0f, 30f, 0f); //Rotation speed in degrees per second

    //Update is called once per frame
    void Update()
    {
        //Rotate the object around its local axes
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
