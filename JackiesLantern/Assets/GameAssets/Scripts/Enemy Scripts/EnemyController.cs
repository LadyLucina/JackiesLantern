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
    #region Enemy Stats
    [Header("Enemy Stats")]
    private float wanderSpeed = 1.7f;  //Speed during wandering
    private float chaseSpeed = 5f;     //Speed during chasing
    public float standingDetectionRange = 10f;  //Detection range while player is standing
    public float crouchedDetectionRange = 5f;   //Detection range while player is crouched

    private bool ignorePlayer = false;  //Flag to ignore the player temporarily
    public float ignorePlayerCooldown = 2f;  //Cooldown period for ignoring the player
    private float ignorePlayerTimer;  //Timer for ignoring the player
    #endregion

    #region Player Assignment
    [Header("Player Assignment")]
    [SerializeField] private Transform player;  //Reference to the player's transform
    #endregion

    #region Spawn Points & NavMesh
    [Header("Spawn Points")]
    public Transform[] spawnPoints;  //Array of spawn points for the enemy


    private NavMeshAgent navMeshAgent;  //Reference to the NavMeshAgent component
    private Vector3 wanderDestination;   //Destination for wandering
    #endregion

    #region Chase Text
    [Tooltip("Settings for the HINT text for the user")]
    public Text chaseText;  //Reference to the UI text for chase hints
    private bool canDisplayChaseText = true;  //Flag to control chase text display
    public float chaseTextCooldown = 20f;  //Cooldown period for displaying chase text
    #endregion

    #region DEBUG ONLY
    [Header("Enemy Chase Check DEBUG ONLY")]
    public bool isChasing;  //Debug flag indicating if the enemy is currently chasing

    [Header("Enemy Wander Check DEBUG ONLY")]
    public bool isWandering;  //Debug flag indicating if the enemy is currently wandering
    #endregion


    public void Start()
    {
        //Check if spawn points are assigned
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.Log("Spawn points array is not assigned or empty. Please assign spawn points in the Inspector.");
            enabled = false;
            return;
        }

        //Set the initial position and rotation of the enemy to a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        //Get the NavMeshAgent component and set the speed for wandering
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = wanderSpeed;

        //Set a random wander destination
        SetRandomWanderDestination();
    }

    private void Update()
    {
        //Update ignorePlayerTimer if ignorePlayer is true
        if (ignorePlayer)
        {
            ignorePlayerTimer -= Time.deltaTime;
            if (ignorePlayerTimer <= 0f)
            {
                ignorePlayer = false;
            }
        }

        //Calculate the detection range based on the player's crouch state
        float detectionRange = player.GetComponent<ThirdPersonMovement>().IsCrouching() ? crouchedDetectionRange : standingDetectionRange;

        //Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //Check if the player is within the detection range and not ignored
        if (distanceToPlayer <= detectionRange && !ignorePlayer)
        {
            //Set chase mode
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

            //Display chase text if it's allowed
            if (canDisplayChaseText)
            {
                DisplayChaseText("You've been spotted!\nSprint to get away!");

                //Start the cooldown timer for chase text
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
            //The enemy is not chasing or wandering
            isChasing = false;
        }
    }

    //Called when a collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        //Check if the collider is an "EndOfZone" trigger
        if (other.CompareTag("EndOfZone"))
        {
            //Turn around and set a new random wander destination
            SetRandomWanderDestination();
        }

        //Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            //Ignore the player for a cooldown period
            ignorePlayer = true;
            ignorePlayerTimer = ignorePlayerCooldown;
        }
    }

    //Set a random wander destination within the detection range
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
    //Display the chase text on the UI
    private void DisplayChaseText(string text)
    {
        if (chaseText != null)
        {
            chaseText.text = text;
            chaseText.gameObject.SetActive(true);

            //Hide the text after 2 seconds
            Invoke("HideChaseText", 2f);
        }
    }

    //Hide the chase text on the UI
    private void HideChaseText()
    {
        if (chaseText != null)
        {
            chaseText.gameObject.SetActive(false);
        }
    }

    //Start the cooldown for displaying chase text
    private void StartChaseTextCooldown()
    {
        canDisplayChaseText = false;
        Invoke("ResetChaseTextCooldown", chaseTextCooldown);
    }

    //Reset the cooldown for displaying chase text
    private void ResetChaseTextCooldown()
    {
        canDisplayChaseText = true;
    }
    #endregion
}