using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/* Author: Stephanie
 * Details: This script is a modified version of the EnemyController script to be dedicated to the Farmer only.
 * It includes the same functions as before but will also allow animation support.
 */

public class FarmerController : MonoBehaviour
{
    #region Farmer Stats & NavMesh
    [Header("Farmer Stats")]
    private float wanderSpeed = 1.7f; //Speed during wandering
    private float chaseSpeed = 6f;    //Speed during chasing
    public float standingDetectionRange = 10f;  //Detection range while player is standing
    public float crouchedDetectionRange = 5f;   //Detection range while player is crouched
    
    private NavMeshAgent navMeshAgent;  // Reference to the NavMeshAgent component
    private Vector3 wanderDestination;

    private bool ignorePlayer = false;  //Flag to ignore the player temporarily
    public float ignorePlayerCooldown = 2f;  //Cooldown period for ignoring the player
    private float ignorePlayerTimer;  //Timer for ignoring the player
    #endregion

    #region Player Assignment
    [Header("Player Assignment")]
    [SerializeField] private Transform player;  //Reference to the players transform
    #endregion

    #region Spawn Points
    [Header("Spawn Points")]
    public Transform[] spawnPoints;  //Array of spawn points for the enemy
    #endregion

    #region DEBUG ONLY
    [Header("Enemy Chase Check DEBUG ONLY")]
    public bool isChasing; //Used during visual debugging. DO NOT TOUCH WITHIN INSPECTOR

    [Header("Enemy Wander Check DEBUG ONLY")]
    public bool isWandering; //Used during visual debugging. DO NOT TOUCH WITHIN INSPECTOR
    #endregion

    #region Chase Text Variables
    [Tooltip("Settings for the HINT text for the user")]
    public Text chaseText; //Reference to the UI Text component
    private bool canDisplayChaseText = true; //Flag to check if chase text can be displayed
    private float chaseTextCooldown = 20f; //Cooldown time for displaying chase text
    #endregion

    //Called when the script starts
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
        navMeshAgent.speed = wanderSpeed;

        //Reset the wander state
        SetRandomWanderDestination();
    }

    //Called every frame
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
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //Check if the player is within the detection range
        if (distanceToPlayer <= detectionRange && !ignorePlayer)
        {
            isChasing = true;
            chaseSpeed = 6f;

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

    //Called when a collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        //Check if entered trigger zone with the tag "End of Zone"
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

            //Hide the text after 20 seconds
            Invoke("HideChaseText", 5f);
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