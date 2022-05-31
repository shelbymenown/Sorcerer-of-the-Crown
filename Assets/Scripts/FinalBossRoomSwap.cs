using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FinalBossRoomSwap : MonoBehaviour
{
    public Color color;
    public GameObject[] rooms;
    public Tilemap[] floor;
    public Tilemap[] walls;
    public float swapTime = 20f;
    private int currentRoom = 0;
    private float alphaLevel = 1;
    private float maxRGB = 1;
    private bool isFadingOut = false;
    private bool isSwapping = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SwapRooms", 10f, swapTime);
    }

    private void Update()
    {
        //Debug.LogError(isFadingOut + " " + isSwapping);
        // fades out current room while activating next room to fade/swap in
        if (isFadingOut)
        {
            float rgbChange = color.r;
            rgbChange -= 0.005f;

            if (rgbChange <= 0)
            {
                isFadingOut = false;
                isSwapping = true;

                maxRGB = (currentRoom == 3) ? 0.5f : 1;

                // activate next room (then fade in)
                rooms[(currentRoom + 1) % rooms.Length].SetActive(true);

                // deactivate prev room
                rooms[currentRoom].SetActive(false);

                currentRoom = (currentRoom + 1) % rooms.Length;
                //Debug.LogError("Done Fade");
            }

            color = new Color(rgbChange, rgbChange, rgbChange, alphaLevel);
            floor[currentRoom].color = color;
            walls[currentRoom].color = color;
        }
        else if (isSwapping)
        {
            float rgbChange = color.r;
            rgbChange += 0.005f;

            if (rgbChange >= maxRGB)
            {
                isSwapping = false;
                //Debug.LogError("Done Swap");
            }

            color = new Color(rgbChange, rgbChange, rgbChange, alphaLevel);
            floor[currentRoom].color = color;
            walls[currentRoom].color = color;
        }
    }

    void SwapRooms()
    {
        //Debug.LogError("Swapping..." + floor[currentRoom].color);
        color = floor[currentRoom].color;

        isFadingOut = true;

        //for (float rgbChange = color.r;  rooms[currentRoom].color.r > 0; rgbChange -= 0.00001f)
        //{
        //    color = new Color(rgbChange, rgbChange, rgbChange, alphaLevel);
        //    rooms[currentRoom].color = color;
        //}
    }
}
