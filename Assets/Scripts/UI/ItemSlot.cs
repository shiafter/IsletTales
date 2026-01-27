using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    //====ITEM DATA====
    public ItemData itemData;
    public int quantity;
    public bool full;

    //====ITEM SLOT====
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    public GameObject selectedFrame;
    public bool thisItemSelected;
    public Sprite emptySprite;
    [SerializeField]
    private ItemData emptyItem;

    private float lastClickTime;
    private float doubleClickTime = 0.3f;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = InventoryManager.instance;
    }
    public int AddItem(ItemData item, int amount)
    {
        if (item == null || item.empty)
        {
            return amount;
        }

        if (itemData == null || itemData.empty) // nếu ô trống thì thêm item vào
        {
            itemData = item;
            quantity = 0;
            full = false;

            itemImage.enabled = true;
            itemImage.sprite = item.itemImage;
        }
        else if (itemData != null && itemData != item)
        {
            return amount;
        }

        int spaceLeft = item.maxStack - quantity; //số lượng còn lại trong slot có thể stack thêm
        
        if(spaceLeft <= 0)
        {
            full = true;
            return amount;
        }

        int addAmount = Mathf.Min(spaceLeft, amount); //số lượng thêm vào slot 
        quantity += addAmount;

        quantityText.text = quantity.ToString();
        quantityText.enabled = quantity > 1;

        if(quantity >= item.maxStack)
        {
            full = true;
        }

        return amount - addAmount;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
    }
    public void OnLeftClick()
    {
        if(itemData == null || itemData.empty)
        {
            return;
        }
        if (thisItemSelected)
        {
            // Kiểm tra double click
            if (Time.time - lastClickTime <= doubleClickTime)
            {
                // Double click -> sử dụng item
                if (itemData.type != ItemData.ItemType.Consumable) return;
                inventoryManager.UseItem(itemData.itemName);

                this.quantity -= 1;
                if (this.quantity <= 0)
                {
                    EmptySlot();
                }
                else
                {
                    quantityText.text = this.quantity.ToString();
                } 
                lastClickTime = 0f;
                return;
            }
            lastClickTime = Time.time;
        }
        else
        {
            inventoryManager.DeselectAllSlot();
            selectedFrame.SetActive(true);
            thisItemSelected = true;
            inventoryManager.UpdateItemInfo(itemData);
        }
    }
    public void EmptySlot()
    {
        itemData = emptyItem;
        quantity = 0;
        full = false;

        quantityText.enabled = false;
        itemImage.enabled = false;
        itemImage.sprite = null;

        thisItemSelected = false;
        selectedFrame.SetActive(false);
    }
    public void UpdateUI()
    {
        if (itemData == null || itemData.empty)
        {
            itemImage.enabled = false;

            quantityText.enabled = false;
            return;
        }

        quantityText.text = quantity > 1 ? quantity.ToString() : "";
        quantityText.enabled = quantity > 1;

        itemImage.enabled = true;
        itemImage.sprite = itemData.itemImage;

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemData.empty)
        {
            return;
        }
        DragManager.instance.EnableDrag(itemImage.sprite, this);
    }
    public void OnDrag(PointerEventData eventData)
    {
        DragManager.instance.DragItem(eventData.position);
    }
    public void OnDrop(PointerEventData eventData)
    {
        ItemSlot sourceItem = DragManager.instance.sourceSlot;

        if (sourceItem == null || sourceItem == this)
            return;
        if (itemData.empty) //drag tới vị trí trống thì di chuyển
        {
            CopyItem(sourceItem);
            sourceItem.EmptySlot();
            return;
        }

        if (itemData == sourceItem.itemData && !full) // drag tới vị trí trùng item thì stack vào
        {
            int newQty = AddItem(sourceItem.itemData, sourceItem.quantity);
            sourceItem.quantity = newQty;

            if (sourceItem.quantity <= 0)
            {
                sourceItem.EmptySlot();
            }
            else
            {
                sourceItem.UpdateUI();
            }
            return;
        }

        SwapItemWith(sourceItem);
        return;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        DragManager.instance.DisableDrag();
    }
    public void CopyItem(ItemSlot itemToCopy)
    {
        itemData = itemToCopy.itemData;
        quantity = itemToCopy.quantity;
        full = itemToCopy.full;

        itemImage.sprite = itemToCopy.itemImage.sprite;
        itemImage.enabled = true;

        UpdateUI();
    }

    public void SwapItemWith(ItemSlot itemToSwap)
    {
        ItemData tempItem= itemData;
        int tempQuantity = quantity;
        bool tempFull = full;

        itemData = itemToSwap.itemData;
        quantity = itemToSwap.quantity;
        full = itemToSwap.full;

        itemToSwap.itemData = tempItem;
        itemToSwap.quantity = tempQuantity;
        itemToSwap.full = tempFull;

        UpdateUI();
        itemToSwap.UpdateUI();
    }
    public int RemoveAmount(int amount) //trừ số lượng nhất định vào tổng số lượng item đang có  
    {
        if(itemData == null || itemData.empty) return amount;

        if (amount <= 0) return 0;

        int removeAmount = Mathf.Min(quantity, amount); //số lượng item trừ khỏi slot
        quantity -= removeAmount; //số lượng sau khi trừ: nếu về 0 thì clear slot

        if(quantity <= 0)
        {
            EmptySlot();
        }
        else
        {
            quantityText.text = quantity.ToString();
            quantityText.enabled = quantity > 1;
        }
        return amount - removeAmount;
    }
}
