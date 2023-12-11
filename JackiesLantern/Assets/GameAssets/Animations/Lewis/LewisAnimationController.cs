using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LewisAnimationController : MonoBehaviour
{
    private Animator myAnim;
    public LewisController lewisController; //Reference to the FarmerController script

    //Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        lewisController = GetComponent<LewisController>();
    }

    //Update is called once per frame
    void Update()
    {
        bool isChasing = lewisController.isChasing; //State is dependent on the current state in LewisController script

        if (isChasing)
        {
            myAnim.SetBool("isCrawling", true);
        }
    }
}
