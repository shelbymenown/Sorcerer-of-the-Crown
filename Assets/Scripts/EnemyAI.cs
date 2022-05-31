using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    private float enemyScaleX;
    private float enemyScaleY;
    private float healthScaleX;
    private float healthScaleY;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndofPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;
        
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            return;
        }
        else
        {
            reachedEndofPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;


        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);


        //Debug.LogError("Current Waypoint: " + currentWaypoint);
        //Debug.LogError(rb.velocity);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
            if (rb.velocity.magnitude < 2.0 && rb.velocity.magnitude > 0.001 && currentWaypoint <= 2)
                rb.AddForce(force, ForceMode2D.Impulse);
        }
        if (enemyGFX != null)
        {
            enemyScaleX = enemyGFX.transform.localScale.x;
            enemyScaleY = enemyGFX.transform.localScale.y;
            healthScaleX = enemyGFX.GetChild(0).GetChild(0).transform.localScale.x;
            healthScaleY = enemyGFX.GetChild(0).GetChild(0).transform.localScale.y;

            if (force.x >= 0.01f)
            {
                enemyGFX.localScale = new Vector3(-Mathf.Abs(enemyScaleX), enemyScaleY, 1f);
                enemyGFX.transform.GetChild(0).GetChild(0).localScale = new Vector3(-Mathf.Abs(healthScaleX), healthScaleY, 1f);
            }
            else if (rb.velocity.x <= -0.01f)
            {
                enemyGFX.localScale = new Vector3(Mathf.Abs(enemyScaleX), enemyScaleY, 1f);
                enemyGFX.transform.GetChild(0).GetChild(0).localScale = new Vector3(Mathf.Abs(healthScaleX), healthScaleY, 1f);
            }
        }
    }
}
