using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LewisController : MonoBehaviour
{
    #region Lewis's Stats and NavMesh
    [Header("Enemy Stats")]
    public float chaseSpeed; //Speed at which the enemy chases the player
    public float standingDetectionRange = 10f;  //Detection range while player is standing
    public float crouchedDetectionRange = 5f;   //Detection range while player is crouched
    private NavMeshAgent navMeshAgent;  //Reference to the NavMeshAgent component
    #endregion

    #region Player Assignment
    [Header("Player Assignment")]
    [SerializeField] private Transform player;  //Reference to the players transform
    #endregion

    #region Spawn Point
    [Header("Spawn Point")]
    public Transform[] lewisSpawnPoint;  //Array of spawn points for the enemy
    #endregion

    #region Chase Variables
    [Tooltip("Settings for the HINT text for the user")]
    public Text chaseText; // Reference to the UI Text component
    private bool canDisplayChaseText = true; // Flag to check if chase text can be displayed
    #endregion

    #region DEBUG ONLY
    [Header("Enemy Chase Check DEBUG ONLY")]
    [SerializeField] public bool isChasing; // Used during visual debugging. DO NOT TOUCH WITHIN INSPECTOR
    #endregion

    void Start()
    {
        //Initialize NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = chaseSpeed; //Set the initial chase speed
        canDisplayChaseText = true; //Enable chase text display
    }

    void Update()
    {
        float detectionRange = player.GetComponent<ThirdPersonMovement>().IsCrouching() ? crouchedDetectionRange : standingDetectionRange;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            //If player is within detection range, start chasing
            ChaseJackie();
        }
    }

    #region Chase Methods

    public void ChaseJackie()
    {
        isChasing = true;
        chaseSpeed = 6.5f; //CHANGE HERE - Adjust the chase speed as needed  (6.8)

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
            //Display chase text to instruct the player
            DisplayChaseText("RUN AWAY!");

            //Start the cooldown timer
            StartChaseTextCooldown();
        }
    }

    private void DisplayChaseText(string text)
    {
        if (chaseText != null)
        {
            chaseText.text = text;
            chaseText.gameObject.SetActive(true);
        }
    }

    private void StartChaseTextCooldown()
    {
        canDisplayChaseText = false; //Disable chase text display temporarily
    }

    #endregion
}