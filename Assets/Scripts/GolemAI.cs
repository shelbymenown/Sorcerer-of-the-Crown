using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAI : MonoBehaviour
{
    private GameObject player;
    private ShooterEnemy shooter;
    private float range = 100f;
    private bool inTurretMode = false;

    public GameObject mechaGolem;
    public AudioSource bullet1Sfx;
    public AudioSource bullet2Sfx;
    public AudioSource chargeSfx;
    public GameObject bullet1Prefab;
    public GameObject bullet2Prefab;
    public Animator animator;
    public float meleeDistance = 2.5f;
    public float speedDistance = 4.5f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shooter = gameObject.GetComponent<ShooterEnemy>();
        bullet1Sfx = GameObject.Find("Bullet1Sfx").GetComponent<AudioSource>();
        bullet2Sfx = GameObject.Find("BlackholeSfx").GetComponent<AudioSource>();
        animator = mechaGolem.GetComponent<Animator>();
        shooter.bulletSfx = bullet1Sfx;
        
        Invoke("TurretMode", 10f);
    }

    // Update is called once per frame
    void Update()
    {
        range = (gameObject.transform.position - player.transform.position).magnitude;


        if (!inTurretMode)
        {
            if (range < meleeDistance)
            {
                Debug.LogError("In range");
                // Do melee attack animation
            }
            else if (range < speedDistance)
            {
                gameObject.GetComponent<MoveTowardsPlayer>().speed = 1;
            }
            else
            {
                gameObject.GetComponent<MoveTowardsPlayer>().speed = 2;
            }
        }

    }

    void TurretMode()
    {
        //Debug.LogError("Turret: " + (Time.deltaTime - timey));
        animator.SetBool("exitTurret", false);
        inTurretMode = true;
        chargeSfx.Play();
        gameObject.GetComponent<MoveTowardsPlayer>().speed = 0;
        shooter.enabled = false;
        shooter.bulletPrefab = bullet2Prefab;
        shooter.bulletSfx = bullet2Sfx;

        startTurretAnimation();

        Invoke("Fire", 1.8f);

        // --------------------------------
        // start laser attack animation here
        // --------------------------------

        Invoke("ResetGolem", 5);
    }

    void Fire()
    {
        shooter.enabled = true;
    }

    void ResetGolem()
    {
        shooter.bulletPrefab = bullet1Prefab;
        shooter.bulletSfx = bullet1Sfx;
        if (inTurretMode)
        {
            inTurretMode = false;
            animator.SetBool("exitTurret", true);
            Wait(0.13f);
            animator.SetInteger("AttackType", 0);
        }
        Invoke("TurretMode", Random.Range(5, 10));
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    void startTurretAnimation()
    {
        animator.SetInteger("AttackType", 3);
        Wait(0.13f);
    }
}