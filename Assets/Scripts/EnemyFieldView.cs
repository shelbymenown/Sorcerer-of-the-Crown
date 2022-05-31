using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldView : MonoBehaviour
{
    private EnemyAI path;
    private GameObject player;

    public float activationDistance = 3.0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        path = GetComponent<EnemyAI>();
    }

    void Update()
    {
        if ((gameObject.transform.position - player.transform.position).magnitude < activationDistance)
        {
            //Debug.LogError("In Field View");
            path.enabled = true;
        }
        else
        {
            path.enabled = false;
        }
    }
}
