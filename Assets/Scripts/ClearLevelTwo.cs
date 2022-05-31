using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ClearLevelTwo : MonoBehaviour
{
    public AudioSource WinSfx;
    public GameObject boss;
    public GameObject toNextScene;
    public Tilemap wall;
    private bool hasBeenActive = false;

    private void Start()
    {
        WinSfx = GameObject.Find("WinSfx").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null && !hasBeenActive)
        {
            for (int i = 0; i < 3; i++)
            {
                wall.SetTile(new Vector3Int(-54 + i, 33, 0), null);
                wall.SetTile(new Vector3Int(-54 + i, 32, 0), null);
            }

            hasBeenActive = true;
            WinSfx.Play();
        }
    }
}
