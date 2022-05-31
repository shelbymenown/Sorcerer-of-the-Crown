using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOff : MonoBehaviour
{
    public AudioSource OffSfx;
    public SpriteRenderer screen;
    public bool dontCount = false;
    private float alphaLevel = 0.0f;
    private bool isActivated = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isActivated)
        {
            /*
            for (float i = 0.0f; i < 1; i += 0.1f)
            {
                time = i;
                Invoke("BootUp", i);
            }
            */
            screen.color = new Color(1, 1, 1, alphaLevel);
            isActivated = true;
            OffSfx.Play();

            if (!dontCount)
                AllComputersDeactivated.count++;
            TutorialComputerDeactivate.pressed = true;
        }
    }

    private void Update()
    {
        if (isActivated)
        {
            alphaLevel += 0.01f;
            screen.color = new Color(1, 1, 1, alphaLevel);
        }
    }
}
