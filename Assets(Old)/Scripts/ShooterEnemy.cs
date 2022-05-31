using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    public AudioSource bulletSfx;
    public GameObject player;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = .75f;
    int canFire = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        bulletSfx = GameObject.Find("BulletSfx").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (canFire == 1)
        {
            Shoot();
            canFire = 0;
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
        canFire = 1;
    }
}
