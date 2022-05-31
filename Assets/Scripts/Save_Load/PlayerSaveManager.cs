using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSaveManager : MonoBehaviour
{
    private HealthSystem playerHealth;
    private ManaSystem playerMana;
    private GameObject player;
    private Inventory inventory;
    private ItemDatabase itemDatabase;
    public static  bool LoadState = false;
    public static bool dontLoadPosition = false;
    List<Vector3> startingPosInLevels = new List<Vector3>();
    List<Vector3> checkpointStartPosInLevels = new List<Vector3>();
    private int playerMaxHealth = 120; // UPDATE IF PLAYER MAX HEALTH CHANGES
    private int playerMaxMana = 100; // UPDATE IF PLAYER MAX HEALTH CHANGES

    private void Awake()
    {
        startingPosInLevels.Add(new Vector3(-18f,2f,0f)); // Sand
        startingPosInLevels.Add(new Vector3(-16f,5f,0f)); // Cata
        checkpointStartPosInLevels.Add(new Vector3(-11.11f, -58.9f, 0f)); // Mech (boss)
        checkpointStartPosInLevels.Add(new Vector3(261.9f, -105.8f, 0f)); // Sand 1 (enter temple)
        checkpointStartPosInLevels.Add(new Vector3(-29.00717f, -6.89204f, 0f)); // Sand 2 (exit temple)
        checkpointStartPosInLevels.Add(new Vector3(2.58f, -77.46f, 0f)); // Cata 1 (Enter Rm 2) 
        checkpointStartPosInLevels.Add(new Vector3(5.08f, -211.54f, 0f)); // Cata 2 (boss)
        checkpointStartPosInLevels.Add(new Vector3(-2.45f, -277.95f, 0f)); // Cata 3 (final boss)
    }

    private void Start()
    {
        if (LoadState == true && !PauseMenu.GameIsPaused)
        {
            Debug.Log("LoadState: TRUE");
            OnLoad();
        }
    }
    public void OnSave()
    {
        var playerData = SaveData.current.player;

        if (!(SceneManager.GetActiveScene().name == "Main Menu"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<Inventory>();
            playerHealth = player.GetComponent<HealthSystem>();
            playerMana = player.GetComponent<ManaSystem>();
            playerData.health = playerHealth.health;
            playerData.mana = playerMana.mana;
        }

        
        if (dontLoadPosition)
        {
            playerData.level = SceneManager.GetActiveScene().buildIndex + 1;
            int i = playerData.level - 2;
            if (playerData.level == 3)
            {
                i = 0;
                playerData.playerLocationX = startingPosInLevels[i][0];
                playerData.playerLocationY = startingPosInLevels[i][1];
                playerData.playerLocationZ = startingPosInLevels[i][2];
            }
            else if (playerData.level == 5)
            {
                i = 1;
                playerData.playerLocationX = startingPosInLevels[i][0];
                playerData.playerLocationY = startingPosInLevels[i][1];
                playerData.playerLocationZ = startingPosInLevels[i][2];
            }
            if (player != null)
            {
                playerData.health = player.GetComponent<HealthSystem>().maxHealth;
                playerData.mana = player.GetComponent<ManaSystem>().maxMana;
            }
            if (playerData.hotBar7Item != null)
                playerData.hotBar7Item.amount = 0;
            if (playerData.hotBar8Item != null)
                playerData.hotBar8Item.amount = 0;
        }
        else
        {
            if (!(SceneManager.GetActiveScene().name == "Main Menu"))
            {
                playerData.level = SceneManager.GetActiveScene().buildIndex - 1;
                playerData.playerLocationX = player.transform.position.x;
                playerData.playerLocationY = player.transform.position.y;
                playerData.playerLocationZ = player.transform.position.z;
                playerData.hotBar7Item = GameObject.FindGameObjectWithTag("HotBar7").GetComponentInChildren<UIItem>().item;
                playerData.hotBar8Item = GameObject.FindGameObjectWithTag("HotBar8").GetComponentInChildren<UIItem>().item;
            }
            else
            {
                playerData.level = SceneManager.GetActiveScene().buildIndex + 1;
                playerData.playerLocationX = -35.2f;
                playerData.playerLocationY = 10.5f;
                playerData.playerLocationZ = 0f;
                playerData.health = playerMaxHealth;
                playerData.mana = playerMaxMana;
            }
        }

        SerializationManager.Save("SOTC", SaveData.current);
        Debug.Log("SOTC SAVED");
        Debug.Log(playerData.health);
        Debug.Log(playerData.mana);
    }

    public void OnLoad()
    {
        // level # ?
        if (SerializationManager.checkDirectoryExists())
        {
            SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/SOTC.dat");
            var playerData = SaveData.current.player;

            // Load level scene first.
            Debug.Log("_______________ LEVEL " + playerData.level + " __________________");
            //SceneManage.LoadLevel(playerData.level);

            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                inventory = player.GetComponent<Inventory>();
                itemDatabase = inventory.itemDatabase;
            }

            

            if (player != null)
            {
                playerHealth = player.GetComponent<HealthSystem>();
                playerMana = player.GetComponent<ManaSystem>();

                if (!dontLoadPosition)
                {
                    playerHealth.health = playerData.health;
                    playerHealth.Heal(0);
                    playerMana.mana = playerData.mana;
                    playerMana.AddMana(0);
                    player.transform.position = new Vector3(playerData.playerLocationX, playerData.playerLocationY, playerData.playerLocationZ);
                    UIItem i7 = GameObject.FindGameObjectWithTag("HotBar7").GetComponentInChildren<UIItem>();
                    UIItem i8 = GameObject.FindGameObjectWithTag("HotBar8").GetComponentInChildren<UIItem>();

                    if (i7 != null)
                        if (i7.item != null && playerData.hotBar8Item != null)
                            i7.item.amount = playerData.hotBar7Item.amount;
                    if (i8 != null)
                        if (i8.item != null && playerData.hotBar8Item != null)
                            i8.item.amount = playerData.hotBar8Item.amount;
                    inventory.UpdateUI();
                }
                else
                {
                    dontLoadPosition = false;
                    playerHealth.health = playerHealth.maxHealth;
                    playerHealth.Heal(0);
                    playerMana.mana = playerMana.maxMana;
                    playerMana.AddMana(0);
                }

                // Inventory CODE:
                //Debug.Log("ITEM COUNT: " + savedItems.Count);
                //foreach (Item i in savedItems)
                //{
                //    if (i != null)
                //    {
                //        i.icon = itemDatabase.GetItem(i.id).icon;
                //    }
                       
                //}
                //// Not properly updating player items, maybe overwrites and then doesn't accurately work. Not saving/ loading items in inventory correct?
                //// after loading any items picked up afterwards is not updated to that inventory, 

                //for (int index = 0; index < savedItems.Count; index++)
                //{
                //    inventory.playerItems.Add(savedItems[index]);
                //    Debug.Log("ITEM ADDED FROM LOAD");
                //}
                //inventory.UpdateUI();
            }

            // Load hot bar items
            //GameObject hBar7 = GameObject.FindGameObjectWithTag("HotBar7");
            //GameObject hBar8 = GameObject.FindGameObjectWithTag("HotBar8");

            //if (hBar7 != null)
            //{
            //    hBar7.GetComponentInChildren<UIItem>().item = playerData.hotBar7Item;
            //}
            //if (hBar7 != null)
            //    Debug.Log("HHHHHHHHHHHHHHHHHHHHHHHHH : " + playerData.hotBar7Item.amount);

            //if (hBar8 != null)
            //    hBar8.GetComponentInChildren<UIItem>().item = playerData.hotBar8Item;

            Debug.Log("Loading Health: " + HealthSystem.healthLoad);
            Debug.Log("Loading Mana: " + ManaSystem.manaLoad);
        }
        else
        {
            Debug.Log("Directory 'saves' does not exist yet");
        }
    }

    public void LoadScene()
    {
        if (SerializationManager.checkDirectoryExists())
        {
            SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/SOTC.dat");
            var playerData = SaveData.current.player;
            SceneManage.LoadLevel(playerData.level);
        }
    }

    public void SetLoadStateTrue()
    {
        LoadState= true;
        dontLoadPosition = false;
    }

    public void SetLoadStateTrue(bool newPosition)
    {
        dontLoadPosition = true;
        OnSave();
        LoadState = true;
    }

    public void SetLoadStateFalse()
    {
        LoadState = false;
        dontLoadPosition = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Mech (boss)
        if (gameObject.tag == "Player" && collision.gameObject.tag == "Checkpoint1")
        {
            OnSave(0);
        }

        // Sand 1 (enter temple)
        if (gameObject.tag == "Player" && collision.gameObject.tag == "Checkpoint2")
        {
            OnSave(1);
        }

        // Sand 2 (exit temple)
        if (gameObject.tag == "Player" && collision.gameObject.tag == "Checkpoint3")
        {
            OnSave(2);
        }

        // Cata 1 (Enter Rm 2) 
        if (gameObject.tag == "Player" && collision.gameObject.tag == "Checkpoint4")
        {
            OnSave(3);
        }

        // Cata 2 (boss)
        if (gameObject.tag == "Player" && collision.gameObject.tag == "Checkpoint5")
        {
            OnSave(4);
        }

        // Cata 3 (final boss)
        if (gameObject.tag == "Player" && collision.gameObject.tag == "Checkpoint6")
        {
            OnSave(5);
        }
    }

    public void OnSave(int checkpointIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        playerHealth = player.GetComponent<HealthSystem>();
        playerMana = player.GetComponent<ManaSystem>();
        var playerData = SaveData.current.player;

        if (checkpointIndex == 5)
        {
            playerData.health = playerHealth.maxHealth;
            playerData.mana = playerMana.maxMana;
        }
        else
        {
            playerData.health = playerHealth.health;
            playerData.mana = playerMana.mana;
        }
        playerData.level = SceneManager.GetActiveScene().buildIndex - 1;
        playerData.playerLocationX = checkpointStartPosInLevels[checkpointIndex].x;
        playerData.playerLocationY = checkpointStartPosInLevels[checkpointIndex].y;
        playerData.playerLocationZ = checkpointStartPosInLevels[checkpointIndex].z;
        playerData.hotBar7Item = GameObject.FindGameObjectWithTag("HotBar7").GetComponentInChildren<UIItem>().item;
        playerData.hotBar8Item = GameObject.FindGameObjectWithTag("HotBar8").GetComponentInChildren<UIItem>().item;


        SerializationManager.Save("SOTC", SaveData.current);
        Debug.Log("SOTC SAVED");
        Debug.Log(playerData.health);
        Debug.Log(playerData.mana);

        GameObject GameSavedCanvas = GameObject.FindGameObjectWithTag("GameSavedCanvas");
        if (GameSavedCanvas != null)
        {
            Animator animator = GameSavedCanvas.GetComponentInChildren<Animator>();

            if (animator != null)
                animator.Play("Animate");
        }
    }
}
