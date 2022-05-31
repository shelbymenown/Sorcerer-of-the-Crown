using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    public HealthSystem playerHealth;
    public ManaSystem playerMana;
    public static  bool LoadState = false; 
    private void Start()
    {
        if (LoadState == true)
        {
            Debug.Log("LoadState: TRUE");
            OnLoad();
        }
    }
    public void OnSave()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
        playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaSystem>();
        SaveData.current.player.health = playerHealth.health ;
        SaveData.current.player.mana = playerMana.mana;
        SerializationManager.Save("SOTC", SaveData.current);
        Debug.Log("SOTC SAVED");
        Debug.Log(SaveData.current.player.health);
        Debug.Log(SaveData.current.player.mana);
    }

    public void OnLoad()
    {
        if (SerializationManager.checkDirectoryExists())
        {
            SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/SOTC.dat");
            HealthSystem.healthLoad = SaveData.current.player.health;
            ManaSystem.manaLoad = SaveData.current.player.mana;
            Debug.Log("Loading Health: " + HealthSystem.healthLoad);
            Debug.Log("Loading Mana: " + ManaSystem.manaLoad);
        }
        else
        {
            Debug.Log("Directory 'saves' does not exist yet");
        }
    }

    public void SetLoadStateTrue()
    {
        LoadState= true;
    }

    public void SetLoadStateFalse()
    {
        LoadState = false;
    }
}
