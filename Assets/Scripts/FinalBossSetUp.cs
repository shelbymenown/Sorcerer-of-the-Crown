using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FinalBossSetUp : MonoBehaviour
{
    public GameObject oldBGM;
    public GameObject oldBGMBoss;
    public GameObject finalBoss;
    public GameObject finalBossRoom;
    public Color color;
    public Tilemap floor, wall, barrier;
    private bool isFadingIn = false;

    // Start is called before the first frame update
    void Start()
    {
        oldBGM = GameObject.Find("BGM");
        oldBGMBoss = GameObject.Find("Boss_BGM");
        finalBossRoom = GameObject.Find("Final_Boss_Room");
    }

    // Update is called once per frame
    void Update()
    {
        // When dialog done, then do this: FinalBossBGM, Activate Boss AI, Activate room change.
        if (isFadingIn)
        {
            float rgbChange = color.r;
            rgbChange += 0.01f;

            if (rgbChange >= 0.5f) { isFadingIn = false; }

            color = new Color(rgbChange, rgbChange, rgbChange, 1);
            floor.color = color;
            wall.color = color;
            barrier.color = color;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        oldBGM.SetActive(false);
        oldBGMBoss.SetActive(false);
        isFadingIn = true;

        Invoke("Activate", 3.0f);
    }

    void Activate()
    {
        GameObject.Find("FinalBoss_BGM").GetComponent<AudioLoop>().enabled = true;
        finalBossRoom.GetComponent<FinalBossRoomSwap>().enabled = true;
        finalBoss.SetActive(true);
        gameObject.SetActive(false);
    }
}
