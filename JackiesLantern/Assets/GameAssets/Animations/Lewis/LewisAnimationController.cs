using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Stephanie M
 * Details: 
 */

public class LewisAnimationController : MonoBehaviour
{
    private Animator myAnim; //Reference to the Animator component.
    public LewisController lewisController; //Reference to the FarmerController script.

    void Start()
    {
        myAnim = GetComponent<Animator>(); //Assign the Animator component of this GameObject.
        lewisController = GetComponent<LewisController>(); //Assign the LewisController script of this GameObject.
    }

    void Update()
    {
        bool isChasing = lewisController.isChasing; //Get the 'isChasing' state from the LewisController script.

        if (isChasing)
        {
            myAnim.SetBool("isCrawling", true); //Set the 'isCrawling' parameter in the Animator to true if chasing.
        }
    }
}