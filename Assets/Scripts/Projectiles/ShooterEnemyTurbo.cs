using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyTurbo : MonoBehaviour
{
    public AudioSource bulletSfx;
    public GameObject player;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = .75f;
    public bool spawnEnemy = false;
    public bool forCrystal = false;
    public int canFire = 1;
    public int numShards = 12;
    public int shardSpread = 30;
    public int multishot = 0;
    public float tempFirerate = 0f;
    public Animator animator;

    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        bulletSfx = GameObject.Find("Bullet1Sfx").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (canFire == 1)
        {
            animator.SetTrigger("clickedLeftMouse");
            Shoot();
            canFire = 0;
            Invoke("ShootReset", tempFirerate);
        }
    }

    void Shoot()
    {
        tempFirerate = fireRate;
        Vector2 playerAngle = player.transform.position - gameObject.transform.position;
        playerAngle = playerAngle.normalized;

        //spellPoint.rotation = angle;
        float angle = Mathf.Atan2(playerAngle.y, playerAngle.x) * Mathf.Rad2Deg;
        bulletSfx.Play();

        if (forCrystal)
        {
            ForCrystalBarrage(playerAngle, angle);
        }
        else if (multishot == 1)
        {
            int randomNum;
            randomNum = Random.Range(0, 3);

            if (randomNum == 0 || i > 0)
            {
                if (i == 0)
                {
                    i = 3;
                }

                tempFirerate = fireRate;
                GameObject enemyBullet = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
                Rigidbody2D rbBullet = enemyBullet.GetComponent<Rigidbody2D>();
                //AudioSource pewPewSfx = enemyBullet.GetComponent<AudioSource>();

                rbBullet.rotation = angle;

                if (spawnEnemy)
                    rbBullet.rotation = 0;

                //rbBullet.velocity = fireDirection * bulletSpeed;

                rbBullet.AddForce(playerAngle * bulletSpeed, ForceMode2D.Impulse);
                //AudioSource.PlayClipAtPoint(pewPewSfx.clip, gameObject.transform.position);
                i--;
            }
            else if (randomNum == 1)
            {

                tripleshot();
            }
            else
            {
                quadshot();
            }



        }
        else
        {
            tempFirerate = fireRate;

            GameObject enemyBullet = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D rbBullet = enemyBullet.GetComponent<Rigidbody2D>();
            //AudioSource pewPewSfx = enemyBullet.GetComponent<AudioSource>();

            rbBullet.rotation = angle;

            if (spawnEnemy)
                rbBullet.rotation = 0;

            //rbBullet.velocity = fireDirection * bulletSpeed;

            rbBullet.AddForce(playerAngle * bulletSpeed, ForceMode2D.Impulse);
            //AudioSource.PlayClipAtPoint(pewPewSfx.clip, gameObject.transform.position);
        }

    }
    public void ShootReset()
    {
        canFire = 1;
    }

    void ForCrystalBarrage(Vector2 playerAngle, float angle)
    {
        
        for (int i = 0; i < numShards; i++)
        {
            int randomNumber;
            randomNumber = Random.Range(-shardSpread, shardSpread);
            Vector2 ShardDirection = playerAngle;

            ShardDirection.x = ShardDirection.x * Mathf.Cos(randomNumber * Mathf.Deg2Rad) - ShardDirection.y * Mathf.Sin(randomNumber * Mathf.Deg2Rad);
            ShardDirection.y = ShardDirection.x * Mathf.Sin(randomNumber * Mathf.Deg2Rad) + ShardDirection.y * Mathf.Cos(randomNumber * Mathf.Deg2Rad);

            if (ShardDirection.x == 0.0f)
            {
                ShardDirection.x = 0.01f;
            }
            if (ShardDirection.y == 0.0f)
            {
                ShardDirection.y = 0.01f;
            }

            GameObject shard = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D rbShard = shard.GetComponent<Rigidbody2D>();
            rbShard.rotation = angle + randomNumber;

            //Debug.LogError("Force: " + ShardDirection * (bulletSpeed - Mathf.Exp(1) * i) + "\nShardDirection: " + ShardDirection);
            rbShard.AddForce(ShardDirection * (bulletSpeed - 1.666f * i - .833f), ForceMode2D.Impulse);
        }
    }

    void tripleshot()
    {
        tempFirerate = fireRate * 3;

        Vector2 playerAngle1 = player.transform.position - gameObject.transform.position;
        Vector2 playerAngle2 = playerAngle1;
        Vector2 playerAngle3 = playerAngle1;

        float angle = Mathf.Atan2(playerAngle1.y, playerAngle1.x) * Mathf.Rad2Deg;

        playerAngle1.x = playerAngle1.x * Mathf.Cos(-30 * Mathf.Deg2Rad) - playerAngle1.y * Mathf.Sin(-30 * Mathf.Deg2Rad);
        playerAngle1.y = playerAngle1.x * Mathf.Sin(-30 * Mathf.Deg2Rad) + playerAngle1.y * Mathf.Cos(-30 * Mathf.Deg2Rad);

        playerAngle3.x = playerAngle3.x * Mathf.Cos(30 * Mathf.Deg2Rad) - playerAngle3.y * Mathf.Sin(30 * Mathf.Deg2Rad);
        playerAngle3.y = playerAngle3.x * Mathf.Sin(30 * Mathf.Deg2Rad) + playerAngle3.y * Mathf.Cos(30 * Mathf.Deg2Rad);


        bulletSfx.Play();
        GameObject enemyBullet1 = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rbBullet1 = enemyBullet1.GetComponent<Rigidbody2D>();
        GameObject enemyBullet2 = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rbBullet2 = enemyBullet2.GetComponent<Rigidbody2D>();
        GameObject enemyBullet3 = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rbBullet3 = enemyBullet3.GetComponent<Rigidbody2D>();

        rbBullet1.rotation = angle - 30;
        rbBullet2.rotation = angle;
        rbBullet3.rotation = angle + 30;

        playerAngle1 = playerAngle1.normalized;
        playerAngle2 = playerAngle2.normalized;
        playerAngle3 = playerAngle3.normalized;

        rbBullet1.AddForce(playerAngle1 * bulletSpeed / 2, ForceMode2D.Impulse);
        rbBullet2.AddForce(playerAngle2 * bulletSpeed / 2, ForceMode2D.Impulse);
        rbBullet3.AddForce(playerAngle3 * bulletSpeed / 2, ForceMode2D.Impulse);
    }

    void quadshot()
    {
        tempFirerate = fireRate * 3;
        Vector2 playerAngle1 = player.transform.position - gameObject.transform.position;
        Vector2 playerAngle2 = playerAngle1;
        Vector2 playerAngle3 = playerAngle1;
        Vector2 playerAngle4 = playerAngle1;

        float angle = Mathf.Atan2(playerAngle1.y, playerAngle1.x) * Mathf.Rad2Deg;

        playerAngle1.x = playerAngle1.x * Mathf.Cos(-45 * Mathf.Deg2Rad) - playerAngle1.y * Mathf.Sin(-45 * Mathf.Deg2Rad);
        playerAngle1.y = playerAngle1.x * Mathf.Sin(-45 * Mathf.Deg2Rad) + playerAngle1.y * Mathf.Cos(-45 * Mathf.Deg2Rad);

        playerAngle2.x = playerAngle2.x * Mathf.Cos(-15 * Mathf.Deg2Rad) - playerAngle2.y * Mathf.Sin(-15 * Mathf.Deg2Rad);
        playerAngle2.y = playerAngle2.x * Mathf.Sin(-15 * Mathf.Deg2Rad) + playerAngle2.y * Mathf.Cos(-15 * Mathf.Deg2Rad);

        playerAngle3.x = playerAngle3.x * Mathf.Cos(15 * Mathf.Deg2Rad) - playerAngle3.y * Mathf.Sin(15 * Mathf.Deg2Rad);
        playerAngle3.y = playerAngle3.x * Mathf.Sin(15 * Mathf.Deg2Rad) + playerAngle3.y * Mathf.Cos(15 * Mathf.Deg2Rad);

        playerAngle4.x = playerAngle4.x * Mathf.Cos(45 * Mathf.Deg2Rad) - playerAngle4.y * Mathf.Sin(45 * Mathf.Deg2Rad);
        playerAngle4.y = playerAngle4.x * Mathf.Sin(45 * Mathf.Deg2Rad) + playerAngle4.y * Mathf.Cos(45 * Mathf.Deg2Rad);


        bulletSfx.Play();
        GameObject enemyBullet1 = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rbBullet1 = enemyBullet1.GetComponent<Rigidbody2D>();
        GameObject enemyBullet2 = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rbBullet2 = enemyBullet2.GetComponent<Rigidbody2D>();
        GameObject enemyBullet3 = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rbBullet3 = enemyBullet3.GetComponent<Rigidbody2D>();
        GameObject enemyBullet4 = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rbBullet4 = enemyBullet4.GetComponent<Rigidbody2D>();

        rbBullet1.rotation = angle - 45;
        rbBullet2.rotation = angle - 15;
        rbBullet3.rotation = angle + 15;
        rbBullet4.rotation = angle + 45;

        playerAngle1 = playerAngle1.normalized;
        playerAngle2 = playerAngle2.normalized;
        playerAngle3 = playerAngle3.normalized;
        playerAngle4 = playerAngle4.normalized;

        rbBullet1.AddForce(playerAngle1 * bulletSpeed / 2, ForceMode2D.Impulse);
        rbBullet2.AddForce(playerAngle2 * bulletSpeed / 2, ForceMode2D.Impulse);
        rbBullet3.AddForce(playerAngle3 * bulletSpeed / 2, ForceMode2D.Impulse);
        rbBullet4.AddForce(playerAngle4 * bulletSpeed / 2, ForceMode2D.Impulse);
    }
}
