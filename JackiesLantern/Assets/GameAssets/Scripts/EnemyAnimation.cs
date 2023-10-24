using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public AnimationClip Take001;

    private Animation grabAnimation;

    void Awake()
    {
        // Reference to Animation component
        grabAnimation = GetComponent<Animation>();
    }

    public void OnCollisionEnter()
    {
        // Play the animation clip:
        grabAnimation.Play(Take001.name);
    }

}
