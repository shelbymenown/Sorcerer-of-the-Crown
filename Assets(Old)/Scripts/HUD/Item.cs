﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public int amount;
    public int slot;
    public string title;
    public string description;
    public Dictionary<string, int> stats = new Dictionary<string, int>();
    public Sprite icon;

    public Item(int id, int amount, int slot, string title, string description,
        Dictionary<string, int> stats)
    {
        this.amount = amount;
        this.id = id;
        this.slot = slot;
        this.title = title;
        this.description = description;
        this.icon = Resources.Load<Sprite>("ItemSprites/" + title);
        this.stats = stats;
    }

    public Item (Item item)
    {
        this.amount = item.amount;
        this.id = item.id;
        this.slot = item.slot;
        this.title = item.title;
        this.description = item.description;
        this.icon = Resources.Load<Sprite>("ItemSprites/" + item.title);
        this.stats = item.stats;
    }
}
