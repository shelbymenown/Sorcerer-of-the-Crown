using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioSource bulletSfx;
    public AudioSource fireballSfx;
    public AudioSource blackholeSfx;
    public AudioSource IceSfx;
    public AudioSource EmpowerStartSfx;
    public AudioSource EmpowerEndSfx;
    public AudioSource ConeOfColdSfx;
    public AudioSource ImperviousStartSfx;
    public AudioSource ImperviousEndSfx;
    public AudioSource DodgeSfx;
    public GameObject bulletPrefab;
    public GameObject fireballPrefab;
    public GameObject orbOfAnnihilationPrefab;
    public GameObject coneOfColdPrefab;
    public GameObject crystalShardPrefab;
    public Transform spellPoint;
    public Camera cam;
    public SpriteRenderer orientation;
    public Animator animator;
    public Rigidbody2D rigidBody;
    private GameObject _player;
    private Inventory _inventory;
    private Material matRedTint;
    private Material matDefault;
    SpriteRenderer sr;
    GameObject hotBar7;
    GameObject hotBar8;
    UIItem hotBar7Item;
    UIItem hotBar8Item;
    Transform temp;
    Vector3 tempVector;
    string currentScene;

    public int spells = 1;
    public float speed = 8.0f;
    public float bulletSpeed = 30.0f;
    public float crystalShardSpeed = 40.0f;
    public int numShards = 12;
    public int shardSpread = 30;
    public float fireRate = .33f;
    public float spellCooldown = 1f;
    public float dodgeSpeed = 25.0f;
    public float dodgeCooldown = 3.0f;
    public float dodgeTime = .3f;
    public int manaCost = 10;


    int canFire = 1;
    int canCast = 1;
    int canDodge = 1;
    public int dodging = 0;
    int empowered = 0;
    int impervious = 1;
    private float alphaLevel = 0.75f;
    private bool isActivated = false;
    float R = 1;
    float G = 0;
    float B = 1;
    float colorRate = 0.08f;

    int i = 0;

    Vector2 mousePos;
    Vector2 fireDirection;
    public Vector2 movement;
    //SpriteRenderer orientation = get;
    public GameObject dashIndicator;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _inventory = _player.GetComponent<Inventory>();
        hotBar7 = GameObject.FindGameObjectWithTag("HotBar7");
        hotBar8 = GameObject.FindGameObjectWithTag("HotBar8");
        hotBar7Item = hotBar7.GetComponentInChildren<UIItem>();
        hotBar8Item = hotBar8.GetComponentInChildren<UIItem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Example());
        cam = Camera.main;
        bulletSfx = GameObject.Find("BulletSfx").GetComponent<AudioSource>();
        fireballSfx = GameObject.Find("FireballSfx").GetComponent<AudioSource>();
        blackholeSfx = GameObject.Find("BlackholeSfx").GetComponent<AudioSource>();
        IceSfx = GameObject.Find("IceSfx").GetComponent<AudioSource>();
        EmpowerStartSfx = GameObject.Find("EmpowerStartSfx").GetComponent<AudioSource>();
        EmpowerEndSfx = GameObject.Find("EmpowerEndSfx").GetComponent<AudioSource>();
        ImperviousStartSfx = GameObject.Find("ImperviousStartSfx").GetComponent<AudioSource>();
        ImperviousEndSfx = GameObject.Find("ImperviousEndSfx").GetComponent<AudioSource>();
        ConeOfColdSfx = GameObject.Find("ConeOfColdSfx").GetComponent<AudioSource>();
        DodgeSfx = GameObject.Find("DodgeSfx").GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;
        matRedTint = Resources.Load("RedShade", typeof(Material)) as Material;
        currentScene = SceneManager.GetActiveScene().name;
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

            if (dodging == 0)
            {
                Move();
            }

            if(Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                spells++;
                if (currentScene == "Mechanical Dungeon")
                {
                    if (spells > 2)
                    {
                        spells = 1;
                    }    
                }
                else if (currentScene == "Sand Temple")
                {
                    if (spells > 4)
                    {
                        spells = 1;
                    }
                }
                else
                {
                    if (spells > 6)
                    {
                        spells = 1;
                    }
                }
                    
                       

                toolbarSelectionIndicator.nextPosition = spells - 1;
            }
            else if(Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                spells--;
               
                if (spells < 1)
                {
                    if (currentScene == "Mechanical Dungeon")
                        spells = 2;
                    else if (currentScene == "Sand Temple")
                        spells = 4;
                    else
                        spells = 6;
                }

                toolbarSelectionIndicator.nextPosition = spells - 1;
            }


            if (Input.GetButtonDown("Num1"))
            {
                spells = 1;
            }

            if (Input.GetButtonDown("Num2"))
            {
                spells = 2;
            }
            if (currentScene == "Sand Temple" || currentScene == "Catacombs")
            {
                if (Input.GetButtonDown("Num3"))
                {
                    spells = 3;
                }

                if (Input.GetButtonDown("Num4"))
                {
                    spells = 4;
                }
            }
            if (currentScene == "Catacombs")
            {
                if (Input.GetButtonDown("Num5"))
                {
                    spells = 5;
                }

                if (Input.GetButtonDown("Num6"))
                {
                    spells = 6;
                }
            }

            // 7 is pressed
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (hotBar7Item != null)
                    if (hotBar7Item.item != null)
                        _inventory.RemoveItem(hotBar7Item.item.id);
            }
            // 8 is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hotBar8Item != null)
                    if (hotBar8Item.item != null)
                        _inventory.RemoveItem(hotBar8Item.item.id);
            }

            if (Input.GetButton("Fire1"))
            {
                if (canFire == 1)
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
            
            if (Input.GetButton("Dodge"))
            {
                if (canDodge == 1)
                {
                    dashIndicator.GetComponent<dashIndicator>().startDodge();
                    dodging = 1;

                    gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                    DodgeSfx.Play();
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
                    Dodge();
                    canDodge = 0;
                    Invoke("EndDodge", dodgeTime);
                    Invoke("DodgeReset", dodgeCooldown);
                }
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

                sr.color = new Color(R, G, B, alphaLevel);
            }
        }
    }

    // Default fixedUpdate is called 50 times per second
    void FixedUpdate()
    {
        // Movement
        if (dodging == 0)
        {
            rigidBody.MovePosition(rigidBody.position + movement * speed * Time.fixedDeltaTime);
        }
        else
        {
           
                rigidBody.MovePosition(rigidBody.position + movement * dodgeSpeed * Time.fixedDeltaTime);
            
        }

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

    void Dodge()
    {

        movement.x = Input.GetAxisRaw("Horizontal"); //value between -1 and 1 (left -> -1, right -> 1, else 0)
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized; //This prevents diagonal movement from being faster



    }

    void Shoot()
    {
        //spellPoint.rotation = angle.normalized;
        GameObject bullet = Instantiate(bulletPrefab, spellPoint.position, spellPoint.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        AudioSource pewPewSfx = bullet.GetComponent<AudioSource>();
        float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;

        rbBullet.rotation = angle;

        if (empowered == 1)
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

    void DodgeReset()
    {
        canDodge = 1;
        dashIndicator.GetComponent<dashIndicator>().endDodge();
    }

    
    void EndDodge()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        dodging = 0;
    }

    void Spell()
    {
        if (canCast == 1)
        {
            //Debug.LogError("imperviousHitOff = " + ContactDamage.imperviousHitOff);



            canCast = 0;
            Invoke("SpellReset", spellCooldown);

            if (spells == 1 && gameObject.GetComponent<ManaSystem>().SpendMana(25) == 1)
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
            else if (spells == 2 && gameObject.GetComponent<ManaSystem>().SpendMana(30) == 1)
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
                if (gameObject.GetComponent<ManaSystem>().SpendMana(30) == 1)
                {
                    EmpowerStartSfx.Play();
                    // sr.material = matRedTint;
                    sr.color = new Color(1.0f, 0.0f, 0.0f);
                    Invoke("ResetMaterial", .1f);
                    empowered = 1;
                    speed = speed * 1.25f;
                    fireRate = fireRate / 1.25f;
                    Invoke("EmpowerDuration", 6);
                }

            }
            else if (spells == 4 && gameObject.GetComponent<ManaSystem>().SpendMana(25) == 1)
            {
                //tempVector.x = 0f;
                //tempVector.y = 0f;
                //tempVector.z = 0f;
                tempVector = Vector3.zero;

                float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;

                ConeOfColdSfx.Play();
                
                //temp.rotation.Set(50,50,50);
                tempVector.x = fireDirection.x * 5;
                tempVector.y = fireDirection.y * 5;
                tempVector.z = 0f;
                //temp.position = tempVector;

                GameObject cone = Instantiate(coneOfColdPrefab, spellPoint.position + tempVector, spellPoint.rotation);

                Rigidbody2D rbCone = cone.GetComponent<Rigidbody2D>();               
                rbCone.rotation = angle - 90;
                
                //rbOrb.AddForce(fireDirection * 10f, ForceMode2D.Impulse);

            }
            else if (spells == 5 && gameObject.GetComponent<ManaSystem>().SpendMana(20) == 1)
            {


                float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;


                IceSfx.Play();
                while(i < numShards)
                {
                    int randomNumber;
                    randomNumber = Random.Range(-shardSpread, shardSpread);
                    Vector2 ShardDirection = fireDirection;

                    ShardDirection.x = ShardDirection.x * Mathf.Cos(randomNumber * Mathf.Deg2Rad) - ShardDirection.y * Mathf.Sin(randomNumber * Mathf.Deg2Rad);
                    ShardDirection.y = ShardDirection.x * Mathf.Sin(randomNumber * Mathf.Deg2Rad) + ShardDirection.y * Mathf.Cos(randomNumber * Mathf.Deg2Rad);

                    GameObject shard = Instantiate(crystalShardPrefab, spellPoint.position, spellPoint.rotation);
                    Rigidbody2D rbShard = shard.GetComponent<Rigidbody2D>();
                    rbShard.rotation = angle + randomNumber;

                    rbShard.AddForce(ShardDirection * (crystalShardSpeed - 2 * i), ForceMode2D.Impulse);
                    i++;
                }
                i = 0;
            }
            else if (spells == 6 && impervious == 1 && gameObject.GetComponent<ManaSystem>().SpendMana(50) == 1)
            {
                //gameObject.GetComponent<Knockback>().enabled = false;
                Debug.LogError("Cheeky");
                isActivated = true;
                Knockback.isDisabled = true;
                ContactDamage.imperviousHitOff = 0;
                EnemyProjectile.playerImpervious = true;
                ImperviousStartSfx.Play();
                impervious = 0;
                Invoke("EndImperviousness", 5);
                Invoke("ImperviousReset", 5);
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

    void EndImperviousness()
    {
        //gameObject.GetComponent<Knockback>().enabled = true;
        isActivated = false;
        sr.color = new Color(1, 1, 1, 1);
        ImperviousEndSfx.Play();
        Knockback.isDisabled = false;
        ContactDamage.imperviousHitOff = 1;
        EnemyProjectile.playerImpervious = false;
    }

    void ImperviousReset()
    {
        impervious = 1;
    }

    void SpellReset()
    {
        canCast = 1;
    }

    void EmpowerDuration()
    {
        EmpowerEndSfx.Play();
        sr.color = new Color(1.0f, 1.0f, 1.0f);
        empowered = 0;
        speed = speed / 1.25f;
        fireRate = fireRate * 1.25f;
    }

    
    IEnumerator timer()
    {
        yield return new WaitForSeconds(.18f);
    }

    /*
    void RainbowVFX()
    {

    }
    */
}
