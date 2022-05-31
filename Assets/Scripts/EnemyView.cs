using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private float viewDistance = 20f;
    private ShooterEnemy shooterEnemy;
    private GameObject player;
    private bool playerInRange = false;
    private MoveTowardsPlayer moveTowardsPlayer;
    private EnemyAI path; 

    private void Awake()
    {
        moveTowardsPlayer = GetComponent<MoveTowardsPlayer>();
        shooterEnemy = GetComponent<ShooterEnemy>();
        player = GameObject.FindGameObjectWithTag("Player");
        path = GetComponent<EnemyAI>();
    }

    private void FixedUpdate()
    {
        playerInRange = CheckPlayerInRange();
        if (playerInRange)
        {
            if (shooterEnemy != null)
                shooterEnemy.enabled = true;
            if (moveTowardsPlayer != null)
                moveTowardsPlayer.enabled = true;
            if (path != null)
                path.enabled = true;
        }
        else
        {
            if (shooterEnemy != null)
                shooterEnemy.enabled = false;
            if (moveTowardsPlayer != null)
                moveTowardsPlayer.enabled = false;
            if (path != null)
                path.enabled = false;
        }
    }

    private bool CheckPlayerInRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, viewDistance);
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);

        if (hit.collider)
        {
            //Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("Player"))
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        return false;
    }
}
