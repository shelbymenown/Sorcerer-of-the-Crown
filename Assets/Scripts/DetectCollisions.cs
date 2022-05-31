using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DetectCollisions : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnTriggerEnter2D(Collider2D enemy)
    {
        
    }*/


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "wall")
        {
           if(gameObject.GetComponentInParent<PlayerController>().dodging == 1)
           {

                gameObject.GetComponentInParent<PlayerController>().rigidBody.position = gameObject.GetComponentInParent<PlayerController>().rigidBody.position - gameObject.GetComponentInParent<PlayerController>().movement * 25 * Time.fixedDeltaTime;
           }
            gameObject.GetComponentInParent<PlayerController>().movement.x = 0;
            gameObject.GetComponentInParent<PlayerController>().movement.y = 0;
        }
    }
}
