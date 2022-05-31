using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Unselected hotbar borders have a color hex value of RBG: 63, 60, 60
 */

public class toolbarSelectionIndicator : MonoBehaviour
{
    public static int currentPosition = 0; // this value will be 1 less than the displayed number on the hotbar
    public static int nextPosition;
    private GameObject[] hotbar = new GameObject[8];
    private string currentScene;

    public Color unselectedColor;
    public Color selectedColor;

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = 0;
        currentScene = SceneManager.GetActiveScene().name;

        // Save hotbar positions into array
        hotbar[0] = GameObject.Find("HotBarSlot1");
        hotbar[1] = GameObject.Find("HotBarSlot2");
        hotbar[2] = GameObject.Find("HotBarSlot3");
        hotbar[3] = GameObject.Find("HotBarSlot4");
        hotbar[4] = GameObject.Find("HotBarSlot5");
        hotbar[5] = GameObject.Find("HotBarSlot6");
        hotbar[6] = GameObject.Find("HotBarSlot7");
        hotbar[7] = GameObject.Find("HotBarSlot8");

        // Set position 1 to default position
        hotbar[currentPosition].GetComponent<Image>().color = selectedColor;
    }

    // Update is called once per frame
    void Update()
    {
        getNextPosition();
        //Debug.LogError(nextPosition);

        // If the next selected position is not still the current position
        if (currentPosition != nextPosition)
        {
            //Debug.Log("Hotbar position " + currentPosition + " unselected, position " + nextPosition + " selected.");
            hotbar[currentPosition].GetComponent<Image>().color = unselectedColor; // Swap old hotbar position to unselected color
            hotbar[nextPosition].GetComponent<Image>().color = selectedColor; // Swap new hotbar position to selected color
            currentPosition = nextPosition;
        }
    }

    // Gets the next hotbar position selected by the player
    void getNextPosition()
    {
        if (Input.GetButtonDown("Num1")) nextPosition = 0;
        if (Input.GetButtonDown("Num2")) nextPosition = 1;
        if (currentScene == "Sand Temple" || currentScene == "Catacombs")
        {
            if (Input.GetButtonDown("Num3")) nextPosition = 2;
            if (Input.GetButtonDown("Num4")) nextPosition = 3;
        }
        if (currentScene == "Catacombs")
        {
            if (Input.GetButtonDown("Num5")) nextPosition = 4;
            if (Input.GetButtonDown("Num6")) nextPosition = 5;
        }

        //if (Input.GetButtonDown("Num7")) nextPosition = 6;
        //if (Input.GetButtonDown("Num8")) nextPosition = 7;
    }
}
