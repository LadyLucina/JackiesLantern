using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/* Author: Stephanie M.
 * Details: This script is to control the Skulls movements using Unitys NavMesh component.
 * The enemies will wander their specific zone of the map. If they collide with the "EndOfZone" tagged GameObject,
 * they will turn and begin wandering their area once again. 
 */

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Stats")]
    private float wanderSpeed = 1.7f;
    private float chaseSpeed = 5f;
    public float standingDetectionRange = 10f;
    public float crouchedDetectionRange = 5f;

    [Header("Player Assignment")]
    [SerializeField] private Transform player;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Enemy Chase Check DEBUG ONLY")]
    public bool isChasing;

    private NavMeshAgent navMeshAgent;
    private Vector3 wanderDestination;

    [Header("Enemy Wander Check DEBUG ONLY")]
    public bool isWandering;

    [Tooltip("These are settings for the HINT text for the user")]
    public Text chaseText;
    private bool canDisplayChaseText = true;
    public float chaseTextCooldown = 20f;

    private bool ignorePlayer = false;
    public float ignorePlayerCooldown = 2f;
    private float ignorePlayerTimer;

    public void Start()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn points array is not assigned or empty. Please assign spawn points in the Inspector.");
            enabled = false;
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = wanderSpeed;

        SetRandomWanderDestination();
    }

    private void Update()
    {
        if (ignorePlayer)
        {
            ignorePlayerTimer -= Time.deltaTime;
            if (ignorePlayerTimer <= 0f)
            {
                ignorePlayer = false;
            }
        }

        float detectionRange = player.GetComponent<ThirdPersonMovement>().IsCrouching() ? crouchedDetectionRange : standingDetectionRange;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && !ignorePlayer)
        {
            isChasing = true;
            chaseSpeed = 5f;

            //Stop the NavMeshAgent from wandering
            navMeshAgent.isStopped = false;

            //Perform raycast to detect ground height
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                //Move towards the player, adjusting for ground height
                Vector3 targetPosition = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, chaseSpeed * Time.deltaTime);
            }

            //Calculate the direction to the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            //Calculate the rotation with fixed Y-axis rotation
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0f, directionToPlayer.z));

            //Set the rotation
            transform.rotation = targetRotation;

            //Move towards the player
            transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);

            if (canDisplayChaseText)
            {
                DisplayChaseText("You've been spotted!\nSprint to get away!");

                //Start the cooldown timer
                StartChaseTextCooldown();
            }
        }
        else if (isWandering)
        {
            //Resume wandering
            navMeshAgent.isStopped = false;
            wanderSpeed = 1.7f;

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
        if (other.CompareTag("EndOfZone"))
        {
            // Turn around and set a new random wander destination
            SetRandomWanderDestination();
        }

        if (other.CompareTag("Player"))
        {
            // Ignore the player for a cooldown period
            ignorePlayer = true;
            ignorePlayerTimer = ignorePlayerCooldown;
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

    #region Alert/Chase Text Methods
    private void DisplayChaseText(string text)
    {
        if (chaseText != null)
        {
            chaseText.text = text;
            chaseText.gameObject.SetActive(true);

            //Hide the text after 20 seconds
            Invoke("HideChaseText", 20f);
        }
    }

    private void HideChaseText()
    {
        if (chaseText != null)
        {
            chaseText.gameObject.SetActive(false);
        }
    }

    private void StartChaseTextCooldown()
    {
        canDisplayChaseText = false;
        Invoke("ResetChaseTextCooldown", chaseTextCooldown);
    }

    private void ResetChaseTextCooldown()
    {
        canDisplayChaseText = true;
    }
    #endregion
}