using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioSource bulletSfx;
    public AudioSource fireballSfx;
    public AudioSource blackholeSfx;
    public GameObject bulletPrefab;
    public GameObject fireballPrefab;
    public GameObject orbOfAnnihilationPrefab;
    public Transform spellPoint;
    public Camera cam;
    public SpriteRenderer orientation;
    public Animator animator;
    public Rigidbody2D rigidBody;

    public int spells = 1;
    public float speed = 8.0f;
    public float bulletSpeed = 30.0f;
    public float fireRate = .33f;
    public float spellCooldown = 1f;
    public int manaCost = 10;
    int canFire = 1;
    int canCast = 1;
    int empowered = 0;

    Vector2 mousePos;
    Vector2 fireDirection;
    Vector2 movement;
    //SpriteRenderer orientation = get;

    

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Example());
        cam = Camera.main;
        bulletSfx = GameObject.Find("BulletSfx").GetComponent<AudioSource>();
        fireballSfx = GameObject.Find("FireballSfx").GetComponent<AudioSource>();
        blackholeSfx = GameObject.Find("BlackholeSfx").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            fireDirection = mousePos - rigidBody.position;
            fireDirection = fireDirection.normalized;
            //float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg - 90f;

            /*
            if (fireDirection.x < 0)
            {
                orientation.flipX = true;
            }
            else
            {
                orientation.flipX = false;
            }
            */

            //rb.rotation = angle;

            Move();

            if (Input.GetButtonDown("Num1"))
            {
                spells = 1;
            }

            if (Input.GetButtonDown("Num2"))
            {
                spells = 2;
            }

            if (Input.GetButtonDown("Num3"))
            {
                spells = 3;
            }

            if (Input.GetButton("Fire1"))
            {
                if(canFire == 1)
                {
                    animator.SetTrigger("clickedLeftMouse");
                    Shoot();
                    canFire = 0;
                    Invoke("ShootReset", fireRate);
                }
            }
            if (Input.GetButtonDown("Fire2"))
            {
                animator.SetTrigger("clickedRightMouse");
                Spell();
            }
        }
    }

    // Default fixedUpdate is called 50 times per second
    void FixedUpdate()
    {
        // Movement
        rigidBody.MovePosition(rigidBody.position + movement * speed * Time.fixedDeltaTime);
    }

    void Move()
    {
        /*
        float horizontalValue = Input.GetAxis("Horizontal");
        float verticalValue = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(horizontalValue, verticalValue);

        rb.velocity = direction * speed;
        */

        // Get input direction
        movement.x = Input.GetAxisRaw("Horizontal"); //value between -1 and 1 (left -> -1, right -> 1, else 0)
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized; //This prevents diagonal movement from being faster

        // Sends information to player animator
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        animator.SetFloat("horizontalMousePos", mousepos.x);
        animator.SetFloat("verticalMousePos", mousepos.y);
        animator.SetFloat("horizontalMovementPos", movement.x);
        animator.SetFloat("verticalMovementPos", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void Shoot()
    {
        //spellPoint.rotation = angle.normalized;
        GameObject bullet = Instantiate(bulletPrefab,spellPoint.position, spellPoint.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        AudioSource pewPewSfx = bullet.GetComponent<AudioSource>();
        float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;

        rbBullet.rotation = angle;

        if(empowered == 1)
        {
            bullet.GetComponent<FriendlyProjectile>().damageAmount = bullet.GetComponent<FriendlyProjectile>().damageAmount * 1.4f;
        }

        //rbBullet.velocity = fireDirection * bulletSpeed;
        rbBullet.AddForce(fireDirection * bulletSpeed, ForceMode2D.Impulse);
        //AudioSource.PlayClipAtPoint(pewPewSfx.clip, spellPoint.position);
        bulletSfx.Play();
    }

    void ShootReset()
    {
        canFire = 1;
    }

    void Spell()
    {   
        if(canCast == 1)
        {
            
            


            canCast = 0;
            Invoke("SpellReset", spellCooldown);

            if (spells == 1 && gameObject.GetComponent<ManaSystem>().SpendMana(manaCost) == 1)
            {
                GameObject fireball = Instantiate(fireballPrefab, spellPoint.position, spellPoint.rotation);
                Rigidbody2D rbFireball = fireball.GetComponent<Rigidbody2D>();
                //AudioSource pewPewSfx = fireball.GetComponent<AudioSource>();
                float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;
                rbFireball.rotation = angle;

                rbFireball.AddForce(fireDirection * bulletSpeed, ForceMode2D.Impulse);
                //AudioSource.PlayClipAtPoint(pewPewSfx.clip, spellPoint.position);
                fireballSfx.Play();
            }
            else if (spells == 2 && gameObject.GetComponent<ManaSystem>().SpendMana(manaCost) == 1)
            {
                GameObject orb = Instantiate(orbOfAnnihilationPrefab, spellPoint.position, spellPoint.rotation);
                Rigidbody2D rbOrb = orb.GetComponent<Rigidbody2D>();


                float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;
                rbOrb.rotation = angle;

                rbOrb.AddForce(fireDirection * 10f, ForceMode2D.Impulse);
                blackholeSfx.Play();
            }
            else if (spells == 3 && empowered == 0)
            {
                if(gameObject.GetComponent<ManaSystem>().SpendMana(manaCost) == 1)
                {
                    empowered = 1;
                    speed = speed * 1.25f;
                    fireRate = fireRate / 1.25f;
                    Invoke("EmpowerDuration", 6);
                }

            }
            /*else if (spells == 2)
            {
                RaycastHit2D hit = Physics2D.Raycast(spellPoint.position, spellPoint.up);
                if (hit)
                {


                    if (hit.transform.CompareTag("Enemy"))
                    {
                          

                        hit.transform.GetComponent<HealthSystem>().Damage(20);
                        //ChainLightning(hit.transform.gameObject, 4);
                    }
                }


            }*/
            
        }
        
    }

    /*void ChainLightning(GameObject target, int bounces)
    {
        GameObject[] multipleEnemies;
        if (bounces == 0)
        {
            return;
        }
        multipleEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        StartCoroutine(timer());
        Collider2D tmp = Physics2D.OverlapCircle(target.transform.position, 8);
        multipleEnemies[0].GetComponent<HealthSystem>().Damage(20);
        ChainLightning(multipleEnemies[0], bounces);
        return;

        
    }
    
    /*void ChainLightning(Collider2D target, int bounces)
    {
        GameObject[] multipleEnemies
        //Collider2D[] tmp = Physics2D.OverlapCircleAll(target.transform.position, 20);
        if(bounces == 0)
        {
            return;
        }
        Collider2D tmp = Physics2D.OverlapCircle(target.transform.position, 20);
        tmp.transform.GetComponent<HealthSystem>().Damage(20);

        Invoke("ChainLightning"(tmp, bounces), .06f);
        
        //foreach (Collider2D collider in tmp)
    }*/



    void SpellReset()
    {
        canCast = 1;
    }

    void EmpowerDuration()
    {
        empowered = 0;
        speed = speed / 1.25f;
        fireRate = fireRate * 1.25f;
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(.18f);
    }

}
