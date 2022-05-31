using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uIItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int numberOfSlots = 16;
    public List<GameObject> slotInstances = new List<GameObject>();
    [SerializeField] protected GameObject coinAmountObject;

    public void Awake()
    {
        // Create inventory slots
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel, false);
            instance.SetActive(false);
            uIItems.Add(instance.GetComponentInChildren<UIItem>());
            slotInstances.Add(instance);
        }
        Debug.Log(uIItems.Count);
    }

    public void UpdateCoinAmount(Item currencyItem)
    {
        coinAmountObject.GetComponent<Text>().text = currencyItem.amount.ToString();
    }

    public void UpdateSlot(int slot, Item item)
    {
        uIItems[slot].UpdateItem(item);
    }

    public void UpdateStack(Item item)
    {
        Item check;
        int i = 0;
        foreach (GameObject obj in slotInstances)
        {
            check = obj.GetComponentInChildren<UIItem>().item;
            if (check != null)
                if (check.id == item.id)
                    UpdateSlot(i, item);
            i++;
        }
    }

    public void AddNewItem(Item item)
    {
        UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
    }
    public void RemoveItem(Item item)
    {
        UpdateSlot(uIItems.FindIndex(i => i.item == item), null);
    }
}
