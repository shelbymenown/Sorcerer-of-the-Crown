using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider manaBar;
    public ManaSystem playerMana;
    private Text manaAmount;

    private void Start()
    {
        playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaSystem>();
        manaAmount = GetComponentInChildren<Text>();
        manaBar.maxValue = playerMana.maxMana;
        manaBar.value = playerMana.mana;
        manaAmount.text = manaBar.value.ToString() + " / " + playerMana.maxMana.ToString();
    }

    public void SetMana(int mana)
    {
        manaBar.value = mana;
        if (manaAmount != null)
            manaAmount.text = mana.ToString() + " / " + playerMana.maxMana.ToString();
    }
}
