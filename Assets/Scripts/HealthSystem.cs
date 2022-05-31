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
    int dropFlag = 0;
    public int isPoisoned = 0;
    public float poisonStacks;
    public int poisonCD = 1;

    public int[] lootTable = { 80, 20 };
    public GameObject[] items;
    public Animator animator;

    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;

    private PauseMenu pauseMenu;

    int randomRoll;
    int total;
    int dropCounter = 0;

    private void Start()
    {
        health = maxHealth;
        poisonCD = 1;
        poisonStacks = 0;
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

        pauseMenu = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<PauseMenu>();
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
            health = health - poisonStacks;
            Invoke("Poisontick", 1);

            if (health < 0) health = 0;
            if (health == 0)
            {
                if (gameObject.tag == "Enemy" && dropFlag == 0)
                {
                    dropFlag = 1;
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
                        if (randomRoll <= dropChance)
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
                    if (GameObject.Find("BGM") != null)
                    {
                        GameObject.Find("BGM").SetActive(false);
                    }
                    else if (GameObject.Find("Boss_BGM") != null)
                    {
                        GameObject.Find("Boss_BGM").SetActive(false);
                    }
                    else if (GameObject.Find("FinalBoss_BGM") != null)
                    {
                        GameObject.Find("FinalBoss_BGM").SetActive(false);
                    }
                    GameObject.Find("Outro_GameOver").GetComponent<AudioLoop>().enabled = true;
                    pauseMenu.GameOver();
                }
            }

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
            if(gameObject.tag == "Enemy" && dropFlag == 0)
            {
                dropFlag = 1;
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
                if (GameObject.Find("BGM") != null)
                {
                    GameObject.Find("BGM").SetActive(false);
                }
                else if (GameObject.Find("Boss_BGM") != null)
                {
                    GameObject.Find("Boss_BGM").SetActive(false);
                }
                else if (GameObject.Find("FinalBoss_BGM") != null)
                {
                    GameObject.Find("FinalBoss_BGM").SetActive(false);
                }
                GameObject.Find("Outro_GameOver").GetComponent<AudioLoop>().enabled = true;
                pauseMenu.GameOver();
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
