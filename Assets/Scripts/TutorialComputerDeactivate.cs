using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialComputerDeactivate : MonoBehaviour
{
    public GameObject cover;
    public GameObject teleporter;
    public static bool pressed = false;
    //private bool alreadyDone = false;

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            //alreadyDone = true;
            cover.SetActive(false);
            teleporter.SetActive(true);
            pressed = false;
        }
    }

}
