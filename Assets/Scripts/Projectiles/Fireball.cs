using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject fireballExplosionPrefab;
    public int damageAmount;
    public Transform center;
    public bool isPlayer = true;

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
        if ((isPlayer && other.CompareTag("Enemy")) || (!isPlayer && other.CompareTag("Player")))
        {
            other.gameObject.GetComponent<HealthSystem>().Damage(damageAmount);
            GameObject fireball = Instantiate(fireballExplosionPrefab, center.position, center.rotation);
            //AudioSource pewPewSfx = fireballExplosionPrefab.GetComponent<AudioSource>();
            //AudioSource.PlayClipAtPoint(pewPewSfx.clip, center.position);
            Destroy(gameObject);
        }
        else if (other.CompareTag("wall") || other.CompareTag("obstacle"))
        {
            GameObject fireball = Instantiate(fireballExplosionPrefab, center.position, center.rotation);
            //AudioSource pewPewSfx = fireballExplosionPrefab.GetComponent<AudioSource>();
            //AudioSource.PlayClipAtPoint(pewPewSfx.clip, center.position);
            Destroy(gameObject);
        }
    }
}
