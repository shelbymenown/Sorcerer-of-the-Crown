using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConeOfCold : MonoBehaviour
{
    public int damageAmount = 10;
    GameObject[] enemies;
    public bool isPlayer = true;
    int i = 0, k = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("Hitbox", .0166f);
        Invoke("Remove", .5f);
        Invoke("EndSlow", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

  
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (isPlayer && other.CompareTag("Enemy"))
        {
            
            other.gameObject.GetComponent<HealthSystem>().Damage(damageAmount);
            other.GetComponent<MoveTowardsPlayer>().speed = other.GetComponent<MoveTowardsPlayer>().speed * .5f;
            Array.Resize(ref enemies, i + 1);
            enemies[i] = other.gameObject;
            i++;
            
        }
    }


    public void Hitbox()
    {

        gameObject.GetComponent<EdgeCollider2D>().enabled = false;
    }

    public void Remove()
    {
        gameObject.GetComponent<EdgeCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    void EndSlow()
    {

        while(k < i)
        {
            if(enemies[k] != null)
            {
                enemies[k].GetComponent<MoveTowardsPlayer>().speed = enemies[k].GetComponent<MoveTowardsPlayer>().speed * 2f;
                k++;
            }
            else
            {
                k++;
            }
            
        }
        
        
        Destroy(gameObject);
    }

}


