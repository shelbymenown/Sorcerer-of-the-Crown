using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = GameObject.FindWithTag("Player").transform.position;
        transform.position = new Vector3(position.x, position.y, -10);
    }
}
