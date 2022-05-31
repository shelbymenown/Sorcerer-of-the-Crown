using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem : MonoBehaviour
{
    public ManaBar manaBar;
    public int maxMana = 100;
    public int mana;
    public int manaPerSecond = 3;
    public float timeCycle = 1;
    public static int manaLoad; 

    // Start is called before the first frame update
    void Start()
    {
        mana = maxMana;
        if (gameObject == GameObject.FindGameObjectWithTag("Player") && PlayerSaveManager.LoadState == true)
        {
            manaBar = GameObject.FindGameObjectWithTag("PlayerManaBar").GetComponent<ManaBar>();
            mana = manaLoad;
            manaBar.SetMana(mana);
        }

        InvokeRepeating("ReplenishMana", 1.0f, timeCycle);
    }

    public void ReplenishMana()
    {
        AddMana(manaPerSecond);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMana(int manaAdd)
    {
        mana += manaAdd;
        if (mana > maxMana) mana = maxMana;

        if (gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            manaLoad = mana;
            manaBar.SetMana(mana);
        }
    }

    public int SpendMana(int manaCost)
    {
        if(mana - manaCost < 0)
        {
            Debug.Log("Not enough mana!");
            return 0;
        }
        else
        {
            mana -= manaCost;
            //Debug.Log("Current Mana = " + mana + " out of " + maxMana);
            if (gameObject == GameObject.FindGameObjectWithTag("Player"))
            {
                manaLoad = mana;
                manaBar.SetMana(mana);
            }
            
            return 1;
        }     
    }
        


}
