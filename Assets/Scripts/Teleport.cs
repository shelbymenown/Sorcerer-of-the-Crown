using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public AudioSource teleportSfx;
    public GameObject TeleportTo;
    public GameObject Entity;
    public float seconds = 1f;
    public float delay = 5f;
    public bool forBoss = false;
    public GameObject[] TeleportList;
    private static bool isDisabled = false;

    private void Start()
    {
        isDisabled = false;
    }

    private void Update()
    {
        if (!isDisabled && forBoss)
        {
            Debug.Log("entered");
            isDisabled = true;
            TeleportTo = TeleportList[Random.Range(0, TeleportList.Length)];
            StartCoroutine(WaitToEnable());
            StartCoroutine(TeleportNow());
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isDisabled && !forBoss)
        {
            isDisabled = true;
            if (teleportSfx != null) teleportSfx.Play();
            StartCoroutine(WaitToEnable());
            StartCoroutine(TeleportNow());
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!isDisabled && forBoss)
        {
            Debug.Log("entered");
            isDisabled = true;
            StartCoroutine(WaitToEnable());
            StartCoroutine(TeleportNow());
        }
    }

    IEnumerator TeleportNow()
    {
        Debug.Log("TeleportFrom: " + Entity.transform.position);
        yield return new WaitForSeconds(seconds);
        Entity.transform.position = new Vector2(TeleportTo.transform.position.x, TeleportTo.transform.position.y);
        Debug.Log("TeleportTo: " + Entity.transform.position);
    }

    IEnumerator WaitToEnable()
    {
        yield return new WaitForSeconds(delay);
        isDisabled = false;
    }
}
