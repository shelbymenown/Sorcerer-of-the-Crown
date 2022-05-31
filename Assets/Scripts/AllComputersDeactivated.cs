using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AllComputersDeactivated : MonoBehaviour
{
    public GameObject[] grates;
    public GameObject teleporter;
    public static int count = 0;
    private bool alreadyDone = false;

    // Update is called once per frame
    void Update()
    {
        if (count >= grates.Length && !alreadyDone)
        {
            alreadyDone = true;
            removeGrates();
            teleporter.SetActive(true);
        }
    }

    void removeGrates()
    {
        foreach (GameObject grate in grates)
        {
            Debug.Log(grate);
            //Destroy(grate);
            grate.SetActive(false);
        }
        count = 0;
    }
}
