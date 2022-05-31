using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public GameObject enemy;
    public Transform enemyPos;
    private float repeatRate = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("here");
            InvokeRepeating("EnemySpawner", 0.5f, repeatRate);
            Destroy(gameObject, 8);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void EnemySpawner()
    {
        Vector3 position = enemyPos.position;
        position.z = 0.0f;
        Instantiate(enemy, position, enemyPos.rotation);
    }
}
