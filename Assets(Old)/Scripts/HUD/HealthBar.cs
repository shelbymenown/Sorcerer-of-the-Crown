using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public HealthSystem playerHealth;
    public HealthSystem enemyHealth;

    private void Start()
    {
        if (gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
            healthBar.maxValue = playerHealth.maxHealth;
            healthBar.value = playerHealth.health;
        }
        else if (gameObject.tag == "Enemy")
        {
            enemyHealth = gameObject.GetComponent<HealthSystem>();
            healthBar.maxValue = enemyHealth.maxHealth;
            healthBar.value = enemyHealth.maxHealth;
        }
    }

    public void SetHealth(float hp)
    {
        healthBar.value = hp;
    }
}