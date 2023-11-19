using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideEnable : MonoBehaviour
{
    private Target test;
    void Start()
    {
        test = GetComponent<Target>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            test.enabled = false;
        }
    }
}
