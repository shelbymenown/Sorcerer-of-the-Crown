using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public HealthSystem playerHealth;
    public HealthSystem enemyHealth;
    private Text healthAmount;

    private void Start()
    {
        healthAmount = GetComponentInChildren<Text>();

        if (gameObject == GameObject.FindGameObjectWithTag("PlayerHealthBar"))
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

        healthAmount.text = healthBar.value.ToString() + " / " + healthBar.maxValue.ToString();
    }

    public void SetHealth(float hp)
    {
        healthBar.value = hp;
        if (healthBar != null && healthAmount != null)
            healthAmount.text = healthBar.value.ToString() + " / " + healthBar.maxValue.ToString();
    }
}