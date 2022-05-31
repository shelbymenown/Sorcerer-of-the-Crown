using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbOfAnnihilation : MonoBehaviour
{
    public AudioSource blackholeSfx;
    public GameObject orbPrefab;
    public Transform center;
    public int damageAmount = 25;
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
        if (isPlayer && other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<HealthSystem>().Damage(damageAmount);
            //GameObject orbResidual = Instantiate(orbPrefab, center.position, center.rotation);
            //AudioSource pewPewSfx = orbPrefab.GetComponent<AudioSource>();
            //AudioSource.PlayClipAtPoint(pewPewSfx.clip, center.position);
            blackholeSfx.Play();
            //Destroy(orbResidual, 1.0f);
        }
        else if (other.CompareTag("obstacle"))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
}
