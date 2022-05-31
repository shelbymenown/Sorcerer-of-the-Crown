using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dashIndicator : MonoBehaviour
{
    public Image dashTimer;
    private float dodgeCooldown = 3.0f;
    private float timeLeft;
    private bool onCooldown;

    void Start()
    {
        dashTimer.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (onCooldown)
        {
            timeLeft -= Time.deltaTime;
            dashTimer.fillAmount = (timeLeft / dodgeCooldown);
        }
    }

    public void startDodge()
    {
        dashTimer.fillAmount = 1.0f;
        onCooldown = true;
        timeLeft = dodgeCooldown;
        Debug.Log("dash cooldown started");
    }

    public void endDodge()
    {
        onCooldown = false;
        dashTimer.fillAmount = 0.0f;
        Debug.Log("dash cooldown is up");
        timeLeft = 0;
    }
}
