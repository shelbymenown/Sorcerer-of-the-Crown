using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public GameObject player;
    public AudioSource enemyNoise;
    public float speed = 3.5f;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }



    void Move()
    {
        Vector2 playerAngle = player.transform.position - gameObject.transform.position;

        playerAngle = playerAngle.normalized;

        rb.velocity = playerAngle * speed;

        if (enemyNoise != null)
            Invoke("EnemySfx", 2.0f);
    }

    void EnemySfx()
    {
        Debug.Log("Here");
        AudioSource.PlayClipAtPoint(enemyNoise.clip, gameObject.transform.position);
    }
}