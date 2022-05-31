using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public GameObject enemy;
    public GameObject newInstance;
    public List<GameObject> bossList = new List<GameObject>();
    public Transform enemyPos;
    public float repeatRate = 15f;
    public int spawnAmount = 6;
    private int count = 0;
    private bool isInitialized = false;
    private bool skipForNow = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool isNull(GameObject obj)
    {
        Debug.LogError("In predicate: " + (obj == null));
        return obj == null;
    }

    void Update()
    {
        bossList.RemoveAll(isNull);
        //Debug.LogError(bossList.Contains(null));
        if (isInitialized && (bossList.Count == 0) && (count < spawnAmount))
        {
            skipForNow = false;
            EnemySpawner();
            skipForNow = true;
        }

        if (count >= spawnAmount && (bossList.Count == 0))
        {
            //Debug.LogError("checks");
            GameObject.Find("Room 3").GetComponent<FadeOut>().enabled = true;
            Destroy(gameObject, 0);
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("here");
            InvokeRepeating("EnemySpawner",.5f, repeatRate);
            //Destroy(gameObject, repeatRate * spawnAmount);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void EnemySpawner()
    {
        Vector3 position = enemyPos.position;
        position.z = 0.0f;
        
        if (!skipForNow)
            newInstance = Instantiate(enemy, position, enemyPos.rotation);
        
        count++;
        isInitialized = true;
        skipForNow = false;

        bossList.Add(newInstance);
        foreach (var x in bossList)
        {
            Debug.LogError(x.ToString());
        }
    }
}
