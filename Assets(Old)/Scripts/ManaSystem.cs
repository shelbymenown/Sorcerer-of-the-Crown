using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem : MonoBehaviour
{
    public ManaBar manaBar;
    public int maxMana = 100;
    public int mana;
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
    }

    // Update is called once per frame
    void Update()
    {
        
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
