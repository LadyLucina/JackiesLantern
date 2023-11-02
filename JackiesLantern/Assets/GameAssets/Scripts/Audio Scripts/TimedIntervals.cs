using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedIntervals : MonoBehaviour
{
    //Sets the max and min range in public
    [Tooltip("Min Range")]
    public float minInterval = 0.0f;

    [Tooltip("Max Range")]
    public float maxInterval = 1.0f;

    // Called once the wait is over
    public UnityEvent OnIntervalElapsed = new UnityEvent();

    private void Start()
    {
        StartCoroutine(IntervalRoutine());
    }
    //Plays the sound randomly between the set max and min range
    private IEnumerator IntervalRoutine()
    {
        float interval = Random.Range(minInterval, maxInterval);
        yield return new WaitForSeconds(interval);
        

        PlayEvent();
        StartCoroutine(IntervalRoutine());
    }

    private void PlayEvent()
    {
        OnIntervalElapsed.Invoke();
    }
}
