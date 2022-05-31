using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        BuildDataBase();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string itemName)
    {
        return items.Find(item => item.title == itemName);
    }

    void BuildDataBase()
    {
        items = new List<Item>() {
            new Item(0,0,0,"Coin", "",
            new Dictionary<string, int>
            {

            }),

            new Item(1, 1, 0, "Health Potion", "Heals player.",
            new Dictionary<string, int>
            { 
                { "Health", 20 } 
            }),

            new Item(2, 1, 0, "Mana Potion", "Replenish Mana.", 
            new Dictionary<string, int>
            {
                {"Mana", 20 }
            })
        };
    }
    
}
