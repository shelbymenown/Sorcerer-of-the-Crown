using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    public Animator animator;
    public AudioSource bulletSfx;
    public GameObject player;
    public GameObject bulletPrefab;
    public float delay = 5.0f;
    private float timer = 0.0f;
    private bool canEnter = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        bulletSfx = GameObject.Find("BulletSfx").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            this.GetComponent<ShooterEnemy>().enabled = false;
            animator.SetBool("Timed", true);
            Debug.Log("AnimatedIn");
            //animator.SetBool("Timed", false);
        }
        if (timer >= 2 + delay)
        {
            animator.SetBool("Timed", false);
            Debug.Log("AnimatedOut");
            timer = 0;
            //animator.SetBool("Timed", false);
            this.GetComponent<ShooterEnemy>().enabled = true;
        }
    }

    public IEnumerator Burrow()
    {
        yield return new WaitForSeconds(delay);
        canEnter = false;
    }
}
