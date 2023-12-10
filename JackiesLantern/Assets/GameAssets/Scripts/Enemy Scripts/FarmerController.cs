using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/* Author: Stephanie
 * Details: This script is a modified version of the EnemyController script to be dedicated to the Farmer only.
 * It includes the same functions as before but will also allow animation support.
 */

public class FarmerController : MonoBehaviour
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
    public bool isChasing; //Used during visual debugging. DO NOT TOUCH WITHIN INSPECTOR

    private NavMeshAgent navMeshAgent;
    private Vector3 wanderDestination;

    [Header("Enemy Wander Check DEBUG ONLY")]
    public bool isWandering; //Used during visual debugging. DO NOT TOUCH WITHIN INSPECTOR

    [Tooltip("These are settings for the HINT text for the user")]
    public Text chaseText; //Reference to the UI Text component
    private bool canDisplayChaseText = true; //Flag to check if chase text can be displayed
    private float chaseTextCooldown; //Cooldown time for displaying chase text


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

    private void Update()
    {
        float detectionRange = player.GetComponent<ThirdPersonMovement>().IsCrouching() ? crouchedDetectionRange : standingDetectionRange;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
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
                DisplayChaseText("You've been spotted!" + "\nSprint to get away!");

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

