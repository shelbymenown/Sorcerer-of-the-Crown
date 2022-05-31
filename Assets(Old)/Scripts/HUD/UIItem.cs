using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    private Image spriteImage;
    private GameObject selectedItemObject;
    private UIItem selectedItem;
    private Tooltip toolTip;
    [SerializeField] protected GameObject stackSizeObject;

    private void Awake()
    {
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
        selectedItemObject = GameObject.Find("SelectedItem");
        selectedItem = selectedItemObject.GetComponent<UIItem>();
        toolTip = GameObject.Find("ToolTip").GetComponent<Tooltip>();
    }

    private void Update()
    {
        if (!InventoryManager._gameIsPaused)
        {
            toolTip.gameObject.SetActive(false);
        }
    }

    public void UpdateItem(Item item)
    {
        this.item = item;
        if (this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;

            
            // stackSize.text = "" + this.item.amount;
            Debug.Log(this.item.icon);
            if (stackSizeObject != null)
                stackSizeObject.GetComponentInChildren<Text>().text = this.item.amount.ToString();
        }
        else
        {
            spriteImage.color = Color.clear;
            if (stackSizeObject != null)
                stackSizeObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.item != null)
        {
            if (selectedItem.item != null)
            {
                Item clone = new Item(selectedItem.item);

                selectedItem.UpdateItem(this.item);
                UpdateItem(clone);
            }
            else
            {
                selectedItem.UpdateItem(this.item);
                UpdateItem(null);
            }
        }
        else if (selectedItem.item != null)
        {
            UpdateItem(selectedItem.item);
            selectedItem.UpdateItem(null);
        }
    }

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
