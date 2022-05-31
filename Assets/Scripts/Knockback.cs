using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Knockback : MonoBehaviour
{
    //public float thrust;
    //public float knockTime;
    public AudioSource KnockbackSfx;
    private Rigidbody2D rb;
    public float knockbackDuration = 50f;
    public float knockbackPower = 1.0f;
    public float invulnerabilityTimer = 1f;
    public static bool isDisabled = false;
    //private static bool stopWall = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        KnockbackSfx = GameObject.Find("KnockbackSfx").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        /*
        Debug.Log(other);
        if (other.gameObject.CompareTag("wall") && isDisabled)
        {
            stopWall = true;
        }
        */

        if (other.gameObject.CompareTag("Enemy") && !isDisabled)
        {
            isDisabled = true;
            StartCoroutine(InvincibilityFrame(invulnerabilityTimer));
            StartCoroutine(KnockCo(knockbackDuration, knockbackPower, other.gameObject.transform));
        }
    }

    IEnumerator InvincibilityFrame(float delay)
    {
        yield return new WaitForSeconds(delay);
        isDisabled = false;
    }

    public IEnumerator KnockCo(float kbDuration, float kbPower, Transform obj)
    {
        float timer = 0;
        //Debug.LogError("Played");
        KnockbackSfx.Play();
        while (kbDuration > timer)
        {
            /*
            if (stopWall)
            {
                stopWall = false;
                timer = kbDuration;
                kbPower = -kbPower;
                Debug.Log("Did it");
            }
            */
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rb.AddForce(-direction * kbPower);
        }

        yield return 0;
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();

            if (enemy != null)
            {
                enemy.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(enemy));
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
        }
    }
    */
}
