using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearThirdLevel : MonoBehaviour
{
    public AudioSource VictorySfx;
    public GameObject boss;
    public GameObject toNextScene;
    private bool hasBeenActive = false;

    // Start is called before the first frame update
    void Start()
    {
        VictorySfx = GameObject.Find("VictorySfx").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null && !hasBeenActive)
        {
            hasBeenActive = true;
            VictorySfx.Play();
            Invoke("WinGame", 5f);
        }
    }

    void WinGame()
    {
        toNextScene.SetActive(true);
    }
}
