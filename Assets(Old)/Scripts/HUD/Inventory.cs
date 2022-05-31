using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> playerItems = new List<Item>();
    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;
    private Item _coin;

    private void Start()
    {
        _coin = itemDatabase.GetItem(0);
        inventoryUI.UpdateCoinAmount(_coin);
    }

    // Give item to player with item id
    public void GiveItem(int id)
    {
        Debug.Log(itemDatabase.GetItem(1));
        Debug.Log(inventoryUI);
        Debug.Log("Stack Check: " + PutInStack(id));

        if (PutInStack(id))
        {
            Item item = CheckForItem(id);
            if (item != null)
                item.amount++;
            inventoryUI.UpdateStack(item);
            Debug.Log("Stack Amount:" + item.amount);
        }
        else
        {
            Item itemToAdd = itemDatabase.GetItem(id);
            playerItems.Add(itemToAdd);
            inventoryUI.AddNewItem(itemToAdd);
            Debug.Log("Added item: " + itemToAdd.title);
        }
    }

    // Give item to player with item name
    public void GiveItem(string itemName)
    {
        Item itemToAdd = itemDatabase.GetItem(itemName);
        playerItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.title);
    }

    public bool PutInStack(int id)
    {
       return playerItems.Contains(CheckForItem(id));
    }

    // Check for item by id
    public Item CheckForItem(int id)
    {
        return playerItems.Find(item => item.id == id);
    }

    // Remove item by id
    public void RemoveItem(int id)
    {
        Item itemToRemove = CheckForItem(id);

        if (itemToRemove != null)
        {
            if (itemToRemove.amount < 2)
            {
                playerItems.Remove(itemToRemove);
                inventoryUI.RemoveItem(itemToRemove);
                Debug.Log("Item removed: " + itemToRemove.title);
            }
            else
            {
                itemToRemove.amount--;
                inventoryUI.UpdateStack(itemToRemove);
            }
        }
    }

    public void AddCoin()
    {   
        // Null may never happen as it should exist at Start()
        if (_coin == null)
        {
            _coin = itemDatabase.GetItem(0);
            _coin.amount++;
            inventoryUI.UpdateCoinAmount(_coin);
        }
        else
        {
            _coin.amount++;
            inventoryUI.UpdateCoinAmount(_coin);
        }
    }
    
    public bool RemoveCoin(int amount = 0)
    {
        if (amount <= _coin.amount)
        {
            _coin.amount -= amount;
            inventoryUI.UpdateCoinAmount(_coin);
            return true;
        }
        else
        {
            return false;
        }
    }
}
