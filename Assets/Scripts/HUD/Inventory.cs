using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    Animator animateText;
    //public List<Item> playerItems = new List<Item>();
    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;
    //private Item _coin;
    GameObject hotBar1;
    GameObject hotBar2;
    GameObject hotBar3;
    GameObject hotBar4;
    GameObject hotBar5;
    GameObject hotBar6;
    GameObject hotBar7;
    GameObject hotBar8;
    GameObject player;
    UIItem hotBar1Item;
    UIItem hotBar2Item;
    UIItem hotBar3Item;
    UIItem hotBar4Item;
    UIItem hotBar5Item;
    UIItem hotBar6Item;
    UIItem hotBar7Item;
    UIItem hotBar8Item;
    Item check7;
    Item check8;
    HealthSystem healthSystem;
    ManaSystem manaSystem;
    float healthPotionAmount = 20.0f;
    GameObject animationTextObject;
    Text animationText;

    private void Awake()
    {
        animationTextObject = GameObject.FindGameObjectWithTag("ItemTextAnimator");
        animateText = animationTextObject.GetComponent<Animator>();
        animationText = animationTextObject.GetComponent<Text>();
        hotBar1 = GameObject.FindGameObjectWithTag("HotBar1");
        hotBar2 = GameObject.FindGameObjectWithTag("HotBar2");
        hotBar3 = GameObject.FindGameObjectWithTag("HotBar3");
        hotBar4 = GameObject.FindGameObjectWithTag("HotBar4");
        hotBar5 = GameObject.FindGameObjectWithTag("HotBar5");
        hotBar6 = GameObject.FindGameObjectWithTag("HotBar6");
        hotBar7 = GameObject.FindGameObjectWithTag("HotBar7");
        hotBar8 = GameObject.FindGameObjectWithTag("HotBar8");
        hotBar1Item = hotBar1.GetComponentInChildren<UIItem>();
        hotBar2Item = hotBar2.GetComponentInChildren<UIItem>();
        hotBar3Item = hotBar3.GetComponentInChildren<UIItem>();
        hotBar4Item = hotBar4.GetComponentInChildren<UIItem>();
        hotBar5Item = hotBar5.GetComponentInChildren<UIItem>();
        hotBar6Item = hotBar6.GetComponentInChildren<UIItem>();
        hotBar7Item = hotBar7.GetComponentInChildren<UIItem>();
        hotBar8Item = hotBar8.GetComponentInChildren<UIItem>();

        player = GameObject.FindGameObjectWithTag("Player");
        healthSystem = player.GetComponent<HealthSystem>();
        manaSystem = player.GetComponent<ManaSystem>();
    }

    //private void Start()
    //{
    //    _coin = itemDatabase.GetItem(0);
    //    inventoryUI.UpdateCoinAmount(_coin);
    //}

    private void Start()
    {
        hotBar1Item.item = itemDatabase.GetItem(3);
        hotBar2Item.item = itemDatabase.GetItem(4);
        hotBar3Item.item = itemDatabase.GetItem(5);
        hotBar4Item.item = itemDatabase.GetItem(6);
        hotBar5Item.item = itemDatabase.GetItem(7);
        hotBar6Item.item = itemDatabase.GetItem(8);
        hotBar7Item.item = itemDatabase.GetItem(1);
        hotBar7Item.item.amount = 0;
        hotBar8Item.item = itemDatabase.GetItem(2);
        hotBar8Item.item.amount = 0;
        inventoryUI.UpdateStack(hotBar7Item.item);
        inventoryUI.UpdateStack(hotBar8Item.item);
    }

    public void UpdateUI()
    {
        inventoryUI.UpdateStack(hotBar7Item.item);
        inventoryUI.UpdateStack(hotBar8Item.item);
    }

    //public void UpdateUI()
    //{
    //    foreach (Item i in playerItems)
    //    {
    //        if (i != null)
    //        {
    //            inventoryUI.UpdateSlot(i.slot, i);
    //            Debug.Log("CHEEEEEEEEEEEEEEEEEEEEK   " + i.id + " am " + i.amount);
    //            Debug.Log("Slot Location:" + i.slot) ;
    //        }

    //    }
    //}

    // Give item to player with item id
    public void GiveItem(int id)
    {
        Debug.Log(itemDatabase.GetItem(1));
        //Debug.Log(inventoryUI);
        //Debug.Log("Stack Check: " + PutInStack(id));

        //if (PutInStack(id))
        //{
            Item item = CheckForItem(id);
            if (item != null)
                item.amount++;
        inventoryUI.UpdateStack(item);


        // animationText.text = "Added: " + item.title;
        Debug.Log("Stack Amount:" + item.amount);
        //}
        //else
        //{
        //    Item itemToAdd = itemDatabase.GetItem(id);
        //    //playerItems.Add(itemToAdd);
        //    //inventoryUI.AddNewItem(itemToAdd);
        //    // animationText.text = "Added: " + itemToAdd.title;
        //    Debug.Log("Added item: " + itemToAdd.title);
        //}

        animationText.text = "Picked Up Potion"; 
        animateText.Play(stateName: "Animate");
    }

    // Give item to player with item name
    public void GiveItem(string itemName)
    {
        Item itemToAdd = itemDatabase.GetItem(itemName);
        //playerItems.Add(itemToAdd);
        //inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.title);
    }

    //public bool PutInStack(int id)
    //{
    //   return playerItems.Contains(CheckForItem(id));
    //}

    //Check for item by id
    //public Item CheckForItem(int id)
    //{
    //    return playerItems.Find(item => item.id == id);
    //}

    public Item CheckForItem(int id)
    {
        if (id == 1)
        {
            return hotBar7Item.item;
        }
        else if (id == 2)
        {
            return hotBar8Item.item;
        }
        else
        {
            return null;
        }
    }

    public void RemoveItem(int id)
    {
        Item itemToRemove = CheckForItem(id);

        if (itemToRemove != null)
        {
            if (itemToRemove.amount > 0)
            {
                itemToRemove.amount--;
                activateItem(itemToRemove);
            }

            inventoryUI.UpdateStack(itemToRemove);
        }
    }

    // Remove item by id
    //public void RemoveItem(int id)
    //{
    //    Item itemToRemove = CheckForItem(id);

    //    Debug.Log("IREM: " + itemToRemove);

    //    if (itemToRemove != null)
    //    {
    //        if (itemToRemove.amount < 2)
    //        {
    //            playerItems.Remove(itemToRemove);
    //            check7 = hotBar7Item.item;
    //            check8 = hotBar8Item.item;
               
    //            if (check7 != null && check7.id == itemToRemove.id && itemToRemove.amount == check7.amount)
    //            {
    //                Debug.Log("Remove from hotbar7");
    //                inventoryUI.RemoveItem(itemToRemove, -1);
    //                activateItem(itemToRemove); 
    //            }
    //            else if (check8 != null && check8.id == itemToRemove.id && itemToRemove.amount == check8.amount)
    //            {
    //                Debug.Log("Remove from hotbar8");
    //                inventoryUI.RemoveItem(itemToRemove, -2);
    //                activateItem(itemToRemove);
    //            }
    //            else
    //            {
    //                inventoryUI.RemoveItem(itemToRemove, 0);
    //            }
                    
                
    //            Debug.Log("Item removed: " + itemToRemove.title);
    //        }
    //        else
    //        {
    //            itemToRemove.amount--;
    //            activateItem(itemToRemove);
    //            inventoryUI.UpdateStack(itemToRemove);
    //        }
    //    }
    //}

    private void activateItem(Item itemToRemove)
    {   
        // Health Potion
        if (itemToRemove.id == 1)
        {
            healthSystem.Heal(healthPotionAmount);
        }
        // Mana Potion
        else if (itemToRemove.id == 2)
        {
            manaSystem.AddMana(20);
        }
    }

    //public void AddCoin()
    //{   
    //    // Null may never happen as it should exist at Start()
    //    if (_coin == null)
    //    {
    //        _coin = itemDatabase.GetItem(0);
    //        _coin.amount++;
    //        inventoryUI.UpdateCoinAmount(_coin);
    //    }
    //    else
    //    {
    //        _coin.amount++;
    //        inventoryUI.UpdateCoinAmount(_coin);
    //    }
    //}
    
    //public bool RemoveCoin(int amount = 0)
    //{
    //    if (amount <= _coin.amount)
    //    {
    //        _coin.amount -= amount;
    //        inventoryUI.UpdateCoinAmount(_coin);
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
}
