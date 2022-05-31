using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManaPotion : MonoBehaviour
{
    private AudioSource pickUpSfx;
    private GameObject _player;
    private Inventory _inventory;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _inventory = _player.GetComponent<Inventory>();
        pickUpSfx = GameObject.Find("PickUpSfx").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //update player inventory here
            _inventory.GiveItem(2);
            pickUpSfx.Play();
            Destroy(gameObject);
        }


    }

}
