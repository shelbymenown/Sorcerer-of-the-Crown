using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireballExplosion : MonoBehaviour
{
    public AudioSource explosionSfx;
    public int damageAmount;

    // Start is called before the first frame update
    void Start()
    {
        explosionSfx.Play();
        Invoke("Remove", 1);
       
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

        }

    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
