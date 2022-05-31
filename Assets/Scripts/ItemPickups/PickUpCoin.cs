using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoin : MonoBehaviour
{
    private GameObject _player;
    private Inventory _inventory;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _inventory = _player.GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //update player inventory here
            //_inventory.AddCoin();
            Destroy(gameObject);
        }
    }
}
