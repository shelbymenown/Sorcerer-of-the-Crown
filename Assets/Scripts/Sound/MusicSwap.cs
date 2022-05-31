using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwap : MonoBehaviour
{
    public GameObject SetUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.Find("BGM").SetActive(false);
            GameObject.Find("Boss_BGM").GetComponent<AudioLoop>().enabled = true;

            if (SetUp != null)
            {
                SetUp.SetActive(true);
            }
        
            Destroy(GameObject.Find("Music_Trigger"));
        }
    }
}
