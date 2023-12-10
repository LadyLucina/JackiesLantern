using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LewisController : MonoBehaviour
{
    [Header("Enemy Stats")]
    private float chaseSpeed = 5f;
    public float standingDetectionRange = 10f;
    public float crouchedDetectionRange = 5f;

    [Header("Player Assignment")]
    [SerializeField] private Transform player;

    [Header("Spawn Point")]
    public Transform[] lewisSpawnPoint;

    [Header("Enemy Chase Check DEBUG ONLY")]
    [SerializeField] public bool isChasing; //Used during visual debugging. DO NOT TOUCH WITHIN INSPECTOR

    private NavMeshAgent navMeshAgent;

    [Tooltip("These are settings for the HINT text for the user")]
    public Text chaseText; //Reference to the UI Text component
    private bool canDisplayChaseText = true; //Flag to check if chase text can be displayed
    private float chaseTextCooldown; //Cooldown time for displaying chase text


    void Start()
    {
        //Initialize NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = chaseSpeed;
    }


    void Update()
    {
        float detectionRange = player.GetComponent<ThirdPersonMovement>().IsCrouching() ? crouchedDetectionRange : standingDetectionRange;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            ChaseJackie();
        }
    }

    #region Chase Methods

    public void ChaseJackie()
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
            DisplayChaseText("Run away from Lewis!" + "\nGet to your pumpkin patch!");

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
