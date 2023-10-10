using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* Author: Jonathon T.
 * Details: this script provides a way to create a system in Unity where events can be triggered at random time intervals within a specified range, 
 * offering flexibility for various gameplay and interaction scenarios.
 */

public class TimedIntervals : MonoBehaviour
{
    [Tooltip("Minimum time between intervals in seconds.")]
    public float minInterval = 0.0f;

    [Tooltip("Maximum time between intervals in seconds.")]
    public float maxInterval = 1.0f;

    //Event that gets triggered once the specified time interval has elapsed.
    public UnityEvent OnIntervalElapsed = new UnityEvent();


    private void Start()
    {
        //Start the interval routine.
        StartCoroutine(IntervalRoutine());
    }

    //Coroutine that manages the time intervals.
    private IEnumerator IntervalRoutine()
    {
        //Calculate a random interval duration within the specified range.
        float interval = Random.Range(minInterval, maxInterval);

        //Wait for the calculated interval duration.
        yield return new WaitForSeconds(interval);

        //Trigger the custom event.
        PlayEvent();

        //Start the interval routine again for continuous intervals.
        StartCoroutine(IntervalRoutine());
    }

    //Method to trigger the custom event.
    private void PlayEvent()
    {
        //Invoke the custom event, notifying other components.
        OnIntervalElapsed.Invoke();
    }
}