using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] protected GameObject coinAmountObject;
    [SerializeField] protected GameObject inventoryUI;
    internal static bool _gameIsPaused;

    private Image _inventoryImage;
    private GameObject _inventoryTitle;
    private List<GameObject> _inventorySlots;
    private bool _tabPressedAllowed = true;

    //private void Awake()
    //{
    //    //_inventorySlots = GameObject.Find("InventoryPanel").GetComponent<UIInventory>().slotInstances;
    //    _inventoryImage = inventoryUI.GetComponent<Image>();
    //    _inventoryTitle = GameObject.Find("Title");
    //    _inventoryTitle.SetActive(false);
    //    _inventoryImage.enabled = false;
    //    coinAmountObject.SetActive(false);
    //    Debug.Log("length" + _inventorySlots.Count);
    //}

    //private void Start()
    //{
    //    // _inventory = new Inventory();
    //}
    //void Update()
    //{
    //    // Prevent pressing tab when Pause Menu is displayed
    //    if (PauseMenu.GameIsPaused)
    //    {
    //        _tabPressedAllowed = false;
    //    }
    //    else
    //    {
    //        _tabPressedAllowed = true;
    //    }

    //    // Stop game time when inside inventory
    //    if (Input.GetKeyDown(KeyCode.Tab) && _tabPressedAllowed)
    //    {
    //        if (_gameIsPaused)
    //        {
    //            Resume();
    //        }
    //        else
    //        {
    //            Pause();
    //        }
    //    }
    //}

    //private void Resume()
    //{
    //    foreach (GameObject slot in _inventorySlots)
    //    {
    //        slot.SetActive(false);
    //    }
    //    _inventoryTitle.SetActive(false);
    //    _inventoryImage.enabled = false;
    //    coinAmountObject.SetActive(false);
    //    Time.timeScale = 1f;
    //    _gameIsPaused = false;
    //}

    //private void Pause()
    //{
    //    foreach (GameObject slot in _inventorySlots)
    //    {
    //        slot.SetActive(true);
    //    }
    //    _inventoryTitle.SetActive(true);
    //    _inventoryImage.enabled = true;
    //    //coinAmountObject.SetActive(true);
    //    Time.timeScale = 0f;
    //    inventoryUI.SetActive(true);
    //    _gameIsPaused = true;
    //}
}
