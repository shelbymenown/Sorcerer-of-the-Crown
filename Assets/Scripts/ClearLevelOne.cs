using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearLevelOne : MonoBehaviour
{
    public AudioSource WinSfx;
    public GameObject boss;
    public GameObject toNextScene;
    public GameObject[] grates;
    private bool hasBeenActive = false;

    // Start is called before the first frame update
    void Start()
    {
        WinSfx = GameObject.Find("WinSfx").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null && !hasBeenActive)
        {
            foreach (GameObject grate in grates)
            {
                //Destroy(grate);
                grate.SetActive(false);
            }

            hasBeenActive = true;
            toNextScene.SetActive(true);
            WinSfx.Play();
        }
    }
}
