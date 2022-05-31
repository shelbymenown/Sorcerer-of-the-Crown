using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FadeOut : MonoBehaviour
{
    public AudioSource bossBGM1;
    public AudioSource bossBGM2;
    public Color color;
    public Tilemap[] roomParts;
    public GameObject from;

    // Start is called before the first frame update
    void Start()
    {
        color = roomParts[0].color;
    }

    // Update is called once per frame
    void Update()
    {
        float rgbChange = color.r;
        rgbChange -= 0.005f;

        if (rgbChange <= 0)
        {
            from.GetComponent<BoxCollider2D>().enabled = true;
        }

        if (bossBGM1 != null) bossBGM1.volume = bossBGM2.volume -= 0.001f;

        color = new Color(rgbChange, rgbChange, rgbChange, 1);
        for (int i = 0; i < roomParts.Length; i++)
        {
            roomParts[i].color = color;
        }
    }
}
