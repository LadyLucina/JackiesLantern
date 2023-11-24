using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Author: Stephanie M.
 * Details: This script is to control the Farmer and Boss's movements using Unitys NavMesh component.
 * The enemies will wander their specific zone of the map. If they collide with the "EndOfZone" tagged GameObject,
 * they will turn and begin wandering their area once again. 
 */

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float moveSpeed = 5f;
    public float standingDetectionRange = 10f;
    public float crouchedDetectionRange = 5f;

    [Header("Player Assignment")]
    [SerializeField] private Transform player;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Enemy Chase Check")]
    [SerializeField] private bool isChasing = false; //Used during visual debugging. DO NOT TOUCH WITHIN INSPECTOR

    private NavMeshAgent navMeshAgent;
    private Vector3 wanderDestination;

    [Header("Enemy Wander Check")]
    [SerializeField] private bool isWandering = true; //Used during visual debugging. DO NOT TOUCH WITHIN INSPECTOR


    public void Start()
    {
        //Check if spawnPoints array is empty
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.Log("Spawn points array is not assigned or empty. Please assign spawn points in the Inspector.");
            enabled = false;
            return;
        }

        //Randomly choose a spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        //Initialize NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;

        //Set the initial destination to a random point within the detection range
        SetRandomWanderDestination();
    }

    public void Update()
    {
        float detectionRange = player.GetComponent<ThirdPersonMovement>().IsCrouching() ? crouchedDetectionRange : standingDetectionRange;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;

            //Stop the NavMeshAgent from wandering
            navMeshAgent.isStopped = true;

            //Perform raycast to detect ground height
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                //Move towards the player, adjusting for ground height
                Vector3 targetPosition = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }

            //Rotate to face the player
            transform.LookAt(player);

            //Move towards the player
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else if (isWandering)
        {
            //Resume wandering
            navMeshAgent.isStopped = false;

            //Check if the enemy has reached the destination
            if (navMeshAgent.remainingDistance < 0.1f)
            {
                SetRandomWanderDestination();
            }
        }
        else
        {
            isChasing = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if entered trigger zone with the tag "End of Zone"
        if (other.CompareTag("EndOfZone"))
        {
            //Turn around and set a new random wander destination
            SetRandomWanderDestination();
        }
    }

    private void SetRandomWanderDestination()
    {
        //Set a random point within the detection range as the wander destination
        Vector3 randomPoint = Random.insideUnitSphere * standingDetectionRange;
        NavMesh.SamplePosition(transform.position + randomPoint, out NavMeshHit hit, standingDetectionRange, NavMesh.AllAreas);
        wanderDestination = hit.position;

        //Set the NavMeshAgent destination to the wander destination
        navMeshAgent.SetDestination(wanderDestination);
    }
}
