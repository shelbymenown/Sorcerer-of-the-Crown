using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    private Image spriteImage;
    private GameObject selectedItemObject;
    private UIItem selectedItem;
    private Tooltip toolTip;
    private GameObject player;
    private Inventory inventory;
    [SerializeField] protected GameObject stackSizeObject;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
        selectedItemObject = GameObject.Find("SelectedItem");
        // selectedItem = selectedItemObject.GetComponent<UIItem>();
        toolTip = GameObject.Find("ToolTip").GetComponent<Tooltip>();
    }

    private void Update()
    {
           // toolTip.gameObject.SetActive(false);
    }

    public void UpdateItem(Item item)
    {
        this.item = item;
        if (this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;

            // Update slot number to be hot bar slot number, else revert slot number to index stored in playerItems list.
            if (spriteImage.transform.parent.gameObject.transform.parent.gameObject.name == "HotBarSlot7")
            {
                this.item.slot = -1;
                Debug.Log("________________"  + this.item.amount);
            }
            else if (spriteImage.transform.parent.gameObject.transform.parent.gameObject.name == "HotBarSlot8")
            {
                this.item.slot = -2;
            }
            //else
            //{
            //    this.item.slot = inventory.playerItems.FindIndex(checkItem => checkItem.id == this.item.id);  
            //}

            if (stackSizeObject != null)
                stackSizeObject.GetComponentInChildren<Text>().text = this.item.amount.ToString();
        }
        else
        {
            // spriteImage.color = Color.clear;
            if (stackSizeObject != null)
                stackSizeObject.GetComponentInChildren<Text>().text = "";
        }
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (this.item != null && InventoryManager._gameIsPaused)
    //    {
    //        if (selectedItem.item != null)
    //        {
    //            Item clone = new Item(selectedItem.item);

    //            selectedItem.UpdateItem(this.item);
    //            UpdateItem(clone);
    //        }
    //        else
    //        {
    //            selectedItem.UpdateItem(this.item);
    //            UpdateItem(null);
    //        }
    //    }
    //    else if (selectedItem.item != null)
    //    {
    //        UpdateItem(selectedItem.item);
    //        selectedItem.UpdateItem(null);
    //    }
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item != null)
        {
            toolTip.GenerateToolTip(this.item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.gameObject.SetActive(false);
    }
}
