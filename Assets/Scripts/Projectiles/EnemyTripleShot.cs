using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTripleShot : MonoBehaviour
{
    public AudioSource bulletSfx;
    public GameObject player;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 1.5f;
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
        Vector2 playerAngle1 = player.transform.position - gameObject.transform.position;
        Vector2 playerAngle2 = playerAngle1;
        Vector2 playerAngle3 = playerAngle1;

        float angle = Mathf.Atan2(playerAngle1.y, playerAngle1.x) * Mathf.Rad2Deg;

        //playerAngle1.x = Mathf.Cos((Mathf.Acos(playerAngle1.x) * Mathf.Rad2Deg) + 30);
        //playerAngle1.y = Mathf.Sin((Mathf.Asin(playerAngle1.y) * Mathf.Rad2Deg) + 30);

        //playerAngle3.x = Mathf.Cos((Mathf.Acos(playerAngle3.x) * Mathf.Rad2Deg) + 30);
        //playerAngle3.y = Mathf.Sin((Mathf.Asin(playerAngle3.y) * Mathf.Rad2Deg) + 30);


        playerAngle1.x = playerAngle1.x * Mathf.Cos(-30 * Mathf.Deg2Rad) - playerAngle1.y * Mathf.Sin(-30 * Mathf.Deg2Rad);
        playerAngle1.y = playerAngle1.x * Mathf.Sin(-30 * Mathf.Deg2Rad) + playerAngle1.y * Mathf.Cos(-30 * Mathf.Deg2Rad);

        playerAngle3.x = playerAngle3.x * Mathf.Cos(30 * Mathf.Deg2Rad) - playerAngle3.y * Mathf.Sin(30 * Mathf.Deg2Rad);
        playerAngle3.y = playerAngle3.x * Mathf.Sin(30 * Mathf.Deg2Rad) + playerAngle3.y * Mathf.Cos(30 * Mathf.Deg2Rad);

        //spellPoint.rotation = angle;
        bulletSfx.Play();
        GameObject enemyBullet1 = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rbBullet1 = enemyBullet1.GetComponent<Rigidbody2D>();
        GameObject enemyBullet2 = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rbBullet2 = enemyBullet2.GetComponent<Rigidbody2D>();
        GameObject enemyBullet3 = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rbBullet3 = enemyBullet3.GetComponent<Rigidbody2D>();
        //AudioSource pewPewSfx = enemyBullet.GetComponent<AudioSource>();


        //rbBullet.velocity = fireDirection * bulletSpeed;


        rbBullet1.rotation = angle - 30;
        rbBullet2.rotation = angle;
        rbBullet3.rotation = angle + 30;

        playerAngle1 = playerAngle1.normalized;
        playerAngle2 = playerAngle2.normalized;
        playerAngle3 = playerAngle3.normalized;


        rbBullet1.AddForce(playerAngle1 * bulletSpeed, ForceMode2D.Impulse);
        rbBullet2.AddForce(playerAngle2 * bulletSpeed, ForceMode2D.Impulse);
        rbBullet3.AddForce(playerAngle3 * bulletSpeed, ForceMode2D.Impulse);
        
        //AudioSource.PlayClipAtPoint(pewPewSfx.clip, gameObject.transform.position);
        
    }

    void ShootReset()
    {
        canFire = 1;
    }
}
