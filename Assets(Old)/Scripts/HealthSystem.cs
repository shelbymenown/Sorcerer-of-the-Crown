using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public HealthBar healthBar;
    public float maxHealth = 100;
    public static float healthLoad;
    public float health;
    int isPoisoned = 0;
    float poisonStacks = 0;
    int poisonCD = 1;

    public int[] lootTable = { 80, 20 };
    public GameObject[] items;
    public Animator animator;

    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;


    int randomRoll;
    int total;
    int dropCounter = 0;

    private void Start()
    {
        health = maxHealth;
        if (gameObject == GameObject.FindGameObjectWithTag("Player") && PlayerSaveManager.LoadState == true)
        {
            healthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<HealthBar>();
            health = healthLoad;
            healthBar.SetHealth(health);
        }
        else
        {
            healthBar.SetHealth(health);
        }

        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
    }

    void Update()
    {
        if(isPoisoned == 0)
        {
            poisonStacks = 0;
        }

        if (isPoisoned > 0 && poisonCD == 1)
        {
            poisonCD = 0;
            Damage(poisonStacks);
            Invoke("Poisontick", 1);

        }
    }

    public HealthSystem(float maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public float GetHealth()
    {
        return health;
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        if (health < 0) health = 0;
        //Debug.Log("Health: " +health);
        if(health == 0)
        {
            if(gameObject.tag == "Enemy")
            {
                total = 0;
                foreach (var item in lootTable)
                {
                    total += item;
                }

                randomRoll = Random.Range(0, total);
                //Debug.Log("Total: " + total);
                Debug.Log("Roll: " + randomRoll);

                foreach (var dropChance in lootTable)
                {
                    if(randomRoll <= dropChance)
                    {
                        GameObject Drop = Instantiate(items[dropCounter], gameObject.transform.position, gameObject.transform.rotation);
                        dropCounter = 0;
                        break;
                    }
                    else
                    {
                        dropCounter++;
                        randomRoll -= dropChance;
                    }
                }

                Destroy(gameObject);
            }
                
            if (gameObject.tag == "Player")
            {
                animator.SetBool("ranOutOfHealth", true);
            } 
        }
      
        if (gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            healthLoad = health;
            healthBar.SetHealth(health);
        }
        else
        {
            healthBar.SetHealth(health);
        }

        sr.material = matWhite;
        Invoke("ResetMaterial", .1f);
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth) health = maxHealth;

       
        if (gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            healthLoad = health;
            healthBar.SetHealth(health);
        }
        else
        {
            healthBar.SetHealth(health);
        }

    }

    void ResetMaterial()
    {
        Debug.Log(matDefault);
        sr.material = matDefault;
    }

    public void Poison(float poisonAmount)
    {
        poisonStacks = poisonStacks + poisonAmount;
        isPoisoned = isPoisoned + 1;
        Invoke("PoisonTimer", 5);

    }

    void PoisonTimer()
    {
        isPoisoned = isPoisoned - 1;
    }

    void Poisontick()
    {
        poisonCD = 1;
    }
}
