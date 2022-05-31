using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider manaBar;
    public ManaSystem playerMana;

    private void Start()
    {
        playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaSystem>();
        manaBar.maxValue = playerMana.maxMana;
        manaBar.value = playerMana.mana;
    }

    public void SetMana(int mana)
    {
        manaBar.value = mana;
    }
}
