using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FierceWizardShooter : MonoBehaviour
{
    public AudioSource bulletSfx;
    private GameObject player;
    public Animator animator;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 10f;
    private bool canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        bulletSfx = GameObject.Find("Bullet1Sfx").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (canFire)
        {
            Shoot();
            animator.SetTrigger("basicAttack");
            canFire = false;
            Invoke("ShootReset", fireRate);
        }
    }

    void Shoot()
    {
        Vector2 playerAngle = player.transform.position - gameObject.transform.position;
        //spellPoint.rotation = angle;
        bulletSfx.Play();
        GameObject enemyBullet = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rbBullet = enemyBullet.GetComponent<Rigidbody2D>();
        //AudioSource pewPewSfx = enemyBullet.GetComponent<AudioSource>();

        rbBullet.rotation += 90;

        //rbBullet.velocity = fireDirection * bulletSpeed;
        playerAngle = playerAngle.normalized;

        rbBullet.AddForce(playerAngle * bulletSpeed, ForceMode2D.Impulse);
        //AudioSource.PlayClipAtPoint(pewPewSfx.clip, gameObject.transform.position);
    }

    void ShootReset()
    {
        canFire = true;
    }


}
