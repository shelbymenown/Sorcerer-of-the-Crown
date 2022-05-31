using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    public int damageAmount = 10;
    public int canHit = 1;
    public static int imperviousHitOff = 1;
    float cooldown = 1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canHit == 1)
        {   
            if(canHit == 1 && imperviousHitOff == 1)
            {

                canHit = 0;
                Invoke("HitReset", cooldown);
                collision.gameObject.GetComponent<HealthSystem>().Damage(damageAmount);
                   
                //Invoke("HitReset", cooldown);
            }
                
        }
    }
    

    void HitReset()
    {
        canHit = 1;
    }

}
