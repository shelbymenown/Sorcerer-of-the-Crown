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
            new Dictionary<string, string>
            {

            }),

            new Item(1, 1, 0, "Health Potion", "Replenish Health.",
            new Dictionary<string, string>
            { 
                { "Health", "20" } 
            }),

            new Item(2, 1, 0, "Mana Potion", "Replenish Mana.", 
            new Dictionary<string, string>
            {
                {"Mana", "20" }
            }),

            new Item(3, 1, 0, "<color=#03012d>Fireball</color>", 
            "Fires a fast moving ball of fire that explodes in a large area upon \n colliding with an enemy, obstacle, or wall. The initial projectile \ndeals <b>10 damage</b> to an enemy it hits, and the explosion deals <b>20 damage</b> to \nall enemies in its area of effect.",
            new Dictionary<string, string>
            {
                {"Damage", "20 - 30" },
                { "Costs", "<color=#011288>25 mana</color>" }
            }),

            new Item(4, 1, 0, "<color=#03012d>Orb of Annihilation</color>", 
            "Fires a large and slow moving projectile that peirces all enemies in \nits path, dealing <b>25 damage</b> to any it hits, as well as peircing and\ndestroying any obstacles it comes in contact with. It is destroyed\nupon contact with a wall.",
            new Dictionary<string, string>
            {
                {"Damage", "25" },
                { "Costs", "<color=#011288>30 mana</color>" }
            }),

            new Item(5, 1, 0, "<color=#03012d>Empowered</color>", 
            "Magically enhances your primary fire and movement speed for a short\ntime. Your primary fire deals <b>40% more damage</b>, your <b>fire rate</b> is <b>25%\nfaster</b>, and your <b>movement speed</b> is <b>25% faster</b>. Empower last for <b>\n6 seconds</b> before ending.You can not cast Empower while you are already\nEmpowered.",
            new Dictionary<string, string>
            {
                {"Damage", "+40%" },
                {"Fire Rate", "+25%" },
                {"Movement Speed", "+25%" },
                {"Duration", "6 seconds" },
                { "Costs", "<color=#011288>30 mana</color>" }
            }),

            new Item(6, 1, 0, "<color=#03012d>Cone of Cold</color>", 
            "Fires a blast of frigid wind, damaging and slowing enemies in a massive\ncone in front of you. Enemies take <b>10 damage</b> and \n<b>slow down 50% for 5 seconds</b>.The slow effect stacks, and will\nhalve the enemies speed each time its applied.",
            new Dictionary<string, string>
            {
                {"Damage", "10" },
                { "Costs", "<color=#011288>25 mana</color>" }
            }),

            new Item(7, 1, 0, "<color=#03012d>Crystal Barrage</color>", "Fires a shotgun blast of crystals in a spread.",
            new Dictionary<string, string>
            {
                {"Damage", "7x12" },
                { "Costs", "<color=#011288>20 mana</color>" }
            }),

            new Item(8, 1, 0, "<color=#03012d>Imperviousness</color>", "Become <b>invulnerable</b> for <b>5 seconds</b>\n and has a <b>cooldown</b> of <b>5 seconds</b>",
            new Dictionary<string, string>
            {
                {"Protection", "100%" },
                {"Duration", "5 seconds" },
                {"Cooldown", "5 seconds" },
                { "Costs", "<color=#011288>50 mana</color>" }
            })
        };
    }
    
}
