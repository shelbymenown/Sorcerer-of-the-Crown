using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    //public List<UIItem> uIItems = new List<UIItem>();
    //public GameObject slotPrefab;
    //public Transform slotPanel;
    //public int numberOfSlots = 16;
    //public List<GameObject> slotInstances = new List<GameObject>();
    //[SerializeField] protected GameObject coinAmountObject;
    GameObject hotBar7;
    GameObject hotBar8;
    UIItem hotBar7Item;
    UIItem hotBar8Item;

    public void Awake()
    {
        // Create inventory slots
        //for (int i = 0; i < numberOfSlots; i++)
        //{
        //    GameObject instance = Instantiate(slotPrefab);
        //    instance.transform.SetParent(slotPanel, false);
        //    instance.SetActive(false);
        //    uIItems.Add(instance.GetComponentInChildren<UIItem>());
        //    slotInstances.Add(instance);
        //}
        //Debug.Log(uIItems.Count);
        hotBar7 = GameObject.FindGameObjectWithTag("HotBar7");
        hotBar8 = GameObject.FindGameObjectWithTag("HotBar8");
        hotBar7Item = hotBar7.GetComponentInChildren<UIItem>();
        hotBar8Item = hotBar8.GetComponentInChildren<UIItem>();
    }

    //public void UpdateCoinAmount(Item currencyItem)
    //{
    //    coinAmountObject.GetComponent<Text>().text = currencyItem.amount.ToString();
    //}

    public void UpdateSlot(int slot, Item item)
    {
        if (slot < 0 )
        {
            switch (slot)
            {
                case -1:
                    hotBar7Item.UpdateItem(item);
                    break;
                case -2:
                    hotBar8Item.UpdateItem(item);
                    break;
            }
        }
        else
        {
            Debug.Log("THIS: " + slot);
            //uIItems[slot].UpdateItem(item);      
        }
    }

    public void UpdateStack(Item item)
    {
        Item check;
        //int i = 0;
        //foreach (GameObject obj in slotInstances)
        //{
        //    check = obj.GetComponentInChildren<UIItem>().item;
        //    if (check != null)
        //        if (check.id == item.id)
        //            UpdateSlot(i, item);
        //    i++;
        //}

        check = hotBar7Item.item;
        if (check != null)
            if (check.id == item.id)
                UpdateSlot(-1, item);

        check = hotBar8Item.item;
        if (check != null)
            if (check.id == item.id)
                UpdateSlot(-2, item);

    }

    public void AddNewItem(Item item)
    {
        //UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
    }
    public void RemoveItem(Item item, int hotbarSlot)
    {
        if (hotbarSlot < 0)
        {
            UpdateSlot(hotbarSlot, null);
        }
        //else
        //{
        //    //UpdateSlot(uIItems.FindIndex(i => i.item == item), null);
        //}


    }
}
