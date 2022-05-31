using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossTurbo : MonoBehaviour
{
    private GameObject player;
    private ShooterEnemyTurbo shooter;
    private MoveTowardsPlayer move;

    private AudioSource bulletSfx;
    private AudioSource fireballSfx;
    private AudioSource blackholeSfx;
    private AudioSource empowerStartSfx;
    private AudioSource coneOfColdSfx;
    private AudioSource crystalBarrageSfx;
    private AudioSource imperviousStartSfx;
    private AudioSource enemySpawnSfx;
    private Color originalColor;
    private bool isActivated = false;
    float R = 1;
    float G = 0;
    float B = 1;
    float colorRate = 0.08f;

    public GameObject bulletPrefab;
    public GameObject fireballPrefab;
    public GameObject blackholePrefab;
    //public GameObject empowerPrefab;
    public GameObject coneOfColdPrefab;
    public GameObject crystalBarragePrefab;
    //public GameObject ImperviousStartPrefab;
    public GameObject[] enemiesPrefabs;

    SpriteRenderer sr;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        move = gameObject.GetComponent<MoveTowardsPlayer>();
        shooter = gameObject.GetComponent<ShooterEnemyTurbo>();
        bulletSfx = GameObject.Find("Bullet1Sfx").GetComponent<AudioSource>();
        fireballSfx = GameObject.Find("FireballSfx").GetComponent<AudioSource>();
        blackholeSfx = GameObject.Find("BlackholeSfx").GetComponent<AudioSource>();
        empowerStartSfx = GameObject.Find("EmpowerStartSfx").GetComponent<AudioSource>();
        coneOfColdSfx = GameObject.Find("ConeOfColdSfx").GetComponent<AudioSource>();
        crystalBarrageSfx = GameObject.Find("IceSfx").GetComponent<AudioSource>();
        imperviousStartSfx = GameObject.Find("ImperviousStartSfx").GetComponent<AudioSource>();
        enemySpawnSfx = GameObject.Find("EnemySpawnSfx").GetComponent<AudioSource>();

        shooter.bulletSfx = bulletSfx;

        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        Bullet(); // Boss will always start with bullet

        InvokeRepeating("WeaponSelector", 10f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (move.enabled == false)
        {
            animator.SetFloat("Speed", 0);
        }
        else
        {
            animator.SetFloat("Speed", move.speed);
        }


        if (isActivated)
        {
            if (R >= 0 && G <= 0 && B >= 1)
            {
                R -= colorRate;
                //Debug.LogError("Dec R:" + R);
            }
            else if (R <= 0 && G <= 1 && B >= 1)
            {
                G += colorRate;
                //Debug.LogError("Inc G");
            }
            else if (R <= 0 && G >= 1 && B >= 0)
            {
                B -= colorRate;
                //Debug.LogError("Dec B");
            }
            else if (R <= 1 && G >= 1 && B <= 0)
            {
                R += colorRate;
                //Debug.LogError("Inc R");
            }
            else if (R >= 1 && G >= 0 && B <= 0)
            {
                G -= colorRate;
                //Debug.LogError("Dec G");
            }
            else if (R >= 1 && G <= 0 && B <= 1)
            {
                B += colorRate;
                //Debug.LogError("Inc B");
            }

            sr.color = new Color(R, G, B, 1);
        }
    }

    void WeaponSelector()
    {
        int weapon = Random.Range(0, 8);
        shooter.spawnEnemy = false;
        shooter.forCrystal = false;
        isActivated = false;
        gameObject.tag = "Enemy";
        sr.color = originalColor;

        switch (weapon)
        {
            case 0: Bullet(); break;
            case 1: Fireball(); break;
            case 2: Blackhole(); break;
            case 3: Empower(); break;
            case 4: ConeOfCold(); break;
            case 5: CrystalBarrage(); break;
            case 6: Impervious(); break;
            case 7: SpawnEnemies(); break;
            default: break;
        }
    }

    void Bullet()
    {
        shooter.multishot = 1;
        shooter.bulletSfx = bulletSfx;
        shooter.bulletPrefab = bulletPrefab;
        shooter.fireRate = 0.33f;
        move.speed = 3.0f;
        Debug.LogError("Bullet");
    }

    void Fireball()
    {
     
        shooter.multishot = 1;
        shooter.bulletSfx = fireballSfx;
        shooter.bulletPrefab = fireballPrefab;
        shooter.fireRate = 0.66f;
        move.speed = 2.5f;
        Debug.LogError("Fireball");
    }

    void Blackhole()
    {
        shooter.multishot = 1;
        shooter.bulletSfx = blackholeSfx;
        shooter.bulletPrefab = blackholePrefab;
        shooter.fireRate = 0.7f;
        move.speed = 1.0f;
        Debug.LogError("Blackhole");
    }

    void Empower()
    {
        shooter.multishot = 0;
        empowerStartSfx.Play();
        shooter.bulletSfx = bulletSfx;
        shooter.bulletPrefab = bulletPrefab;
        shooter.fireRate = 0.33f / 1.25f;
        move.speed = 3.5f;
        Debug.LogError("Empower");
    }

    void ConeOfCold()
    {
        shooter.multishot = 0;
        shooter.spawnEnemy = true;
        shooter.bulletSfx = coneOfColdSfx;
        shooter.bulletPrefab = coneOfColdPrefab;
        shooter.fireRate = 1.5f;
        move.speed = 2.0f;
        Debug.LogError("ConeOfCold");
    }

    void CrystalBarrage()
    {
        shooter.multishot = 0;
        shooter.bulletSfx = crystalBarrageSfx;
        shooter.bulletPrefab = crystalBarragePrefab;
        shooter.fireRate = 1.0f;
        shooter.forCrystal = true;
        move.speed = 0.0f;
        Debug.LogError("CrystalBarrage");
    }

    void Impervious()
    {
        shooter.multishot = 0;
        imperviousStartSfx.Play();
        shooter.bulletSfx = bulletSfx;
        shooter.bulletPrefab = bulletPrefab;
        shooter.fireRate = 0.33f;
        move.speed = 3.0f;
        Debug.LogError("Impervious");
        // make invincible;
        isActivated = true;
        gameObject.tag = "Untagged";
    }

    void SpawnEnemies()
    {
        shooter.multishot = 0;
        int index = Random.Range(0, enemiesPrefabs.Length);
        shooter.spawnEnemy = true;
        shooter.bulletSfx = enemySpawnSfx;
        shooter.bulletPrefab = enemiesPrefabs[index];
        shooter.fireRate = 2f;
        move.speed = 0.0f;
        Debug.LogError("SpawnEnemies");
    }
}
