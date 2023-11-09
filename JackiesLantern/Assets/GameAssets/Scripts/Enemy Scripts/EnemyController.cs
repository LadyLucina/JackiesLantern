using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float moveSpeed = 5f;
    public float detectionRange = 10f;

    [Header("Player Assignment")]
    [SerializeField] private Transform player;

    private bool isChasing = false;

    public void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    public void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not found. Make sure the player has the 'Player' tag.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;

            //Rotate to face the player
            transform.LookAt(player);

            //Move towards the player
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            isChasing = false;
        }
    }
}