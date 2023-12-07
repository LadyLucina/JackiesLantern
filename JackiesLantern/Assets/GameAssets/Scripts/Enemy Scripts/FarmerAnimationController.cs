using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerAnimationController : MonoBehaviour
{
    private Animator myAnim;
    public FarmerController farmerController; //Reference to the FarmerController script

    void Start()
    {
        myAnim = GetComponent<Animator>();
        farmerController = GetComponent<FarmerController>();
    }

    void Update()
    {
        bool isChasing = farmerController.isChasing; //State is dependent on the current state in FarmerController script
        bool isWandering = farmerController.isWandering; //State is dependent on the current state in FarmerController script

        //If chasing player
        if (isChasing)
        {
            myAnim.SetBool("isChasing", true);
            myAnim.SetBool("isIdle", false);
            myAnim.SetBool("isWandering", false);
        }
        //If wandering
        else if (isWandering)
        {
            myAnim.SetBool("isChasing", false);
            myAnim.SetBool("isIdle", false);
            myAnim.SetBool("isWandering", true);
        }
       /* //If idle
        else
        {
            myAnim.SetBool("isChasing", false);
            myAnim.SetBool("isIdle", true);
            myAnim.SetBool("isWandering", false);
        }*/
    }
}