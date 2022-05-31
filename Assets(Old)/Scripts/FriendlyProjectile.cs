using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyProjectile : MonoBehaviour
{
    public float damageAmount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<HealthSystem>().Damage(damageAmount);

            Destroy(gameObject);
        }
        else if (other.CompareTag("wall") || other.CompareTag("obstacle"))
        {
            Destroy(gameObject);
        }

    }
}
